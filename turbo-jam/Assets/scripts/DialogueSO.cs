using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Custom/DialogueSystem/Dialogue", fileName = "New Dialogue")]
public class DialogueSO : SerializedScriptableObject
{
    [System.Serializable]
    public class Dialogue
    {
        public CharacterSO characterTalking;
        public string[] characterLines;
    }
    public Dialogue[] dialogues;
}


