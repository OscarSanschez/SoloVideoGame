using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable currentInteractable = null; // Closest interactable object
    public GameObject interactionIcon; 


    void Start()
    {
        interactionIcon.SetActive(false); // Hide the interaction icon at the start
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && currentInteractable != null)
        {
            currentInteractable.Interact(); // Call the Interact method on the interactable object
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            currentInteractable = interactable; // Store the interactable object in range
            interactionIcon.SetActive(true); // Show the interaction icon
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable == currentInteractable)
        {
            currentInteractable = null; // Clear the interactable object when exiting range
            interactionIcon.SetActive(false); // Hide the interaction icon
            
        }
    }
}
