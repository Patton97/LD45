using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interactable
{
    bool locked = true;
    [SerializeField] string correctSequence = "0123456";
    [SerializeField] List<BlockSlot> mySlots = new List<BlockSlot>();
    [SerializeField] Door leftDoor, rightDoor;

    new void Start()
    {
        base.Start();
        prompt = "Locked";
    }

    private void Update()
    {
        if(SequenceCorrect())
        {
            leftDoor.locked = false;
            leftDoor.UpdatePrompt();
            rightDoor.locked = false;
            rightDoor.UpdatePrompt();
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
        foreach (BlockSlot slot in mySlots)
        {
            if(slot.myBlock!=null)
            {
                currentSequence += slot.myBlock.GetTypeNum();
            }            
        }
        return currentSequence;
    }
}
