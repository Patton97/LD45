using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    [SerializeField] public Sprite sprite;
    [SerializeField] protected GameObject model;

    new void Start()
    {
        base.Start();
        prompt = "Pick up " + name;
    }

    public override void Interact()
    {
        HotbarManager.AddItem(this);
        gameObject.SetActive(false);
    }

    public string Prompt => "Pick up " + name;
    public GameObject Model => model;
}
