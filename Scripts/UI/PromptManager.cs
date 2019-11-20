using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PromptManager : MonoBehaviour
{
    [SerializeField] RectTransform head, middle, tail;
    [SerializeField] RectTransform promptContainer;
    [SerializeField] TextMeshProUGUI tmp;


    int iFrame = 0;
    void Update()
    {
        if(iFrame % 2 == 0)
        {
            SetPromptText((iFrame*iFrame).ToString());
        }

        iFrame++;
    }

    void SetPromptText(string newText)
    {
        promptContainer.gameObject.SetActive(newText != "");

        tmp.text = newText;

        //Resize
        middle.localScale = new Vector2(tmp.renderedWidth*0.01f, middle.localScale.y);

        // Reposition
        Vector3 newPosition = Vector3.zero;
        newPosition.x += head.localPosition.x + head.localScale.x + 45;
        newPosition.x += middle.localScale.x;

        tmp.transform.localPosition = newPosition;
        middle.localPosition = newPosition;

        newPosition.x += middle.localScale.x;
        tail.localPosition = new Vector2(middle.localPosition.x*2, middle.localScale.y);
    }
}
