using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Can be later split to abstract with types like ColouredBlockPuzzle, InputPuzzle, etc
public class ColouredBlockPuzzle : MonoBehaviour
{
    public bool completed { get; private set; } = false;

    [SerializeField] ColouredBlock.BlockType correct = ColouredBlock.BlockType.WhiteG;

    public void CheckPuzzle(ColouredBlock item)
    {
        if (item.GetTypeValue() == (byte)correct)
        {
            completed = true;
        }
    }

    public
}
