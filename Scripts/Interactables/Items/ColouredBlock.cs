using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ColouredBlock : Item
{
    private enum Type : byte { WhiteG = 0, YellowB, CyanE, GreenA, PurpleD, RedF, BlueC }
    [SerializeField] Type myType;
    [SerializeField] public List<GameObject> blockTypes = new List<GameObject>();
    [SerializeField] List<Sprite> blockSprites = new List<Sprite>();
    [SerializeField] GameObject blockObject;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        UpdateBlock();//ensure correct model is showing
        if (sprite == null) { sprite = blockSprites[(byte)myType]; }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateBlock()
    {
        //Change object (model)
        DestroyImmediate(blockObject);
        blockObject = Instantiate(blockTypes[(byte)myType]);

        //Reattach & reset transforms
        blockObject.transform.parent = gameObject.transform;
        blockObject.transform.localPosition = Vector3.zero;
        blockObject.transform.localRotation = Quaternion.identity;
        blockObject.transform.localScale = Vector3.one;
    }

    public byte GetTypeNum() => (byte)myType;
}
