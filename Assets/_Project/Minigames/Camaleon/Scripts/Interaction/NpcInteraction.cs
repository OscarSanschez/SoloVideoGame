using UnityEngine;

public class NpcInteraction : MonoBehaviour, IInteractable
{

    public bool isInteracting {get; private set;}

    void Start()
    {
        
    }

    public bool CanInteract()
    {
        return !isInteracting; // If you arent interacting: you can interact, if you are:you cant do it again.
    }

    public void Interact()
    {
        Debug.Log("Interacting with NPC");
        SoundEffectManager.Play("Interaction");
        if (!CanInteract())
        {
            return;
        } 
        StartDialogue();
    }
    
    private void StartDialogue()
    {
        Debug.Log("Starting dialogue with NPC");
        isInteracting = true;
    }

    public void SetAvailable()
    {
        isInteracting = false;
    }

}
