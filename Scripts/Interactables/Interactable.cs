using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string prompt { get; protected set; }

    public void Start()
    {
        prompt = name;
    }

    public abstract void Interact();

    public string Prompt => prompt;
}
