using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] public Sprite sprite;

    public override void Interact()
    {
        GameManager.CurrentCharacter.InventoryAdd(this);
        gameObject.SetActive(false);
    }
}
