using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : Interactable
{
    [SerializeField] List<KeySlot> myKeys = new List<KeySlot>();
    private bool locked = true;

    private new void Start()
    {
        base.Start();
        UpdatePrompt();
    }

    private void Update()
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
        foreach (KeySlot slot in myKeys)
        {
            if (slot.myKey == null)
            {
                temp = false;
            }
        }

        return temp;
    }
}