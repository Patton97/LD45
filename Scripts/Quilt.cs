using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quilt : Interactable
{
    [SerializeField] GameObject quiltOn, quiltOff;
    GameObject quilt;
    bool on = true;
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        UpdateQuilt();
        hoverPrompt = "Move Quilt";
    }

    public override void Interact()
    {
        on = !on;
        Destroy(quilt);
        UpdateQuilt();
    }

    void UpdateQuilt()
    {
        quilt = on ? Instantiate(quiltOn) : Instantiate(quiltOff);
        quilt.transform.parent = gameObject.transform;
        quilt.transform.localPosition = Vector3.zero;
        quilt.transform.localRotation = Quaternion.identity;
    }
}
