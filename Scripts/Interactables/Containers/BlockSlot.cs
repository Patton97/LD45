using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlot : Interactable
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
        if (myBlock == null)
        {
            //if player is holding a block
            //Fail first ftw
            Item item = GameManager.Player.GetCurrentItem();
            if (item != null && item.GetComponent<ColouredBlock>() != null)
            {
                myBlock = GameManager.Player.GetCurrentItem().GetComponent<ColouredBlock>();
                GameManager.Player.InventoryRemove(myBlock);
            }
        }
        else //If block already present
        {
            GameManager.Player.InventoryAdd(myBlock);
            myBlock = null;
        }


        UpdateBlock();


        //After interaction
        if (myBlock == null)
        {
            prompt = "Place in slot";
        }
        else
        {
            prompt = "Remove from slot";
        }

    }

    void UpdateBlock()
    {
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
