using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PathDisplayMode { None, Connections, Paths }


public class AI_WayPoint_Network : MonoBehaviour
{
    public PathDisplayMode DisplayMode = PathDisplayMode.Connections;
    public int UIstart = 0;
    public int UIEnd = 0;
    public List<Transform> Waypoints = new List<Transform>();



 
}
