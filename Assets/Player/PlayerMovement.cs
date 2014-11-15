using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private int LastPlayerDirection;
    private Player Player;
    private Rigidbody PlayerRigidBody;

    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int DEATH_DISTANCE = -25;

    public float MovementSpeedMax;
    private Vector3 MovementSpeedMaxTest;
    public int MovementSpeed; // 250

    public float JumpSpeed = 20;
    public GameObject PlayerGameObject;
    public GameObject WeaponMace;

    private HUDManager HUD;

    public bool IsJumping = false;
    public bool IsFalling = false;
    public bool IsGrounded = true;

    private bool IsEarthPickedUp = false;
    private bool IsFirePickedUp = false;

    public int GetPlayerDirection()
    {
        return LastPlayerDirection;
    }
    void Start()
    {
        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();
        PlayerRigidBody = transform.rigidbody.GetComponent<Rigidbody>();
        MovementSpeedMaxTest = PlayerRigidBody.transform.forward * MovementSpeed;
        Player = gameObject.GetComponent<Player>();
        Debug.Log("Player name in PlayerMovement:" + gameObject.name);
    }
    void FixedUpdate()
    {
        if (PlayerRigidBody.transform.position.y < DEATH_DISTANCE)
        {
            HUD.OnScreenDebugLine("Player Should totally be dead right now");
        }
        if (Input.GetKeyDown("up") && IsGrounded)
        {
            Jump();
        }
        else if (Input.GetKey("left") && !IsJumping && !IsFalling)
        {
            Debug.DrawRay(transform.position, Vector3.left, Color.red, 1);
            Run(LEFT);
        }
        else if (Input.GetKey("right") && !IsJumping && !IsFalling)
        {
            Debug.DrawRay(transform.position, Vector3.right, Color.red, 1);
            Run(RIGHT);
        }
    }
    void OnCollisionExit(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("LevelGround") && !IsJumping) 
        {
            //Debug.Log("Exit Level Ground");
            IsFalling = true;
        }
       
        /*if(Collision.gameObject.name.Contains("Earth_Block") && IsGrounded)
        {
            //Debug.Log("Exit Earth Block & Grounded");
            IsGrounded = true;
            IsFalling = false;
            IsJumping = false;
        }
        if (Collision.gameObject.name.Contains("Earth_Block") && !IsJumping)
        {
            //Debug.Log("Exit Earth Block & !Jumping");
            IsFalling = true;
        }*/
    }
    void OnCollisionEnter(Collision Collision)
    {
        PickUp Potion;
        PlayerActions PlayerActions = (PlayerActions)Player.GetComponent(typeof(PlayerActions));

        if (Collision.gameObject.name.Contains("LevelGround")) 
        {
            //Debug.Log("Collision level Ground");
            IsFalling = false;
            IsGrounded = true;
            IsJumping = false;
        }
        else if (Collision.gameObject.name.Contains("ManaPotion"))
        {
            Potion = GameObject.Find("ManaPotion").GetComponent<PickUp>();
            PlayerActions.PickUpMana(Potion);
        }
        else if(Collision.gameObject.name.Contains("Synapse"))
        {
            // Determine player type
            SynapseBehavior SynapseBehavior = Collision.gameObject.GetComponent<SynapseBehavior>();
            if(HUD.IsCurePlayerSelected() && SynapseBehavior.SynapseType == 0 || HUD.IsDiseasePlayerSelected() && SynapseBehavior.SynapseType == 1)
            {
                Debug.Log(" On Collision Synapse Condition : Player Should die: ");
                Player.SetLives(0);
                Player.SetMana(0);
                HUD.UpdateLives();
                HUD.UpdateMana();
            }
       
        }
    }
    protected void Jump()
    {
        PlayerRigidBody.AddForce(new Vector3(0, 10, 0) * JumpSpeed);
        IsJumping = true;
        IsFalling = true;
        IsGrounded = false;
    }
    protected void Run(int Direction)
    {
        PlayerRigidBody.velocity = MovementSpeedMaxTest; // MovementSpeedMaxText is a Vector3
        if (Direction == LEFT)
        {
            PlayerRigidBody.AddForce(Vector3.left * MovementSpeed);
            LastPlayerDirection = LEFT;
        }
        else if (Direction == RIGHT)
        {
            PlayerRigidBody.AddForce(Vector3.right * MovementSpeed);
            LastPlayerDirection = RIGHT;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 100.0F))
            {
                Debug.Log("Ray Hit: " + hit.collider.name);
                float Distance = hit.distance;
                Debug.Log("Distance: " + Distance);
            }
        }
    }
}