using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public ObjectType type;
    public List<Material> playerMaterial = new List<Material>();
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Inventory inventory;

    private bool alreadyToggle = false;

    public bool doingAction = false;

    [Header("Clips")]
    public AnimationClip plantClip;

    [Header("Movement")]
    [SerializeField] Vector2 input;
    float heading = 0.0f;
    private Rigidbody rb;
    private string hValue;
    private string vValue;
    public Transform camPivot;

    public void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();

        if(type == ObjectType.Black) {
            hValue = "Horizontal_1";
            vValue = "Vertical_1";
        }
        if(type == ObjectType.White) {
            hValue = "Horizontal_2";
            vValue = "Vertical_2";
        }

        this.skinnedMeshRenderer.materials[1].color =  (type == ObjectType.Black) ? Color.black : Color.white;
        this.inventory.player = this;
    }

    public void FixedUpdate()
    {
        if(!doingAction)
        {
            HandleMovement();
            HandleAction();
        }
        HandleToggle();
    }

    public void TriggerDrop()
    {
        this.GetComponent<Animator>().SetTrigger("plant");
    }

    void HandleToggle()
    {
        if (alreadyToggle)
        {
            switch (type)
            {
                case ObjectType.Black: alreadyToggle = Input.GetButton("Toggle_1"); break;
                case ObjectType.White: alreadyToggle = Input.GetButton("Toggle_2"); break;
            }
        }
        else
        {
            bool toggle = false;
            switch (type)
            {
                case ObjectType.Black: toggle = Input.GetButton("Toggle_1"); break;
                case ObjectType.White: toggle = Input.GetButton("Toggle_2"); break;
            }

            if (toggle)
            {
                inventory.TogglePositive();
                alreadyToggle = true;
            }
        }
    }

    void HandleAction()
    {
        switch (type)
        {
            case ObjectType.Black:
            if( Input.GetButton("Action_1") )
                inventory.IndexAction();
            break;
            case ObjectType.White:
            if( Input.GetButton("Action_2") )
                inventory.IndexAction();
            break;
        }
    }

    void HandleMovement()
    {
        heading += Input.GetAxis(hValue)*Time.fixedDeltaTime*180;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);
        this.input = new Vector2(Input.GetAxis(hValue), Input.GetAxis(vValue));
        this.input = Vector2.ClampMagnitude(input, 1);

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized;
        this.GetComponent<Animator>().SetFloat("speed", movement.magnitude);
        
        if(movement == Vector3.zero)
            return;
        

        transform.rotation = Quaternion.LookRotation(movement);
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickable pickable = other.gameObject.GetComponent<IPickable>();
        if (pickable != null)
            if( pickable.TryPick(this) )
                Destroy(other.gameObject);
    }
}
