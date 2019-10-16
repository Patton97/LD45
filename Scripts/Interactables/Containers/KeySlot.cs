using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySlot : Container
{
    public Key myKey { get; protected set; }
    public GameObject myKeyObj;
    [SerializeField] GameObject KeyPrefab;//dogshit in a hurry 
    new void Start()
    {
        base.Start();
        prompt = "Place in slot";
    }

    public override void Interact()
    {
        //If no key in slot
        if (myKey == null)
        {

            //if player is holding a key
            if (GameManager.Player.GetCurrentItem().GetComponent<Key>())
            {
                myKey = GameManager.Player.GetCurrentItem().GetComponent<Key>();
                GameManager.Player.InventoryRemove(myKey);
            }
        }
        else //If block already present
        {
            GameManager.Player.InventoryAdd(myKey);
            myKey = null;
        }

        UpdateModel();
    }
}
