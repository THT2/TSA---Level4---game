using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverKeyInteraction : MonoBehaviour, IInteractable
{
    Material mat;

    public string text = "text";
    public string textResult = "text";

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    public string GetDescription()
    {
        return text;
    }

    public void Interact()
    {
        Debug.Log(textResult);
    }
}
