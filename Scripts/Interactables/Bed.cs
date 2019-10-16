using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    [SerializeField] GameObject quiltOnPrefab, quiltOffPrefab;
    [SerializeField] Collider quiltOnCollider, quiltOffCollider;
    [SerializeField] GameObject quiltObject;
    bool on = true;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        UpdateQuilt();
        prompt = "Move Quilt";
    }

    public override void Interact()
    {
        on = !on;        
        UpdateQuilt();
    }

    void UpdateQuilt()
    {
        //Flip object (model)
        DestroyImmediate(quiltObject);
        quiltObject = on ? Instantiate(quiltOnPrefab) : Instantiate(quiltOffPrefab);
        
        //Flip colliders
        quiltOffCollider.enabled = !on;
        quiltOnCollider.enabled = on;

        //Reattach & reset transform
        quiltObject.transform.parent = gameObject.transform;
        quiltObject.transform.localPosition = Vector3.zero;
        quiltObject.transform.localRotation = Quaternion.identity;
    }
}
