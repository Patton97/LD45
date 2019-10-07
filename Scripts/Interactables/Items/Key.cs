using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    private new void Start()
    {
        base.Start();//god this is ugly
        prompt = "Pick up Key";
    }
}
