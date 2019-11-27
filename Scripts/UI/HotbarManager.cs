using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{

    [SerializeField] RectTransform container;
    [SerializeField] Sprite slotDefault, slotSelected;
    CanvasGroup canvasgroup;
    List<GameObject> slots = new List<GameObject>();
    int selectedSlot = 0;

    #region singletonpattern    
    public static HotbarManager instance { get; private set; }
    private void SetInstance()
    {
        if (instance == null) { instance = this; }
    }
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        SetInstance();
        canvasgroup = container.GetComponent<CanvasGroup>();
        
    }

    // Update is called once per frame
    void Update() { /*Ideally, don't use update for UI*/ }

    void ForceUpdate()
    {
        //Highlight selected slot
        foreach(GameObject slot in slots)
        {
            slot.GetComponent<Image>().sprite = slotDefault;
        }
        slots[selectedSlot].GetComponent<Image>().sprite = slotSelected;

        //Stupid unity canvas required stuff
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(container);
    }

    //Manage hotbar contents
    public void AddItem(Item item) => AddSlot(item.sprite);
    public void RemoveItem(int index) => RemoveSlot(index);
    public void RemoveAll() => slots.Clear();

    static void AddSlot(Sprite itemSprite)
    {
        //Slot (background)
        GameObject slot = new GameObject();
        slot.AddComponent<Image>().sprite = slotDefault;
        slot.GetComponent<RectTransform>().SetParent(container.transform);
        slot.SetActive(true);

        //Item
        GameObject item = new GameObject();
        item.AddComponent<Image>().sprite = itemSprite;
        item.GetComponent<RectTransform>().SetParent(slot.transform);
        item.SetActive(true);

        slots.Add(slot);

        ForceUpdate();
    }

    static void RemoveSlot(int index)
    {
        Destroy(slots[index]); 
        slots.RemoveAt(index); //Remove remnants (null value)
        ForceUpdate();
    }

    public static int GetSelected() => selectedSlot;
    public static void SetSelected(int newSelection)
    {
        //Validate newSelection before updating currentItem
        //Implements wrap-around for intuitive scroll-wheel usage
        if (newSelection < 0) { newSelection = slots.Count - 1; }
        if (newSelection >= slots.Count) { newSelection = 0; }
        
        selectedSlot = newSelection;
    }


    //Toggle visibility
    public static void Show() => canvasgroup.alpha = 1f;
    public static void Hide() => canvasgroup.alpha = 0f;
    public static void Toggle() => canvasgroup.alpha = canvasgroup.alpha == 0f ? 1f : 0f;
}
