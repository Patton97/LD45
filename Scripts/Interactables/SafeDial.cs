using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDial : Interactable
{
    [SerializeField] private Safe safe;
    public int currentNumber { get; protected set; } = 0;

    private new void Start()
    {
        base.Start();
        prompt = "Turn Dial: " + currentNumber; //Keep all dials prompting the same safe prompt
    }

    public override void Interact()
    {
        currentNumber++;
        if(currentNumber > 9) { currentNumber = 0; }
        prompt = "Turn Dial: " + currentNumber;
    }
}
