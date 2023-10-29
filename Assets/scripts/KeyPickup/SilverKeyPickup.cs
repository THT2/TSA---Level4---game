using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArmoryDoor
{
    public class SilverKeyPickup : MonoBehaviour
    {
        public float interactionDistance = 2f;
        public static bool hasSilverKey = false;

        void Update()
        {
            // Cast a ray from the camera's position forward into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object within the interaction distance
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // Check if the object can be removed (for example, it has a specific tag)
                if (hit.collider.CompareTag("RemovableObject"))
                {
                    // Display a message to the player to press 'E' to remove the object
                    Debug.Log("Press 'E' to remove the object");

                    // If the player presses 'E', remove the object
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Remove the object from the scene
                        Destroy(hit.collider.gameObject);
                        hasSilverKey = true;
                        Debug.Log("You have a SilverKey");
                    }
                }
            }
        }
    }
}
