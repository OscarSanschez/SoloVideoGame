using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    private DialogueController dialogueUi;

    public NpcDialogue dialogueData; // Reference to the ScriptableObject containing dialogue data

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private void Start()
    {
        dialogueUi = DialogueController.instance; // Get reference to the DialogueController singleton
    }

    public bool CanInteract()
    {
        return !isDialogueActive; // You can interact if a dialogue isn't already active
    }

    public void Interact()
    {
        if (dialogueData == null) // If no dialogue data 
        {
            Debug.LogWarning("No dialogue data assigned to NPC.");
            return;
        }
        if (isDialogueActive) // If dialogue is already active, ignore interaction
        {
            NextLine(); // Progress to the next line if already in dialogue
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueUi.SetNpcInfo(dialogueData.npcName, dialogueData.npcPortrait);
        dialogueUi.ShowDialogueUi(true);
        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUi.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]); // Instantly show the full line if currently typing
            isTyping = false;
        }

        // Clear choices
        dialogueUi.ClearChoices();
        // Check endDialogueLines
        if (dialogueData.EndDialogueLines.Length > dialogueIndex && dialogueData.EndDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        // Check if choices and display
        foreach(DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }



        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUi.SetDialogueText(""); // Clear the dialogue text in the UI

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUi.SetDialogueText(dialogueUi.dialogueText.text += letter); // Update the dialogue text in the UI
            SoundEffectManager.PlayVoice(dialogueData.voiceSound, dialogueData.voicePitch); // Play voice sound with specified pitch
            yield return new WaitForSeconds(dialogueData.typingSpeed); // Wait before typing next letter
        }

        isTyping = false;
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]) // If this line should auto progress
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay); // Wait before progressing
            NextLine();
        }
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i]; // Capture the next dialogue index for the onClick event
            dialogueUi.CreateChoiceButton(choice.choices[i], () => ChoseOption(nextIndex)); // Create a choice button with the appropriate text and onClick event
        }
    }

    void ChoseOption(int nextIndex)
    {
        dialogueIndex = nextIndex; // Set the dialogue index to the chosen option's next index
        dialogueUi.ClearChoices(); // Clear the choice buttons
        DisplayCurrentLine();

    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine()); 
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUi.SetDialogueText("");
        dialogueUi.ShowDialogueUi(false);
    }

}
