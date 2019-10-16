using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    [SerializeField] List<Item> acceptedItems = new List<Item>();
    public Item itemScript { get; private set; }
    GameObject itemModel;

    [Header("Stored Item Transform")]
    [SerializeField] Vector3 itemPosition = Vector3.zero;
    [SerializeField] Vector3 itemRotation = Vector3.zero;
    [SerializeField] Vector3 itemScale = Vector3.one;

    [Header("Puzzle - Not Required")]
    [SerializeField] ColouredBlockPuzzle puzzle;

            
    new void Start()
    {
        base.Start();
        UpdateModel();
        UpdatePrompt();
    }

    void Update() { /*Nothing*/ }

    override public void Interact()
    {
        UpdateItem();
        UpdateModel();
        UpdatePrompt();
    }

    void UpdateItem()
    {
        //If no item in slot
        if (itemScript == null)
        {
            //Could move this NRE check to PlayerController
            //I would debug the NRE but this is just a fancy if
            try { AddItem(GameManager.Player.GetCurrentItem().GetComponent<Item>()); } catch { } 
        }
        //If item already in slot
        else
        {
            GameManager.Player.InventoryAdd(itemScript);
            itemScript = null;
        }
    }

    //NOTE: This obj/mesh is only ever for display purposes
    //      to provide faux container physicalisation
    protected void UpdateModel()
    {
        if(itemScript == null)
        {
            DestroyImmediate(itemModel);
        }
        else
        {
            itemModel = Instantiate(itemScript.Model);
            itemModel.transform.parent = gameObject.transform;
            itemModel.transform.localPosition = itemPosition;
            itemModel.transform.localRotation = Quaternion.Euler(itemRotation);
            itemModel.transform.localScale = itemScale;
        }
    }
    
    protected void UpdatePrompt()
    {
        prompt = itemScript == null ? "Place in slot" : "Remove from slot";
    }

    private void AddItem(Item newItem)
    {
        //If player is holding an ACCEPTED item
        foreach (Item item in acceptedItems)
        {
            if (item.GetType() == newItem.GetType())
            {
                itemScript = newItem;
                GameManager.Player.InventoryRemove(newItem);
            }
        }
        PuzzleCompleted();
    }

    public bool PuzzleCompleted()
    {
        //If no puzzle assigned, assume completed
        try   { return puzzle.CheckPuzzle(itemScript); }
        catch { return true; }
    }
}