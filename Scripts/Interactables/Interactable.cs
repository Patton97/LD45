using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string prompt { get; protected set; }
    [SerializeField] string customName;

    public void Start()
    {
        if (customName != "") { name = customName; }
        prompt = name;
    }

    public abstract void Interact();

    public string Prompt => prompt;
}
