using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouredBlock : Item
{
    private enum Type : byte { WhiteG=0, YellowB, CyanE, GreenA, PurpleD, RedF, BlueC}
    [SerializeField] Type myType;
    [SerializeField] public List<GameObject> blockTypes = new List<GameObject>();
    [SerializeField] List<Sprite> blockSprites = new List<Sprite>();

    GameObject block;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        prompt = "Pick up block";
        UpdateBlock();
        if(sprite == null) { sprite = blockSprites[(byte)myType]; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateBlock()
    {
        DestroyImmediate(block);
        block = Instantiate(blockTypes[(byte)myType]);
        block.transform.parent = gameObject.transform;
        block.transform.localPosition = Vector3.zero;
        block.transform.localRotation = Quaternion.identity;
        block.transform.localScale = Vector3.one;
    }

    public byte GetTypeNum()
    {
        return (byte)myType;
    }
}
