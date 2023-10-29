using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThroneRoom
{

    public class Lever : MonoBehaviour
    {
        public float leverAngle = 10f; // Angle by which the door opens in degrees
        public float leverSpeed = 0.5f; // Speed of the door opening animation
        public float interactionDistance = 2f;

        private bool leverStatus = false; // Indicates whether the door is open or closed
        private Quaternion initialRotation;
        private Quaternion finalRotation;
        public static bool lever = false;

        private void Start()
        {
            initialRotation = transform.rotation;
            finalRotation = Quaternion.Euler(leverAngle, 0, 0);
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Lever"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!leverStatus)
                        {
                            UseLever();
                            lever = true;
                        }
                    }
                }
            }
        }

        private void UseLever()
        {
            leverStatus = true;
            StartCoroutine(AnimateLever(finalRotation));
        }

        private IEnumerator AnimateLever(Quaternion targetRotation)
        {
            float elapsedTime = 0f;
            Quaternion currentRotation = transform.rotation;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * leverSpeed;
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, elapsedTime);
                yield return null;
            }
        }
    }
}
