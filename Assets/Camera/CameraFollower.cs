using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
    public GameObject Disease;
    public GameObject Cure;

    Transform Target;

    public float Distance = 3.0f;
    public float Height = 3.0f;
    public float Damping = 5.0f;
    public float RotationDamping = 10.0f;
    
    public bool SmoothRotation = true;
    public bool LockRotation;

    private bool CameraIsSetUp = false;
    private HUDManager HUD;

    void Start() 
    {
        HUD = (HUDManager)GameObject.Find("Camera").GetComponent(typeof(HUDManager));
    }
       
    public void SetUpCamera(GameObject Player)
    {
        if (HUD.IsCurePlayerSelected())
        {
            Target = Player.transform;
        }
        else if (HUD.IsDiseasePlayerSelected())
        {
            Target = Player.transform;
        }
        CameraIsSetUp = true;
    }
    void Update()
    {
        if(Target && CameraIsSetUp)
        {
            Vector3 WantedPosition = Target.TransformPoint(0, Height, -Distance);
            transform.position = Vector3.Lerp(transform.position, WantedPosition, Time.deltaTime * Damping);
            if (SmoothRotation)
            {
                Quaternion WantedRotation = Quaternion.LookRotation(Target.position - transform.position, Target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, WantedRotation, Time.deltaTime * RotationDamping);
            }
            else transform.LookAt(Target, Target.up);

            if (LockRotation)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
