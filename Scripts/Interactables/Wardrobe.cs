using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interactable
{
    [SerializeField] List<Door> doors = new List<Door>();
    [SerializeField] List<Container> containers = new List<Container>();
    [SerializeField] string correctSequence = "0123456";
    bool locked = true;

    void Update()
    {
        if(SequenceCorrect())
        {
            foreach(Door door in doors)
            {
                door.locked = false;
                door.UpdatePrompt();
            }
        }
    }

    public override void Interact() {/*NOTHING*/}

    bool SequenceCorrect()
    {
        string currentSequence = "";
        foreach (Container container in containers)
        {
            if(container.item!=null)
            {
                currentSequence += ((ColouredBlock)container.item).GetTypeValue();
            }            
        }        
        return currentSequence == correctSequence;
    }
}
