using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Interactable
{
    [SerializeField] GameObject screenOn, screenFuzzy, screenOff;
    GameObject screen;
    bool on = false, fuzzy = true;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        UpdateScreen();
        prompt = "Turn On";
    }

    public override void Interact()
    {
        //Fail first ftw 
        if(GameManager.Player.GetCurrentItem() != null
        && GameManager.Player.GetCurrentItem().GetComponent<TVRemote>())
        {
            //If TV is off
            if(!on)
            {
                on = !on;
            }
            else
            {
                fuzzy = !fuzzy;
            }
        }
        else
        {
            on = !on;
        }        
        
        UpdateScreen();
    }

    void UpdateScreen()
    {
        Destroy(screen);


        if (on && fuzzy)
        {
            screen = Instantiate(screenFuzzy);
        }
        else if (on)
        {
            screen = Instantiate(screenOn);
        }
        else
        {
            screen = Instantiate(screenOff);
        }

        screen.transform.parent = gameObject.transform;
        screen.transform.localPosition = Vector3.zero;
        screen.transform.localRotation = Quaternion.identity;
        screen.transform.localScale = gameObject.transform.localScale;
        screen.transform.localScale = Vector3.one;
    }
}
