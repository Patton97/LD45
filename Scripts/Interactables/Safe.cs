using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : Interactable
{
    [SerializeField] List<SafeDial> myDials = new List<SafeDial>();
    [SerializeField] string correctSequence = "2819";
    [SerializeField] Door door;
    private bool locked = true;

    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (SequenceCorrect())
        {
            door.locked = false;
            door.UpdatePrompt();
        }
    }
    public override void Interact()
    {
        //NOTHING        
    }

    bool SequenceCorrect()
    {
        return GetCurrentSequence() == correctSequence;
    }

    string GetCurrentSequence()
    {
        string currentSequence = "";
        foreach (SafeDial dial in myDials)
        {
            currentSequence += dial.currentNumber;
        }
        return currentSequence;
    }
}