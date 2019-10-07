using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string prompt { get; protected set; }

    public void Start()
    {
        prompt = "Interact";
    }

    public abstract void Interact();
}
