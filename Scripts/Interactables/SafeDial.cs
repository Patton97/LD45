using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SafeDial : Interactable
{
    [SerializeField] private Safe safe;
    [SerializeField] private TextMeshPro DisplayText;
    public int currentNumber { get; protected set; } = 0;

    private new void Start()
    {
        base.Start();
        prompt = "Turn Dial";
    }

    public override void Interact()
    {
        currentNumber++;
        if(currentNumber > 9) { currentNumber = 0; }
        DisplayText.text = currentNumber.ToString();
    }
}
