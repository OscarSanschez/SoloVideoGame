using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController instance {get; private set;} // singleton instance for easy access from other scripts

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public Transform choiceContainer; // Container for dialogue choices
    public GameObject choiceButtonPrefab; // Prefab for dialogue choice buttons

    void Awake()
    {
        if (instance == null) // Singleton pattern to ensure only one instance of the DialogueController exists
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDialogueUi(bool show)
    {
        dialoguePanel.SetActive(show); // Toggle ui visibility

    }

    public void SetNpcInfo(string npcName, Sprite npcPortrait)
    {
        nameText.text = npcName; // Set the name text
        portraitImage.sprite = npcPortrait; // Set the portrait image
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text; // Set the dialogue text
    }

    public void ClearChoices()
    {
        foreach (Transform child in choiceContainer) // Clear existing choices
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateChoiceButton (string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer); // Create a new choice button
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText; // Set the button text
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick); // Add the onClick event
    }

}
