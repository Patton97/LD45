using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouredBlock : Item
{
    private enum BlockType : byte { WhiteG = 0, YellowB, CyanE, GreenA, PurpleD, RedF, BlueC }
    [SerializeField] BlockType type;
    [SerializeField] public List<GameObject> models = new List<GameObject>();
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] public GameObject blockObject { get; protected set; }

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        sprite = sprites[(byte)type];
        model  =  models[(byte)type];
        UpdateModel();
    }

    //ensure correct model is showing
    public void UpdateModel()
    {
        //Change object (model)
        DestroyImmediate(blockObject);
        blockObject = Instantiate(model);

        //Reattach & reset transforms
        blockObject.transform.parent = gameObject.transform;
        blockObject.transform.localPosition = Vector3.zero;
        blockObject.transform.localRotation = Quaternion.identity;
        blockObject.transform.localScale = Vector3.one;
    }

    //Kinda dumb
    public byte GetTypeValue() => (byte)type;


}
