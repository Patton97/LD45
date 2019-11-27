using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PromptManager : MonoBehaviour
{
    [SerializeField] RectTransform promptContainer;
    [SerializeField] TextMeshProUGUI tmp;
    CanvasGroup canvasgroup;

    #region singletonpattern    
    private static PromptManager instance = null;
    private void SetInstance()
    {
        if (instance == null) { instance = this; }
    }
    #endregion

    void Awake()
    {
        SetInstance();
        canvasgroup = promptContainer.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        
    }

    public void SetPromptText(string newText)
    {
        tmp.text = newText;

        //hide when no prompt available
        if (tmp.text.Length == 0) Hide(); else Show();

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(promptContainer);
    }

    void Show() => canvasgroup.alpha = 1f;
    void Hide() => canvasgroup.alpha = 0f;
    void Toggle() => canvasgroup.alpha = canvasgroup.alpha == 0f ? 1f : 0f;
}
