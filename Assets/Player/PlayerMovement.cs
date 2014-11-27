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

    private Vector3 MovementSpeedMax;
    private Vector3 MidAirMovementSpeedMax;

    public int MovementSpeed;
    public int BreakingSpeed;

    public float JumpSpeed;
    public GameObject PlayerGameObject;

    private HUDManager HUD;

    private bool IsJumping = true;
    private bool IsFalling = true;
    private bool IsGrounded = false;

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
        BreakingSpeed = 112;
        MovementSpeed = 8;
        JumpSpeed = 55;
        HUD = GameObject.Find("Camera").GetComponent<HUDManager>();
        EnergyHealthMeter = (EnergyHealthMeter)GameObject.Find("PlayerEnergy").GetComponent<EnergyHealthMeter>();
        PlayerRigidBody = transform.rigidbody.GetComponent<Rigidbody>();
        MovementSpeedMax = PlayerRigidBody.transform.forward * MovementSpeed;
        MidAirMovementSpeedMax = PlayerRigidBody.transform.forward * BreakingSpeed;
        Player = gameObject.GetComponent<Player>();
    }
    void Update()
    {
        if (Input.GetKeyDown("up") && IsGrounded)
        {
            Jump();
        }
        else if (Input.GetKey("left") && !IsJumping && !IsFalling)
        {
            Run(LEFT);
        }
        else if (Input.GetKey("right") && !IsJumping && !IsFalling)
        {
            Run(RIGHT);
        }
        /*if (Input.GetKey("left") && (IsJumping || IsFalling))
        {
            RunMidAir(LEFT);
        }
        else if (Input.GetKey("right") && (IsJumping || IsFalling))
        {
            RunMidAir(RIGHT);
        }*/
        else if(Input.GetKeyUp("left") && !IsJumping && !IsFalling)
        {
            PlayerRigidBody.AddForce(Vector3.right * BreakingSpeed);
            gameObject.animation.Play("loop_idle");
        }
        else if (Input.GetKeyUp("right") && !IsJumping && !IsFalling)
        {
            PlayerRigidBody.AddForce(Vector3.left * BreakingSpeed);
            gameObject.animation.Play("loop_idle");
        }
        else if (Input.GetKey("q") && WithinSynapseRangeForDamageOrRepair)
        {
            if (HUD.IsCurePlayerSelected())
            {
                if (GetSynapseBehavior().GetSynapseHealth() != 100)
                {
                    gameObject.animation.Play("punch_hi_left");
                    GetSynapseBehavior().Repair(1);
                    EnergyHealthMeter.UseEnergy(1);
                }
            }
            else if (HUD.IsDiseasePlayerSelected())
            {
                if (GetSynapseBehavior().GetSynapseHealth() != 0)
                {
                    gameObject.animation.Play("cmb_street_fight");
                    GetSynapseBehavior().Damage(1);
                    EnergyHealthMeter.UseEnergy(1);
                }
            }
        }
        else if(Input.GetKeyDown("e") && WithinDopamineSacRangeForReleaseOrBurst)
        {
            if(HUD.IsCurePlayerSelected())
            {
                gameObject.animation.Play("kick_jump_right");
                GetDopamineSacBehavior().Release();
            }
            else if(HUD.IsDiseasePlayerSelected())
            {
                gameObject.animation.Play("kick_jump_right");
                GetDopamineSacBehavior().Burst();
            }
        }
        else if(Input.GetKey("q") && WithinRechargeStationRangeForReplenishing)
        {
            if (HUD.IsCurePlayerSelected() || HUD.IsDiseasePlayerSelected())
            {
                gameObject.animation.Play("punch_hi_left");
                GetRechargeStationBehavior().Recharge();
            }
        }
        if(Input.GetKeyDown("space"))
        {
            GameObject InstantiateBullet;
            Vector3 BulletSpawn;
            if(LastPlayerDirection == RIGHT)
            {
                if (EnergyHealthMeter.GetEnergyHealth() > 0.0f)
                {
                    BulletSpawn = new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z);
                    InstantiateBullet = (GameObject)Instantiate(EnergyBullet, BulletSpawn, Quaternion.identity);
                    InstantiateBullet.rigidbody.AddForce(EnergyBullet.transform.right * 1500);
                    EnergyHealthMeter.UseEnergy(10);
                }

            }
            else if(LastPlayerDirection == LEFT)
            {
                if(EnergyHealthMeter.GetEnergyHealth() > 0.0f)
                {
                    BulletSpawn = new Vector3(transform.position.x - 1f, transform.position.y + 1f, transform.position.z);
                    InstantiateBullet = (GameObject)Instantiate(EnergyBullet, BulletSpawn, Quaternion.identity);
                    InstantiateBullet.rigidbody.AddForce(-EnergyBullet.transform.right * 1500);
                    EnergyHealthMeter.UseEnergy(10);
                }
            }
        }
    }
    void OnCollisionEnter(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("LevelGround"))
        {
            IsFalling = false;
            IsGrounded = true;
            IsJumping = false;
        }
        else if (Collision.gameObject.name.Contains("RechargeStation") || Collision.gameObject.name.Contains("DopamineSac"))
        {
            IsFalling = false;
            IsJumping = false;
        }
        else if (Collision.gameObject.name.Contains("Synapse"))
        {
            SynapseBehavior SynapseBehavior = Collision.gameObject.GetComponent<SynapseBehavior>();
            if (HUD.IsCurePlayerSelected() && SynapseBehavior.GetSynapseType() == 0)
            {
                if(SynapseBehavior.GetSynapseHealth() < 50)
                {
                    if (Player.GetLives() > 0)
                    {
                        Player.SetLives(Player.GetLives() - 1);
                    }
                }
            }
            else if(HUD.IsDiseasePlayerSelected() && SynapseBehavior.GetSynapseType() == 1)
            {
                if (SynapseBehavior.GetSynapseHealth() > 50)
                {
                    if (Player.GetLives() > 0)
                    {
                        Player.SetLives(Player.GetLives() - 1);
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider Collider)
    {
        if(Collider.gameObject.name.Contains("DeathTrigger"))
        {
            Debug.Log("Hitting Death Trigger");
            if (Player.GetLives() > 0)
            {
                Player.SetLives(Player.GetLives() - 1);
                Debug.Log("Respawning Player with Intact Stats");
                Player.RespawnPlayer();
            }
        }
    }
    void OnCollisionExit(Collision Collision)
    {
        if (Collision.gameObject.name.Contains("LevelGround") && !IsJumping)
        {
            IsFalling = true;
            gameObject.animation.Stop("loop_run_funny");
            if (LastPlayerDirection == RIGHT)
            {
                PlayerRigidBody.AddForce(new Vector3(3.9f, 0, 0) * JumpSpeed);
            }
            else if (LastPlayerDirection == LEFT)
            {
                PlayerRigidBody.AddForce(new Vector3(-3.9f, 0, 0) * JumpSpeed);
            }
        }
        else if (Collision.gameObject.name.Contains("RechargeStation") && !IsJumping && !IsGrounded)
        {
            IsFalling = true;
            gameObject.animation.Stop("loop_run_funny");
        }
        else if (Collision.gameObject.name.Contains("DopamineSac") && !IsJumping && !IsGrounded)
        {
            IsFalling = true;
            gameObject.animation.Stop("loop_run_funny");
        }
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
        gameObject.animation.Play("jump");
        PlayerRigidBody.AddForce(new Vector3(0, 9.5f, 0) * JumpSpeed);
        if(LastPlayerDirection == RIGHT)
        {
            PlayerRigidBody.AddForce(new Vector3(3.9f, 0, 0) * JumpSpeed);
        }
        else if(LastPlayerDirection == LEFT)
        {
            PlayerRigidBody.AddForce(new Vector3(-3.9f, 0, 0) * JumpSpeed);
        }
        IsJumping = true;
        IsFalling = true;
        IsGrounded = false;
    }
    protected void Run(int Direction)
    {
        PlayerRigidBody.velocity = MovementSpeedMax; // MovementSpeedMaxText is a Vector3
        if (Direction == LEFT)
        {
            // make sure the game object is facing left
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.animation.Play("loop_run_funny", PlayMode.StopAll);

            float moveLeft = MovementSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * moveLeft);

            //PlayerRigidBody.AddForce(Vector3.left * MovementSpeed);
            LastPlayerDirection = LEFT;
        }
        else if (Direction == RIGHT)
        {
            // make sure the game object is facing right
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.animation.Play("loop_run_funny", PlayMode.StopAll);

            float moveRight = MovementSpeed * Time.smoothDeltaTime * Input.GetAxis("Horizontal");
            transform.Translate(Vector3.left * moveRight);

            //PlayerRigidBody.AddForce(Vector3.right * MovementSpeed);
            LastPlayerDirection = RIGHT;
        }
        ShootRayCast(Direction);
    }
    private void RunMidAir(int Direction)
    {
        PlayerRigidBody.velocity = MidAirMovementSpeedMax; // MovementSpeedMaxText is a Vector3
        if (Direction == LEFT)
        {
            // make sure the game object is facing left
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.animation.Play("loop_run_funny", PlayMode.StopAll);
            PlayerRigidBody.AddForce(Vector3.left * BreakingSpeed);
            LastPlayerDirection = LEFT;
        }
        else if (Direction == RIGHT)
        {
            // make sure the game object is facing right
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.animation.Play("loop_run_funny", PlayMode.StopAll);
            PlayerRigidBody.AddForce(Vector3.right * BreakingSpeed);
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
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.left, out Hit, 10.0F))
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
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.right, out Hit, 10.0F))
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