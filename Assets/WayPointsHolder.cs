using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointsHolder : MonoBehaviour 
{
    List<GameObject> WayPoints = new List<GameObject>();

    public GameObject WayPoint1;
    public GameObject WayPoint2;
    public GameObject WayPoint3;
    public GameObject WayPoint4;
    public GameObject WayPoint5;

    public GameObject WayPoint6;
    public GameObject WayPoint7;
    public GameObject WayPoint8;
    public GameObject WayPoint9;
    
    public GameObject WayPoint10;
    public GameObject WayPoint11;
    public GameObject WayPoint12;

	void Start () 
    {
        WayPoints.Add(WayPoint1);
        WayPoints.Add(WayPoint2);
        WayPoints.Add(WayPoint3);
        WayPoints.Add(WayPoint4);
        WayPoints.Add(WayPoint5);

        WayPoints.Add(WayPoint6);
        WayPoints.Add(WayPoint7);
        WayPoints.Add(WayPoint8);
        WayPoints.Add(WayPoint9);

        WayPoints.Add(WayPoint10);
        WayPoints.Add(WayPoint11);
        WayPoints.Add(WayPoint12);
	}
	void Update () 
    {
	
	}
    public List<GameObject> GetWayPoints()
    {
        return WayPoints;
    }
}
