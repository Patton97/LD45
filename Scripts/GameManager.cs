using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton enforcement
    private static GameManager instance = null;
    private void SetInstance()
    {
        if (instance == null) { instance = this; }
    }

    //Really can't decide whether to capitalise these or not
    public static PlayerController Player;

    void Awake()
    {
        SetInstance();
        Player = FindObjectOfType<PlayerController>();
    }
}
