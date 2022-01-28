using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IHitable
{
    public float speed = 5;
    public int id = 0;
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

    [Header("Shooting")]
    public GameObject projectile;
    private bool isShooting = false;

    private Vector3 respawnPosition;

    public int ID => this.id;

    bool gettingHit = false;
    bool picking = false;

    public void Awake()
    {
        this.respawnPosition = this.transform.position;
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

    public void Shoot()
    {
        if(!isShooting)
        {
            isShooting = true;
            this.GetComponent<Animator>().SetTrigger("shoot");
            GameManager.DoActionAfterTime(this, delegate {
                isShooting = false;
            }, 1);
            Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
            Quaternion rotation = Quaternion.LookRotation(this.transform.forward) * projectile.transform.rotation;
            var projec = Instantiate(projectile, this.transform.position + new Vector3(0, 0.25f, 0), rotation);
            projec.GetComponent<BulletBehaviour>().hitter = this.ID;
            projec.GetComponent<Rigidbody>().AddForce(fwd*500, ForceMode.Acceleration);
        }
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
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickable pickable = other.gameObject.GetComponent<IPickable>();
        
        if (pickable != null)
        {
            if(!picking)
            {
                picking = true;
                if( pickable.TryPick(this) )
                    Destroy(other.gameObject);   
                GameManager.DoActionAfterTime(this, delegate {
                    picking = false;
                }, 1);
            }
        }
    }

    private void Respawn()
    {
        this.transform.position = respawnPosition;
    }

    public void GetHit()
    {
        if(!gettingHit)
        {
            gettingHit = true;
            picking = true;
            if( !inventory.DropARandomKey() )
                Respawn();
            GameManager.DoActionAfterTime(this, delegate {
                gettingHit = false;
                picking = false;
            }, 1);
        }
    }
}
