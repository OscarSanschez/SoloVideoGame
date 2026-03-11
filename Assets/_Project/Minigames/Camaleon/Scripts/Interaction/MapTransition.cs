using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEditorInternal;



public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundary;
    CinemachineConfiner2D confiner;

    [SerializeField] Dirrection dirrection;
    [SerializeField] float transitionDistance = 10f; // Distance to move the player during transition
    enum Dirrection
    {
        up, down, left, right
    }

    public void Awake()
    {
        confiner = Object.FindFirstObjectByType<CinemachineConfiner2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision) // Function that checks whenever the object is collided with
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the player is the one that touched it
        {
            confiner.BoundingShape2D = mapBoundary; // Change the boundary to the new area
            UpdatePlayerPosition(collision.gameObject); // Move the player to the new area
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPosition = player.transform.position;

        switch (dirrection)
        {
            // Adjust the values as needed (f is a float literal)
            case Dirrection.up:
                newPosition.y += transitionDistance; 
                break;
            case Dirrection.down:
                newPosition.y -= transitionDistance; 
                break;
            case Dirrection.left:
                newPosition.x -= transitionDistance; 
                break;
            case Dirrection.right:
                newPosition.x += transitionDistance; 
                break;
        }

        player.transform.position = newPosition; // Move the player to the new position
    }
}
