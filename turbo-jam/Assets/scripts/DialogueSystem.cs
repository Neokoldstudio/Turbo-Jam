using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class DialogueSystem : MonoBehaviour
{
    public AnimationController animController;

    public Image dialogueBox;
    public TextMeshProUGUI characterText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public Color backgroundColor;

    public float appearanceTime;
    public float fadeAwayTime;

    [ValueDropdown("typingSpeedDropDown")]
    public float typingSpeed;

    private ValueDropdownList<float> typingSpeedDropDown = new ValueDropdownList<float>()
    {
        { "Nitro Mega Fast", 0.005f },
        { "Fast", 0.04f },
        { "Normal", 0.05f },
        { "Slow", 0.075f },
    };

    public bool inEvent = false;

    public IEnumerator StartDialogue(DialogueSO dialogue)
    {
        inEvent = true;

        //Make sure colors are set
        dialogueBox.color = backgroundColor;
        dialogueText.color = Color.white;
        characterText.color = Color.white;
        characterImage.color = Color.white;


        Debug.Log(dialogue);
        //init current dialogue index, useful to iterate through all dialogues we gotta show
        int currentDialogue = 0;
        int numberOfDialogues = dialogue.dialogues.Length;

        //SetActive gameobjects
        dialogueBox.gameObject.SetActive(true);

        //iterate through dialogues, aka each characters lines
        while (currentDialogue < numberOfDialogues)
        {
            int currentLine = 0;
            int numberOfLines = dialogue.dialogues[currentDialogue].characterLines.Length;

            //Assign proper character portait and text
            CharacterSO currentCharacter = dialogue.dialogues[currentDialogue].characterTalking;

            characterText.text = (currentCharacter != null)? currentCharacter.characterName:"";
            characterImage.sprite = (currentCharacter == null || currentCharacter.characterSprite == null) ?null: currentCharacter.characterSprite;

            //iterate through each lines, display them one after the other
            while (currentLine < numberOfLines)
            {
                yield return DisplayDialogue(dialogue.dialogues[currentDialogue].characterLines[currentLine], dialogue.onEndTrigger);
                currentLine++;
            }
            currentDialogue++;
        }
        yield return FadeTextAway();

        EndDialogue(dialogue.onEndTrigger);
    }

    //Display Text and wait
    public IEnumerator DisplayDialogue(string dialogue, string endTrigger)
    {
        yield return DisplayText(dialogue, endTrigger);

        if (Input.GetKey(KeyCode.Space))
        {
            EndDialogue(endTrigger);
        }
        yield return new WaitForSeconds(appearanceTime);
    }

    public void EndDialogue(string dialogueTrigger)
    {
        if (dialogueTrigger != null)
        {
            animController.anim.SetTrigger(dialogueTrigger);
        }
        SetColorToDialogueBox(Color.clear);
        StopDialogue();
        dialogueBox.gameObject.SetActive(false);
    }

    public void StopDialogue()
    {
        StopAllCoroutines();
    }

    public bool isShowingText()
    {
        return inEvent;
    }

    IEnumerator DisplayText(string dialogue, string endTrigger)
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

            if (Input.GetKey(KeyCode.Space))
            {
                EndDialogue(endTrigger);
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        yield return null;
    }

    public void SetColorToDialogueBox(Color icolor)
    {
        dialogueBox.color = icolor;
        dialogueText.color = icolor;
        characterText.color = icolor;
        characterImage.color = icolor;
    }

    public IEnumerator FadeTextAway()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            Color icolor = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, Mathf.Clamp(i,0, backgroundColor.a));
            SetColorToDialogueBox(icolor);
            yield return null;
        }
        inEvent = false;
    }
}
