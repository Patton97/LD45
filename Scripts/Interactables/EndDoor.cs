using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : Interactable
{
    [SerializeField] List<Container> containers = new List<Container>();
    bool locked = true;

    new void Start()
    {
        base.Start();
        UpdatePrompt();
    }

    void Update()
    {
        if (KeysComplete())
        {
            locked = false;
            UpdatePrompt();
        }
    }

    public override void Interact()
    {
        if(!locked)
        {
            Application.Quit();//no time for menus
        }
    }

    void UpdatePrompt()
    {
        if(locked)
        {
            prompt = "Locked";
        }
        else
        {
            prompt = "Escape!";
        }
    }

    bool KeysComplete()
    {
        bool temp = true;//assume no
        foreach (Container container in containers)
        {
            if (container.item == null)
            {
                temp = false;
            }
        }

        return temp;
    }
}