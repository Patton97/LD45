using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public string itemName;

    public override void Interact()
    {
        GameManager.Player.InventoryAdd(this);
        gameObject.SetActive(false);
    }
}
