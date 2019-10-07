using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OVER ENGINEERED TO FUCK 

//For purposes of keeping the collision trigger stationary, 
//this script is added to the hinge rather than the door itself
//NOTE: Could rename this to script.cs but atm seems weird
[ExecuteInEditMode]
public class Door : Interactable
{
    private enum DoorDirection : short { CLOCKWISE = 1, ANTI_CLOCKWISE = -1 };
    private enum DoorState : short { CLOSED = -1, MOVING, OPEN }
    private enum SwingState : short { CLOSING = -1, NONE, OPENING }

#pragma warning disable 649 //Hide redundant warnings about SerializedFields not being assigned to
    [Header("Requirements")]
    [SerializeField] GameObject door; //The moving door object - should be a child of this component's parent (an empty object)
#pragma warning restore 649 //Unhide warning

    [Header("Default State")]
    [SerializeField] DoorDirection myDirection = DoorDirection.CLOCKWISE;
    [SerializeField] DoorState doorState = DoorState.CLOSED;
    private SwingState swingState = SwingState.NONE;

    [Header("Swing Customisation")]
    [SerializeField] bool autoOpen = false; //Does the door automatically open when a player walks up to it?
    [Range(0, 1000)]
    [SerializeField] float speed = 100;
    [Range(0, 360)]
    [SerializeField] float maxAngle = 90; //Angles at which the door is fully closed or open, respectively    

    //ContextMenu allows functions to be called from inspector
    [ContextMenu("Open")] void CM_Open() => Open();
    [ContextMenu("Close")] void CM_Close() => Close();

    [SerializeField] public bool locked = false;
    private bool open = false;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        UpdatePrompt();
    }

    public override void Interact()
    {
        if (!locked)
        {
            if(open)
            {
                Close();
                open = false;
            }
            else
            {
                Open();
                open = true;
            }
        }

        UpdatePrompt();
    }

    public void UpdatePrompt()
    {
        if (locked)
        {
            prompt = "Locked";
        }
        else if (open)
        {
            prompt = "Close door";
        }
        else
        {
            prompt = "Open door";
        }
    }

    //Every phys tick
    void FixedUpdate()
    {
        //If state is neutral, do nothing
        if (swingState != SwingState.NONE)
        {
            //Update rotation
            Vector3 axis = Vector3.up * (short)myDirection;
            float angle = speed * (short)swingState * Time.fixedDeltaTime;
            door.transform.RotateAround(transform.position, axis, angle);

            //Grab new Y value
            float currentAngle = door.transform.localRotation.eulerAngles.y;

            //If rotation is completed
            //This comparison works for both open & close because Unity's angles wrap (359<->0)
            if ((myDirection == DoorDirection.CLOCKWISE && currentAngle > maxAngle)
            || (myDirection == DoorDirection.ANTI_CLOCKWISE && currentAngle < maxAngle))
            {
                //Set door to a completed state
                doorState = (DoorState)swingState;
                swingState = SwingState.NONE;

                //Snap door to absolute correct angle for end of current rotation
                float snapX = door.transform.localRotation.eulerAngles.x;
                float snapY = 0;
                float snapZ = door.transform.localRotation.eulerAngles.z;

                if (doorState == DoorState.OPEN) { snapY = maxAngle; }
                if (doorState == DoorState.CLOSED) { snapY = 0; }

                door.transform.localRotation.eulerAngles.Set(snapX, snapY, snapZ);
            }
            else
            {
                //Debug.Log(currentAngle);
            }
        }


    }

    //Decide if door should be opened or closed
    //Concentrates external interaction to one function
    [ContextMenu("Use Door")]
    public void Use()
    {
        if (doorState == DoorState.OPEN) { Close(); }
        if (doorState == DoorState.CLOSED) { Open(); }
        //If doorState == MOVING, ignore interaction.
        //This could instead reverse current movement
    }

    //Shorthand EBDs
    public void Open() => swingState = SwingState.OPENING;
    public void Close() => swingState = SwingState.CLOSING;

    //DEBUG ONLY: Draw gizmo to better visualise hinge position(s)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f); //Red (Transparent)
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}

