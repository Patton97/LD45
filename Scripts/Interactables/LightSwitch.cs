﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField] List<Light> connectedLights;
    [SerializeField] GameObject switchOn, switchOff;
    GameObject switchModel;
    bool on = true;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        UpdateSwitchModel();
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
        prompt = on ? "Turn Off" : "Turn On";

        //Update all connected lights
        foreach(Light light in connectedLights)
        {
            light.enabled = on;
        }


        //Update all gitd objects
        foreach (GlowInTheDark thing in FindObjectsOfType<GlowInTheDark>())
        {
            thing.gameObject.GetComponent<MeshRenderer>().enabled = !on; //If lights are on, gitd are off
        }

        UpdateSwitchModel();
    }

    void UpdateSwitchModel()
    {
        DestroyImmediate(switchModel);
        switchModel = on ? Instantiate(switchOn) : Instantiate(switchOff);
        switchModel.transform.parent = gameObject.transform;
        switchModel.transform.localPosition = Vector3.zero;
        switchModel.transform.localRotation = Quaternion.identity;
        switchModel.transform.localScale = Vector3.one;
    }

    //Because lightswitch is rendered based on play variables
    void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0.025f, 0.025f);
        Vector3 scale = new Vector3(.5f, .7f, .2f);
        Gizmos.color = Color.grey;
        Gizmos.DrawCube(gameObject.transform.position + offset, Vector3.Scale(gameObject.transform.lossyScale, scale));
    }
}