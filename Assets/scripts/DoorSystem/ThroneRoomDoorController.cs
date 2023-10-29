using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThroneRoom
{
    public class ThroneRoomDoorController : MonoBehaviour
    {
        public float doorOpenAngle = 90f; // Angle by which the door opens in degrees
        public float openSpeed = 2f; // Speed of the door opening animation
        public float interactionDistance = 2f;

        private bool doorStatus = false; // Indicates whether the door is open or closed
        private Quaternion initialRotation;
        private Quaternion finalRotation;

        private void Start()
        {
            initialRotation = transform.rotation;
            finalRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool GoldKey = GoldKeyPickup.hasGoldKey;
            bool LeverUse = Lever.lever;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!doorStatus && LeverUse == true && GoldKey == true)
                        {
                            OpenDoor();
                            LeverUse = false;
                            GoldKey = false;
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
}
