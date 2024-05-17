using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class DialogueSystem : MonoBehaviour
{
    public DialogueSO dialogueExemple;

    public GameObject dialogueBox;
    public TextMeshProUGUI characterText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    public float appearanceTime;
    public float fadeAwayTime;

    [ValueDropdown("typingSpeedDropDown")]
    public float typingSpeed;

    private ValueDropdownList<float> typingSpeedDropDown = new ValueDropdownList<float>()
    {
        { "Fast", 0.04f },
        { "Normal", 0.05f },
        { "Slow", 0.075f },
    };

    public bool inEvent = false;

    public IEnumerator StartDialogue(DialogueSO dialogue)
    {
        inEvent = true;

        Debug.Log(dialogue);
        //init current dialogue index, useful to iterate through all dialogues we gotta show
        int currentDialogue = 0;
        int numberOfDialogues = dialogue.dialogues.Length;

        //SetActive gameobjects
        dialogueBox.SetActive(true);

        //iterate through dialogues, aka each characters lines
        while (currentDialogue < numberOfDialogues)
        {
            int currentLine = 0;
            int numberOfLines = dialogue.dialogues[currentDialogue].characterLines.Length;

            //Assign proper character portait and text
            characterText.text = dialogue.dialogues[currentDialogue].characterTalking.characterName;
            characterImage.sprite = dialogue.dialogues[currentDialogue].characterTalking.characterSprite;

            //iterate through each lines, display them one after the other
            while (currentLine < numberOfLines)
            {
                yield return DisplayDialogue(dialogue.dialogues[currentDialogue].characterLines[currentLine]);
                currentLine++;
            }
            currentDialogue++;
        }
        yield return FadeTextAway();
    }

    //Display Text and wait
    public IEnumerator DisplayDialogue(string dialogue)
    {
        yield return DisplayText(dialogue);

        yield return new WaitForSeconds(appearanceTime);
    }

    public void StopDialogue()
    {
        StopAllCoroutines();
    }

    public bool isShowingText()
    {
        return inEvent;
    }

    IEnumerator DisplayText(string dialogue)
    {
        dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b);
        dialogueText.text = dialogue;
        dialogueText.ForceMeshUpdate();
        // Type sentence //
        int totalVisibleCharacters = dialogueText.textInfo.characterCount; // Get # of Visible Character in text object
        int counter = 0;
        int visibleCount = 0;

        while (visibleCount < totalVisibleCharacters)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);

            dialogueText.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

            counter += 1; //Increase by one then wait

            yield return new WaitForSeconds(typingSpeed);
        }

        yield return null;
    }

    public IEnumerator FadeTextAway()
    {
        Image backGroundImage = dialogueBox.GetComponent<Image>();
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            Color icolor = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b, i);
            backGroundImage.color = icolor;
            dialogueText.color = icolor;
            characterText.color = icolor;
            characterImage.color = icolor;
            yield return null;
        }
        inEvent = false;
    }
}
