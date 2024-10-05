using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Dialogue that allows to rename objects in the Hierarchy of an <see cref="Animator"/> without breaking the AnimationClip bindings
/// </summary>
public class AnimatorRenameDialogue : EditorWindow
{
    private const string k_MenuName = "GameObject/Rename safe for Animator";
    private const int k_MenuPriority = -9999;
    private const int k_WindowWidth = 290;
    private const int k_WindowHeight = 100;
    private const string k_ObjectToRenameLabel = "Object to rename";
    private const string k_NameinputID = "nameInput";
    private const string k_NewNameLabel = "New Name";
    private const string k_ApplyButtonLabel = "APPLY";
    private const string k_CancelButtonLabel = "Cancel";
    private const string k_PathSeparator = "/";
    private static readonly Vector2 k_WindowSize = new Vector2(k_WindowWidth, k_WindowHeight);
    private static readonly GUIContent k_GUIContent = new GUIContent(k_ObjectToRenameLabel);
    private static readonly Type k_GameObjectType = typeof(GameObject);
    private static readonly float k_ButtonHeight = 1.5f * EditorGUIUtility.singleLineHeight;
    private static readonly GUILayoutOption k_ButtonHeightOption = GUILayout.Height(k_ButtonHeight);

    /// <summary>
    /// Helper class for storing <see cref="EditorCurveBinding"/> and additional information
    /// </summary>
    private abstract class AnimationBindingInfo
    {
        public EditorCurveBinding Binding;

        protected AnimationBindingInfo(EditorCurveBinding binding)
        {
            Binding = binding;
        }
    }

    /// <summary>
    /// An <see cref="AnimationBindingInfo"/> for storing the related <see cref="AnimationCurve"/>
    /// </summary>
    private class AnimationFloatBindingInfo : AnimationBindingInfo
    {
        public AnimationCurve Curve;

        public AnimationFloatBindingInfo(EditorCurveBinding binding, AnimationCurve curve) : base(binding)
        {
            Curve = curve;
        }
    }

    /// <summary>
    /// An <see cref="AnimationBindingInfo"/> for storing the related <see cref="ObjectReferenceKeyframe"/>s
    /// </summary>
    private class AnimationObjectBindingInfo : AnimationBindingInfo
    {
        public ObjectReferenceKeyframe[] Curve;

        public AnimationObjectBindingInfo(EditorCurveBinding binding, ObjectReferenceKeyframe[] curve) : base(binding)
        {
            Curve = curve;
        }
    }

    private GameObject m_SelectedObject;
    private Animator m_ParentAnimator;
    private string m_NewName;
    private bool m_First = true;
    private bool m_ShouldClose;

    /// <summary>
    /// Validation method for <see cref="OpenRenameDialog"/>
    /// </summary>
    /// <returns><see langword="true"/> if in Edit mode and if the selection is a single object that is nested under an <see cref="Animator"/></returns>
    [MenuItem(k_MenuName, true)]
    private static bool CanRename()
    {
        if (Application.isPlaying)
        {
            return false;
        }

        var selectedObjects = Selection.GetFiltered<GameObject>(SelectionMode.Editable | SelectionMode.ExcludePrefab);

        if (selectedObjects == null || selectedObjects.Length != 1)
        {
            return false;
        }

        var selectedObject = selectedObjects[0];
        var animator = selectedObject.GetComponentInParent<Animator>();

        if (!animator)
        {
            return false;
        }

        return animator.transform != selectedObject.transform;
    }

