using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class BlockSlot : Container
{
    public ColouredBlock myBlock { get; protected set; }
    public GameObject myBlockObj;
    [SerializeField] public List<GameObject> blockPrefabs = new List<GameObject>();

    new void Start()
    {
        base.Start();
        prompt = "Place in slot";
    }

    public override void Interact()
    {
        //If no block in slot
        if (item == null)
        {
            //if player is holding a block
            //Fail first ftw
            Item playerItem = GameManager.Player.GetCurrentItem();
            if (playerItem != null && playerItem.GetComponent<ColouredBlock>() != null)
            {
                item = GameManager.Player.GetCurrentItem().GetComponent<ColouredBlock>();
                GameManager.Player.InventoryRemove(item);
            }
        }
        else //If block already present
        {
            GameManager.Player.InventoryAdd(item);
            item = null;
        }


        UpdateObject();


        //After interaction
        if (item == null)
        {
            prompt = "Place in slot";
        }
        else
        {
            prompt = "Remove from slot";
        }

    }

    new void UpdateObject()
    {
        base.UpdateObject();
        displayObject.transform.localScale *= 1.1f;

        /*
        DestroyImmediate(myBlockObj);

        if(myBlock != null)
        {
            myBlockObj = Instantiate(myBlock.blockTypes[myBlock.GetTypeNum()]);
            myBlockObj.transform.parent = gameObject.transform;
            myBlockObj.transform.localPosition = Vector3.zero;
            myBlockObj.transform.localRotation = Quaternion.identity;
            myBlockObj.transform.localScale = Vector3.one * 1.1f;
        }
          
    }    
}
*/
