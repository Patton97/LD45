using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySlot : Interactable
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
        //If no block in slot
        if (myKey == null)
        {

            //if player is holding a block
            if (GameManager.Player.GetCurrentItem().GetComponent<Key>())
            {
                myKey = GameManager.Player.GetCurrentItem().GetComponent<Key>();
                UpdateKey();

                GameManager.Player.InventoryRemove(myKey);
            }
        }
        else //If block already present
        {
            GameManager.Player.InventoryAdd(myKey);
            myKey = null;
            UpdateKey();
        }


        //After interaction
        if (myKey == null)
        {
            prompt = "Place in slot";
        }
        else
        {
            prompt = "Remove from slot";
        }

    }

    void UpdateKey()
    {
        DestroyImmediate(myKeyObj);

        if (myKey != null)
        {
            myKeyObj = Instantiate(KeyPrefab);
            myKeyObj.transform.parent = gameObject.transform;
            myKeyObj.transform.localPosition = Vector3.zero;
            myKeyObj.transform.localRotation = Quaternion.identity;
            myKeyObj.transform.Rotate(new Vector3(0, 90f, 0));
            myKeyObj.transform.localScale = Vector3.one * 0.2f;
        }


    }


}