    /// <summary>
    /// Opens the rename dialogue window
    /// </summary>
    [MenuItem(k_MenuName, false, k_MenuPriority)]
    private static void OpenRenameDialog()
    {
        if (Application.isPlaying)
        {
            return;
        }

        var selectedObjects = Selection.GetFiltered<GameObject>(SelectionMode.Editable | SelectionMode.ExcludePrefab);

        if (selectedObjects == null || selectedObjects.Length != 1)
        {
            return;
        }

        var selectedObject = selectedObjects[0];
        var animator = selectedObject.GetComponentInParent<Animator>();

        if (!animator || animator.transform == selectedObject.transform)
        {
            return;
        }

        var window = GetWindow<AnimatorRenameDialogue>();
        window.m_SelectedObject = selectedObject;
        window.m_NewName = window.m_SelectedObject.name;
        window.m_ParentAnimator = animator;

        window.minSize = k_WindowSize;
        window.maxSize = k_WindowSize;

        window.ShowModalUtility();
    }

    private void OnLostFocus()
    {
        // Have to delay the closing - otherwise Unity crashes!
        m_ShouldClose = true;
        Repaint();
    }

    private void OnGUI()
    {
        if (m_ShouldClose)
        {
            Close();
            return;
        }

        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.ObjectField(k_GUIContent, m_SelectedObject, k_GameObjectType, false);
        }

        EditorGUILayout.Space();

        // Only the first time the dialog opens select the text already
        if (m_First)
        {
            GUI.SetNextControlName(k_NameinputID);
        }

        m_NewName = EditorGUILayout.TextField(k_NewNameLabel, m_NewName);

        if (m_First)
        {
            EditorGUI.FocusTextInControl(k_NameinputID);
            m_First = false;
        }

        EditorGUILayout.Space();

        // also handle Escape and Enter keys 
        var currentEvent = Event.current;

        GUILayout.FlexibleSpace();

        using (new EditorGUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();

            using (new EditorGUI.DisabledScope(string.IsNullOrWhiteSpace(m_NewName) || m_NewName == m_SelectedObject.name))
            {
                var color = GUI.color;
                GUI.color = Color.green;

                if (GUILayout.Button(k_ApplyButtonLabel, k_ButtonHeightOption) || currentEvent.type == EventType.KeyDown && currentEvent.keyCode == KeyCode.Return)
                {
                    RenameObjectSafe(m_SelectedObject, m_ParentAnimator, m_NewName);
                    m_ShouldClose = true;
                }

                GUI.color = color;
            }

            EditorGUILayout.Space();

            if (GUILayout.Button(k_CancelButtonLabel, k_ButtonHeightOption) || currentEvent.type == EventType.KeyDown && currentEvent.keyCode == KeyCode.Escape)
            {
                m_ShouldClose = true;
            }

            GUILayout.FlexibleSpace();
        }

