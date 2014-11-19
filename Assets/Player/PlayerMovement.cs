using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameObject EnergyBullet;

    private int LastPlayerDirection;
    private Player Player;
    private Rigidbody PlayerRigidBody;

    private const int LEFT = 0;
    private const int RIGHT = 1;
    private const int DEATH_DISTANCE = -25;

    public float MovementSpeedMax;
    private Vector3 MovementSpeedMaxTest;
    public int MovementSpeed; 

    public float JumpSpeed;
    public GameObject PlayerGameObject;
    public GameObject WeaponMace;

    private HUDManager HUD;

    public bool IsJumping = false;
    public bool IsFalling = false;
    public bool IsGrounded = true;

    private bool WithinSynapseRangeForDamageOrRepair = false;
    private bool WithinDopamineSacRangeForReleaseOrBurst = false;
    private bool WithinRechargeStationRangeForReplenishing = false;
    
    private SynapseBehavior SynapseBehavior;
    private DopamineSacBehavior DopmaineSacBehavior;
    private RechargeStationBehavior RechargeStationBehavior;

    private EnergyHealthMeter EnergyHealthMeter;

    public int GetPlayerDirection()
    {
        return LastPlayerDirection;
    }
    void Start()
    {
        MovementSpeed = 225;
        JumpSpeed = 60;
        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();
        EnergyHealthMeter = (EnergyHealthMeter)GameObject.Find("PlayerEnergy").GetComponent<EnergyHealthMeter>();
        PlayerRigidBody = transform.rigidbody.GetComponent<Rigidbody>();
        MovementSpeedMaxTest = PlayerRigidBody.transform.forward * MovementSpeed;
        Player = gameObject.GetComponent<Player>();
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
        else if (Input.GetKey("space") && WithinSynapseRangeForDamageOrRepair)
        {
            if (HUD.IsCurePlayerSelected())
            {
                if (GetSynapseBehavior().GetSynapseHealth() != 100)
                {
                    GetSynapseBehavior().Repair(1);
                    EnergyHealthMeter.UseEnergy(1);
                }
            }
            else if (HUD.IsDiseasePlayerSelected())
            {
                if (GetSynapseBehavior().GetSynapseHealth() != 0)
                {
                    GetSynapseBehavior().Damage(1);
                    EnergyHealthMeter.UseEnergy(1);
                }
            }
        }
        else if(Input.GetKeyDown("return") && WithinDopamineSacRangeForReleaseOrBurst)
        {
            if(HUD.IsCurePlayerSelected())
            {
                GetDopamineSacBehavior().Release();
            }
            else if(HUD.IsDiseasePlayerSelected())
            {
                GetDopamineSacBehavior().Burst();
            }
        }
        else if(Input.GetKey("z") && WithinRechargeStationRangeForReplenishing)
        {
            if (HUD.IsCurePlayerSelected() || HUD.IsDiseasePlayerSelected())
            {
                GetRechargeStationBehavior().Recharge();
            }
        }
        if(Input.GetKeyDown("x"))
        {
            GameObject InstantiateBullet;
            Vector3 BulletSpawn;
            if(LastPlayerDirection == RIGHT)
            {
                if (EnergyHealthMeter.GetEnergyHealth() > 0.0f)
                {
                    BulletSpawn = new Vector3(transform.position.x + 1, transform.position.y + 0.5f, transform.position.z);
                    InstantiateBullet = (GameObject)Instantiate(EnergyBullet, BulletSpawn, Quaternion.identity);
                    InstantiateBullet.rigidbody.AddForce(EnergyBullet.transform.right * 1500);
                    EnergyHealthMeter.UseEnergy(10);
                }

            }
            else if(LastPlayerDirection == LEFT)
            {
                if(EnergyHealthMeter.GetEnergyHealth() > 0.0f)
                {
                    BulletSpawn = new Vector3(transform.position.x - 1, transform.position.y + 0.5f, transform.position.z);
                    InstantiateBullet = (GameObject)Instantiate(EnergyBullet, BulletSpawn, Quaternion.identity);
                    InstantiateBullet.rigidbody.AddForce(-EnergyBullet.transform.right * 1500);
                    EnergyHealthMeter.UseEnergy(10);
                }
            }
        }
    }
    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("LevelGround") || Collision.gameObject.name.Contains("RechargeStation"))
        {
            IsFalling = false;
            IsGrounded = true;
            IsJumping = false;
        }
        else if (Collision.gameObject.name.Contains("Synapse"))
        {
            SynapseBehavior SynapseBehavior = Collision.gameObject.GetComponent<SynapseBehavior>();
            if (HUD.IsCurePlayerSelected() && SynapseBehavior.GetSynapseType() == 0 || HUD.IsDiseasePlayerSelected() && SynapseBehavior.GetSynapseType() == 1)
            {
                if (Player.GetLives() > 0)
                {
                    Player.SetLives(Player.GetLives() - 1);
                }
                HUD.UpdateLives();
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
    public void SetRechargeStationBehavior(RechargeStationBehavior RechargeStation)
    {
        RechargeStationBehavior = RechargeStation;
    }
    public RechargeStationBehavior GetRechargeStationBehavior()
    {
        return RechargeStationBehavior;
    }
    public void SetSynapseBehavior(SynapseBehavior Synapse)
    {
        SynapseBehavior = Synapse;
    }
    public SynapseBehavior GetSynapseBehavior()
    {
        return SynapseBehavior;
    }
    public void SetDopamineSacBehavior(DopamineSacBehavior Dopamine)
    {
        DopmaineSacBehavior = Dopamine;
    }
    public DopamineSacBehavior GetDopamineSacBehavior()
    {
        return DopmaineSacBehavior;
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
                    if (Hit.collider.name.Contains("Synapse"))
                    {
                        if (Hit.distance <= 4.0f)
                        {
                            WithinSynapseRangeForDamageOrRepair = true;
                            SetSynapseBehavior((SynapseBehavior)Hit.collider.gameObject.GetComponent<SynapseBehavior>());
                        }
                        else if (Hit.distance > 4.0f)
                        {
                            WithinSynapseRangeForDamageOrRepair = false;
                        }
                    }
                    else if(Hit.collider.name.Contains("DopamineSac"))
                    {
                        if (Hit.distance <= 6.0f)
                        {
                            WithinDopamineSacRangeForReleaseOrBurst = true;
                            SetDopamineSacBehavior((DopamineSacBehavior)Hit.collider.gameObject.GetComponent<DopamineSacBehavior>());
                        }
                        else if (Hit.distance > 6.0f)
                        {
                            WithinDopamineSacRangeForReleaseOrBurst = false;
                        }
                    }
                    else if (Hit.collider.name.Contains("RechargeStation"))
                    {
                        if (Hit.distance <= 4.0f)
                        {
                            WithinRechargeStationRangeForReplenishing = true;
                            SetRechargeStationBehavior((RechargeStationBehavior)Hit.collider.gameObject.GetComponent<RechargeStationBehavior>());
                        }
                        else if (Hit.distance > 4.0f)
                        {
                            WithinRechargeStationRangeForReplenishing = false;
                        }
                    }
                }
                else
                {
                    WithinSynapseRangeForDamageOrRepair = false;
                    WithinDopamineSacRangeForReleaseOrBurst = false;
                    WithinRechargeStationRangeForReplenishing = false;
                }
                break;
            case RIGHT:
                if (Physics.Raycast(transform.position, Vector3.right, out Hit, 5.0F))
                {
                    if (Hit.collider.name.Contains("Synapse"))
                    {
                        if (Hit.distance <= 4.0f)
                        {
                            //Debug.Log("Within Range @" + Hit.distance);
                            WithinSynapseRangeForDamageOrRepair = true;
                            SetSynapseBehavior((SynapseBehavior)Hit.collider.gameObject.GetComponent<SynapseBehavior>());
                        }
                        else if (Hit.distance > 4.0f)
                        {
                            WithinSynapseRangeForDamageOrRepair = false;
                        }
                    }
                    else if (Hit.collider.name.Contains("DopamineSac"))
                    {
                        if (Hit.distance <= 4.0f)
                        {
                            WithinDopamineSacRangeForReleaseOrBurst = true;
                            SetDopamineSacBehavior((DopamineSacBehavior)Hit.collider.gameObject.GetComponent<DopamineSacBehavior>());
                        }
                        else if (Hit.distance > 4.0f)
                        {
                            WithinDopamineSacRangeForReleaseOrBurst = false;
                        }
                    }
                    else if (Hit.collider.name.Contains("RechargeStation"))
                    {
                        if (Hit.distance <= 4.0f)
                        {
                            WithinRechargeStationRangeForReplenishing = true;
                            SetRechargeStationBehavior((RechargeStationBehavior)Hit.collider.gameObject.GetComponent<RechargeStationBehavior>());
                        }
                        else if (Hit.distance > 4.0f)
                        {
                            WithinRechargeStationRangeForReplenishing = false;
                        }
                    }
                }
                else
                {
                    WithinDopamineSacRangeForReleaseOrBurst = false;
                    WithinSynapseRangeForDamageOrRepair = false;
                    WithinRechargeStationRangeForReplenishing = false;
                }
                break;
        }
    }
}