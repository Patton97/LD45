using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Camera cam;

    [Header("HUD")]
    [SerializeField] Texture2D crosshairTexture;
    [SerializeField] float crosshairScale = 1;
    [SerializeField] Texture2D hotbarSlot;
    [SerializeField] Texture2D hotbarHighlighted;
    [SerializeField] Vector2 hotbarSlotSize = new Vector2(150, 150);
    [SerializeField] Text prompt;

    private Interactable target;
    private List<Item> inventory = new List<Item>();
    private int currentItem = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHotbar();
        UpdateMovement();
        UpdateLookingAt();
    }

    void UpdateHotbar()
    {
        int newSelection = currentItem;
        //Surely a better way
        if (Input.GetKeyDown(KeyCode.Alpha1)) { newSelection = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { newSelection = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { newSelection = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { newSelection = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { newSelection = 4; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { newSelection = 5; }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { newSelection = 6; }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { newSelection = 7; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { newSelection = 8; }

        //Mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) { newSelection--; }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) { newSelection++; }

        //Validate newSelection before updating currentItem
        //Implements wrap-around for intuitive scroll-wheel usage
        if (newSelection < 0) { newSelection = inventory.Count - 1; }
        if (newSelection >= inventory.Count) { newSelection = 0; }
        currentItem = newSelection;
    }

    //NOTE: Look into the actual input system
    void UpdateMovement()
    {
        Vector3 movementVector = Vector3.zero;

        movementVector += transform.forward * Input.GetAxis("Vertical");
        movementVector += transform.right * Input.GetAxis("Horizontal");
        movementVector.y = 0; //May need to change to rigidbody stuff later if stairs etc are added

        transform.position += movementVector * Time.deltaTime;
        //NOTE: Fix right stick Y
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90, 90);

        transform.eulerAngles = new Vector3(0, yaw);
        cam.transform.eulerAngles = new Vector3(pitch, yaw);
    }

    void UpdateLookingAt()
    {
        //Cast ray from camera 
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        //If raycast hit something
        if (Physics.Raycast(ray, out hit))
        {
            //Assume user looked at an Interactable (if not, set to null)
            SetTargetInteraction(hit.collider.gameObject.GetComponent<Interactable>());
        }

        //RMB
        if (Input.GetMouseButtonDown(0) && target != null) { target.Interact(); }
    }

    void SetTargetInteraction(Interactable newTarget) => target = newTarget;

    public void InventoryAdd(Item item) => inventory.Add(item);
    public void InventoryRemove(Item item) => inventory.Remove(item);
    public Item GetCurrentItem()
    {
        if (inventory.Count == 0) { return null; }
        return inventory[currentItem];
    }

    void OnGUI()
    {
        //GUIDrawCrosshair();
        GUIDrawPrompt();
        GUIDrawHotbar();
    }

    //Draw Crosshair
    Rect crosshairPos;
    void GUIDrawCrosshair()
    {
        float crosshairW = crosshairTexture.width * crosshairScale;
        float crosshairH = crosshairTexture.height * crosshairScale;
        crosshairPos = new Rect((Screen.width - crosshairW) * .5f,
                                (Screen.height - crosshairH) * .5f,
                                crosshairW, crosshairH);
        GUI.DrawTexture(crosshairPos, crosshairTexture);
    }

    //Draw Prompt
    void GUIDrawPrompt()
    {
        if (target == null)
            GameManager.Prompt.SetPromptText("");
        else
            GameManager.Prompt.SetPromptText(target.prompt);
    }

    //Draw Hotbar    
    Vector2 hotbarSlotPos;
    Vector2 hotbarSlotOffset = Vector2.zero;
    float gap = 150;
    void GUIDrawHotbar()
    {
        //Initialise first slot
        hotbarSlotPos = new Vector2((Screen.width / 2) - (gap * 0.5f), Screen.height - hotbarSlotSize.y);
        hotbarSlotOffset = new Vector2((inventory.Count - 1) * gap * 0.5f, 0);

        float i = 0;
        foreach (Item item in inventory)
        {
            //Inventory slot
            GUI.DrawTexture(new Rect(hotbarSlotPos - hotbarSlotOffset, hotbarSlotSize), i == currentItem ? hotbarHighlighted : hotbarSlot);

            //Item sprite
            GUI.DrawTexture(new Rect((hotbarSlotPos - hotbarSlotOffset + hotbarSlotSize * .25f), hotbarSlotSize * 0.5f), item.sprite.texture);

            //Prep next slot
            hotbarSlotOffset.x -= gap;
            i++;
        }
    }
}


