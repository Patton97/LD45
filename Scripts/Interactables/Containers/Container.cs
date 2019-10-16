using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    [SerializeField] List<Item> acceptedItems = new List<Item>();
    [SerializeField] public Item item;
    GameObject itemModel;

    [Header("Stored Item Transform")]
    [SerializeField] Vector3 itemPosition = Vector3.zero;
    [SerializeField] Vector3 itemRotation = Vector3.zero;
    [SerializeField] Vector3 itemScale = Vector3.zero;

            
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
        if (item == null)
        {
            //If player is holding an ACCEPTED item
            Item newItem = GameManager.Player.GetCurrentItem().GetComponent<Item>();
            if (newItem != null && acceptedItems.Contains(newItem))
            {      
                GameManager.Player.InventoryRemove(newItem);
            }
            else
            {
                Debug.Log("acceptedItems: " + acceptedItems[0]);
            }
        }
        //If item already in slot
        else
        {
            GameManager.Player.InventoryAdd(item);
            item = null;
        }
    }

    //NOTE: This obj/mesh is only ever for display purposes
    //      to provide faux container physicalisation
    protected void UpdateModel()
    {
        if(item == null)
        {
            DestroyImmediate(itemModel);
        }
        else
        {
            itemModel = Instantiate(item.Model);
            itemModel.transform.parent = gameObject.transform;
            itemModel.transform.localPosition = itemPosition;
            itemModel.transform.localRotation = Quaternion.Euler(itemRotation);
            itemModel.transform.localScale = itemScale;
        }
    }
    
    protected void UpdatePrompt()
    {
        prompt = item == null ? "Place in slot" : "Remove from slot";
    }
}
