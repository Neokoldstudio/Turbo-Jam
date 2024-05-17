using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Custom/DialogueSystem/Character", fileName = "New Character")]
public class CharacterSO : SerializedScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
}
