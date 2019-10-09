using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] public Sprite sprite;
    [SerializeField] GameObject prefab;

    new void Start()
    {
        base.Start();
        prompt = "Pick up " + name;
    }

    public override void Interact()
    {
        GameManager.Player.InventoryAdd(this);
        gameObject.SetActive(false);
    }

    public string Prompt => "Pick up " + name;
}
