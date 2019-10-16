using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Interactable
{
    [SerializeField] List<Door> doors = new List<Door>();
    [SerializeField] List<Container> containers = new List<Container>();
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
        foreach (Container container in containers)
        {
            if(!container.PuzzleCompleted())
            {
                return false;
            }            
        }
        //If this point is reached, all puzzles were completed
        return true;
    }
}
