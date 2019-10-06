using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField] GameObject water;
    bool taps = false;//TEMP
    bool plugged = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (taps && plugged)
        {
            FillWater();
        }
        else if(!plugged)
        {
            DrainWater();
        }
    }

    private void FillWater()
    {
        water.GetComponent<MeshRenderer>().enabled = true;
        water.transform.position += new Vector3(0, 0.05f * Time.deltaTime, 0);
        if (water.transform.position.y > 0.95)
        {
            taps = false;
        }
    }

    private void DrainWater()
    {
        water.transform.position -= new Vector3(0, 0.05f * Time.deltaTime, 0);
        if (water.transform.position.y < 0.8)
        {
            plugged = true;
            water.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void TapsOn()  => taps = true;
    public void TapsOff() => taps = false;
    public void PullPlug() => plugged = false;
}
