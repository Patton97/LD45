using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    [SerializeField] GameObject quiltOn, quiltOff;
    GameObject quilt;
    bool on = true;
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        UpdateQuilt();
        prompt = "Move Quilt";
    }

    public override void Interact()
    {
        on = !on;
        Destroy(quilt);
        UpdateQuilt();
    }

    void UpdateQuilt()
    {
        if(on)
        {
            quilt = Instantiate(quiltOn);
            gameObject.GetComponent<BoxCollider>().center = new Vector3(-.5f, .6f, -1.2f);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(1f, .2f, 1.4f);
        }
        else
        {
            quilt = Instantiate(quiltOff);
            gameObject.GetComponent<BoxCollider>().center = new Vector3(-.5f, .65f, -1.6f);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(1f, .3f, 0.6f);
        }
        quilt.transform.parent = gameObject.transform;
        quilt.transform.localPosition = Vector3.zero;
        quilt.transform.localRotation = Quaternion.identity;
    }
}