        EditorGUILayout.Space();
    }

    /// <summary>
    /// Replace the old path by the new path within the <see cref="EditorCurveBinding"/>.<see cref="EditorCurveBinding.path"/>
    /// </summary>
    /// <returns><see langword="true"/> if this binding was affected by change</returns>
    private static bool ReplaceBindingPath(EditorCurveBinding originalBinding, string oldPath, string newPath, out EditorCurveBinding changedBinding)
    {
        var oldBindingPath = originalBinding.path;
        changedBinding = originalBinding;

        // Now comes the clue: Does the path contain our gameObject?
        if (oldBindingPath.StartsWith(oldPath))
        {
            // If yes now in the path we need to replace the old name with the new name
            // -> cut away the oldPath
            var newBindingPath = oldBindingPath.Substring(oldPath.Length);

            // then prepend the new path
            newBindingPath = newPath + newBindingPath;

            changedBinding.path = newBindingPath;

            return true;
        }

        return false;
    }

    /// <summary>
    /// Executes the renaming of a <see cref="GameObject"/> that is nested under an <see cref="Animator"/>
    /// <para>Goes through all <see cref="AnimationClip"/>s used by the <see cref="Animator"/> and changes matching binding paths to the new name</para>
    /// </summary>
    /// <param name="gameObject">The <see cref="GameObject"/> to be renamed</param>
    /// <param name="animator">The <see cref="Animator"/> the gameObject is nested under</param>
    /// <param name="newName">The new name to assign to the gameObject</param>
    public static void RenameObjectSafe(GameObject gameObject, Animator animator, string newName)
    {
        if (!gameObject)
        {
            throw new ArgumentException("No object provided", nameof(gameObject));
        }

        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Object name may not be empty!", nameof(newName));
        }

        if (!animator)
        {
            throw new ArgumentException($"Selected object {gameObject} is not a child of an {nameof(Animator)}!", nameof(gameObject));
        }

        if (gameObject.transform == animator.transform)
        {
            throw new ArgumentException($"Not applicable to the {nameof(Animator)} root object itself! In that case simply rename it the normal way.", nameof(newName));
        }

        // get the relative path from the animator root to this object's parent
        var path = AnimationUtility.CalculateTransformPath(gameObject.transform.parent, animator.transform);

        // In case the parent is the animator itself the path will be empty and we don't want to append the trailing "/"
        if (gameObject.transform.parent != animator.transform)
        {
            path += k_PathSeparator;
        }

        // then append the old and new names
        var oldPath = path + gameObject.name;
        var newPath = path + newName;

        // get all clips used by this Animator
        var controller = animator.runtimeAnimatorController;
        var clips = controller.animationClips;

        // Record all possibly affected assets -> The clips and the gameObject
        var changeableObjects = new List<Object>(clips.Length + 1) { gameObject };
        changeableObjects.AddRange(clips);
        Undo.RecordObjects(changeableObjects.ToArray(), $"Change animated object name from \"{gameObject.name}\" to \"{newName}\"");

        foreach (var clip in clips)
        {
            var floatBindingChanges = new List<AnimationFloatBindingInfo>();

            // Get and store the affected FLOAT keyframe bindings (internally everything that is not an Object reference)
            foreach (var binding in AnimationUtility.GetCurveBindings(clip))
            {
                var curve = AnimationUtility.GetEditorCurve(clip, binding);

                if (ReplaceBindingPath(binding, oldPath, newPath, out var changedBinding))
                {
                    // for each affected binding store two items:
                    // - The original binding with the path that shall be removed (AnimationCurve = null)
                    // - The new binding with the changed path
                    var removeBindingInfo = new AnimationFloatBindingInfo(binding, null);
                    var addBindingInfo = new AnimationFloatBindingInfo(changedBinding, curve);

                    floatBindingChanges.Add(removeBindingInfo);
                    floatBindingChanges.Add(addBindingInfo);
                }
            }

            var objectBindingChanges = new List<AnimationObjectBindingInfo>();

            // Get and store ALL OBJECT reference keyframe bindings 
            foreach (var binding in AnimationUtility.GetObjectReferenceCurveBindings(clip))
            {
                var curve = AnimationUtility.GetObjectReferenceCurve(clip, binding);

                if (ReplaceBindingPath(binding, oldPath, newPath, out var changedBinding))
                {
                    // for each affected binding store two items:
                    // - The original binding with the path that shall be removed (AnimationCurve = null)
                    // - The new binding with the changed path
                    var removeBindingInfo = new AnimationObjectBindingInfo(binding, null);
                    var addBindingInfo = new AnimationObjectBindingInfo(changedBinding, curve);

                    objectBindingChanges.Add(removeBindingInfo);
                    objectBindingChanges.Add(addBindingInfo);
                }
            }

            // a little check to avoid unnecessary work -> are there any affected bindings at all?
            if (floatBindingChanges.Count + objectBindingChanges.Count > 0)
            {
                // Now erase all curves 
                clip.ClearCurves();

                // and assign back ALL the stored ones
                AnimationUtility.SetEditorCurves(clip, floatBindingChanges.Select(info => info.Binding).ToArray(), floatBindingChanges.Select(info => info.Curve).ToArray());
                AnimationUtility.SetObjectReferenceCurves(clip, objectBindingChanges.Select(info => info.Binding).ToArray(), objectBindingChanges.Select(info => info.Curve).ToArray());
                EditorUtility.SetDirty(clip);
            }

            // finally rename the object itself
            gameObject.name = newName;
            EditorUtility.SetDirty(gameObject);
        }
    }
}