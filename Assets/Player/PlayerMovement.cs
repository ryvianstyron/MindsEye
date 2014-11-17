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

    public float JumpSpeed;
    public GameObject PlayerGameObject;
    public GameObject WeaponMace;

    private HUDManager HUD;

    public bool IsJumping = false;
    public bool IsFalling = false;
    public bool IsGrounded = true;

    private bool WithinSynapseRangeForDamageOrRepair = false;
    private SynapseBehavior SynapseBehavior;

    public int GetPlayerDirection()
    {
        return LastPlayerDirection;
    }
    void Start()
    {
        JumpSpeed = 60;
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
        else if(Input.GetKeyDown("space") && WithinSynapseRangeForDamageOrRepair)
        {
            if (HUD.IsCurePlayerSelected())
            {
                GetSynapseBehavior().Repair(1);
            }
            else if(HUD.IsDiseasePlayerSelected())
            {
                GetSynapseBehavior().Damage(1);
            }
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
    public void SetSynapseBehavior(SynapseBehavior Synapse)
    {
        Debug.Log("Set Synapse Behavior");
        SynapseBehavior = Synapse;
    }
    public SynapseBehavior GetSynapseBehavior()
    {
        return SynapseBehavior;
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
        }
        ShootRayCast(Direction);
    }
    public void ShootRayCast(int Direction)
    {
        RaycastHit Hit;
        switch(Direction)
        {
            case LEFT:
                if (Physics.Raycast(transform.position, Vector3.left, out Hit, 5.0F))
                {
                    //Distance = Hit.distance;
                    if (Hit.collider.name.Contains("Synapse"))
                    {
                        if (Hit.distance <= 4.0f)
                        {
                            Debug.Log("Within Range @" + Hit.distance);
                            WithinSynapseRangeForDamageOrRepair = true;
                            SetSynapseBehavior((SynapseBehavior)Hit.collider.gameObject.GetComponent<SynapseBehavior>());
                        }
                        else if (Hit.distance > 4.0f)
                        {
                            WithinSynapseRangeForDamageOrRepair = false;
                        }

                    }
                }
                else WithinSynapseRangeForDamageOrRepair = false;
                break;
            case RIGHT:
                if (Physics.Raycast(transform.position, Vector3.right, out Hit, 5.0F))
                {
                    if (Hit.collider.name.Contains("Synapse"))
                    {
                        if (Hit.distance <= 4.0f)
                        {
                            Debug.Log("Within Range @" + Hit.distance);
                            WithinSynapseRangeForDamageOrRepair = true;
                            SetSynapseBehavior((SynapseBehavior)Hit.collider.gameObject.GetComponent<SynapseBehavior>());
                        }
                        else if (Hit.distance > 4.0f)
                        {
                            WithinSynapseRangeForDamageOrRepair = false;
                        }
                    }
                }
                else WithinSynapseRangeForDamageOrRepair = false;
                break;
        }
    }
}