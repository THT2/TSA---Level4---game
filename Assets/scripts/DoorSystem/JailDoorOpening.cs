using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jail
{
    public class JailDoorOpening : MonoBehaviour
    {
        public float doorOpenAngle = 90f; // Angle by which the door opens in degrees
        public float openSpeed = 2f; // Speed of the door opening animation
        public float interactionDistance = 2f;
        public bool isLastDoor;

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
            bool RustyKey = RustyKeyPickup.hasRustyKey;
            bool CopperKey = CopperKeyPickup.hasCopperKey;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("JailDoor"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!doorStatus && CopperKey == true)
                        {
                            OpenDoor();
                            CopperKey = false;
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
