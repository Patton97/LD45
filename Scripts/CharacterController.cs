using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Camera cam;

    [Header("HUD")]
    [SerializeField] Texture2D crosshairTexture;
    [SerializeField] Texture2D hotbarSlot;

    private Rect crosshairPos;

    private List<Item> inventory = new List<Item>();

    private Interactable target;

    private string prompt = "";

    private int currentItem = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        crosshairPos = new Rect((Screen.width  - crosshairTexture.width)  / 2, 
                                (Screen.height - crosshairTexture.height) / 2, 
                                crosshairTexture.width/2, crosshairTexture.height/2);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemSelection();//Terrible terrible god awful name
        UpdateMovement();
        UpdateLookingAt();
    }

    void UpdateItemSelection()
    {
        //Surely a better way
        if (Input.GetKeyDown(KeyCode.Alpha1)) { currentItem = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { currentItem = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { currentItem = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { currentItem = 3; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { currentItem = 4; }                
    }

    void UpdateMovement()
    {
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) { movementVector += transform.forward; }
        if (Input.GetKey(KeyCode.S)) { movementVector += transform.forward * -1; }
        if (Input.GetKey(KeyCode.A)) { movementVector += transform.right * -1; }
        if (Input.GetKey(KeyCode.D)) { movementVector += transform.right; }

        movementVector.y = 0;//Force stable height

        transform.position += movementVector * Time.deltaTime;

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        pitch = Mathf.Clamp(pitch, -90, 90);

        transform.eulerAngles = new Vector3(0, yaw);
        cam.transform.eulerAngles = new Vector3(pitch, yaw);
    }

    void UpdateLookingAt()
    {
        //Store mousepos at click to avoid issues on delays
        Vector2 clickPosition = Input.mousePosition;
        //Cast ray from camera to mouse click pos
        Ray ray = cam.ScreenPointToRay(clickPosition);
        RaycastHit hit;       

        //If raycast hit something
        if(Physics.Raycast(ray, out hit))
        {
            //If user clicked on an Interactable
            if (hit.collider.gameObject.GetComponent<Interactable>())
            {
                target = hit.collider.gameObject.GetComponent<Interactable>();
                target.Hover();
            }
            else if(target !=null)
            {
                target.Unhover();
                target = null;
            }
        }
    }

    public void InventoryAdd(Item item) => inventory.Add(item);
    public void InventoryRemove(Item item) => inventory.Remove(item);

    public void SetPrompt(string newPrompt) => prompt = newPrompt;

    void OnGUI()
    {
        GUI.DrawTexture(crosshairPos, crosshairTexture);

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = 30;
        Rect promptRect = new Rect((Screen.width - crosshairTexture.width) / 2,
                                  (Screen.height - 200 - style.fontSize * 2),
                                  100, 20);//Width,height
        GUI.Label(promptRect, prompt, style);

        //Draw inventory
        Vector2 size = new Vector2(100, 100);
        Vector2 slotPos = new Vector2(Screen.width/2, Screen.height-100);
        float gap = 150;

        //Initialise first slot
        Vector2 offset = new Vector2((inventory.Count-1) * gap * 0.5f, 0);

        float i = 0;
        foreach (Item item in inventory)
        {
            //Highlight selected item slot            
            GUI.color = i == currentItem ? Color.red : Color.white;
            GUI.Box(new Rect(slotPos-offset, size), GUIContent.none);

            //Item sprite
            GUI.color = Color.white;
            GUI.DrawTexture(new Rect(slotPos-offset, size), item.sprite.texture, ScaleMode.ScaleToFit);
            offset.x -= gap;

            i++;
        }
    }
}


