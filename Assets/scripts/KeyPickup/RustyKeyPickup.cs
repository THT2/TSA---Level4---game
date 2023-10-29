using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jail 
{
    public class RustyKeyPickup : MonoBehaviour
    {
        public float interactionDistance = 5f;
        public static bool hasRustyKey = false;

        void Update()
        {
            // Cast a ray from the camera's position forward into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object within the interaction distance
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                // Check if the object can be removed (for example, it has a specific tag)
                if (hit.collider.CompareTag("RustyKey"))
                {

                    // If the player presses 'E', remove the object
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Remove the object from the scene
                        Destroy(hit.collider.gameObject);
                        hasRustyKey = true;
                        Debug.Log("You have a RustyKey");
                    }
                }
            }
        }
    }
}
