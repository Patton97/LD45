﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] new string name;
    public string prompt { get; protected set; }

    public void Start()
    {
        //base.name = name;
        prompt = name;
    }

    public abstract void Interact();

    public string Prompt => prompt;
}
