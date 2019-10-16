using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField] GameObject switchOn, switchOff;
    [SerializeField] GameObject model;
    
    //These can be moved to a Room.cs for better ownership structure
    [SerializeField] List<Light> lights;
    [SerializeField] List<GlowInTheDark> glowInTheDarks;
    
    bool on = true;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        UpdateModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) { Interact(); } //Debug
    }

    public override void Interact()
    {
        //Update relevant variables/flags
        on = !on;
        RenderSettings.fog = !on; //If lights are on, fog is off
        

        //Update all connected lights
        foreach(Light light in lights)
        {
            light.enabled = on;
        }

        //Update all gitd objects
        foreach (GlowInTheDark thing in glowInTheDarks)
        {
            thing.gameObject.GetComponent<MeshRenderer>().enabled = !on; //If lights are on, gitd are off
        }

        UpdateModel();
        UpdatePrompt();
    }

    void UpdateModel()
    {
        DestroyImmediate(model);
        model = on ? Instantiate(switchOn) : Instantiate(switchOff);
        model.transform.parent = gameObject.transform;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = Vector3.one;
    }

    void UpdatePrompt()
    {
        prompt = on ? "Turn Off" : "Turn On";
    }
}
