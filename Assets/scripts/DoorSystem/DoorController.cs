using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float doorOpenAngle = 120f; // Angle by which the door opens in degrees
    public float openSpeed = 2f; // Speed of the door opening animation
    public float interactionDistance = 1.5f;

    private bool doorStatus = false; // Indicates whether the door is open or closed
    private Quaternion initialRotation;
    private Quaternion finalRotation;


    private void Start()
    {
        initialRotation = transform.rotation;
        finalRotation = Quaternion.Euler(73, 863, doorOpenAngle);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDistance)) // Raycast 2 units in front of the door
        {
            if (hit.collider.CompareTag("CofinDoor")) // Check if the object hit by the ray is the player
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!doorStatus)
                    {
                        OpenDoor();
                    }
                    else
                    {
                        CloseDoor();
                    }
                }
            }
        }
    }

    private void OpenDoor()
    {
        doorStatus = true;
        StartCoroutine(AnimateDoor(finalRotation));
    }

    private void CloseDoor()
    {
        doorStatus = false;
        StartCoroutine(AnimateDoor(initialRotation));
    }

    private IEnumerator AnimateDoor(Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion currentRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, elapsedTime);
            yield return null;
        }
    }
}


