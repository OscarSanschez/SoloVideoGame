using UnityEngine;

[CreateAssetMenu(fileName = "NewNpcDialogue", menuName = "Npc Dialogue")]
public class NpcDialogue : ScriptableObject
{

    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool []autoProgressLines;
    public bool[] EndDialogueLines; // Array to specify which lines end the dialogue
    public float autoProgressDelay = 2f; // Time to wait before automatically progressing to the next line

    public float typingSpeed = 0.05f; // Time between each character being typed
    public AudioClip voiceSound;
    public float voicePitch = 1f;

    public DialogueChoice[] choices; // Array of dialogue choices for branching dialogues

}

[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex; // The dialogue line where the choices appear
    public string[] choices; // The text for each choice
    public int[] nextDialogueIndexes; // The dialogue line to go to for each choice


}


