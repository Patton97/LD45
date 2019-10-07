using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : Interactable
{

    Vector3 openPos, shutPos;
    bool open = false;

    float speed = 2.0f;

    private new void Start()
    {
        base.Start();//god this is ugly
        openPos = transform.localPosition + new Vector3(-0.5f, 0, 0);
        shutPos = transform.localPosition;

        if (open) { prompt = "Close Drawer"; }
        else { prompt = "Open Drawer"; }
    }

    void FixedUpdate()
    {
        if (open)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, openPos, Time.deltaTime*speed);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, shutPos, Time.deltaTime* speed);
        }
    }

    public override void Interact()
    {
        open = !open;
        if (open) { prompt = "Close Drawer"; }
        else { prompt = "Open Drawer"; }
    }
}
