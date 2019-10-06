using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    bool selected = false;
    protected string hoverPrompt;

    public void Start()
    {
        hoverPrompt = "Interact";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && selected)
        {
            Interact();
        }
    }

    public void Hover()
    {
        selected = true;
        GameManager.CurrentCharacter.SetPrompt(hoverPrompt);
    }
    public void Unhover()
    {
        selected = false;
        GameManager.CurrentCharacter.SetPrompt("");
    }

    public abstract void Interact();
}
