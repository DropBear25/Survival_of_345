using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AgentAI : MonoBehaviour
{
    //might need to rework 
    public AI_WayPoint_Network WayPointNetwork = null;
    public int CurrentIndex = 0;
    public bool HasPath = false;
    public bool PathPending = false;
    public bool PathStale = false;
    public NavMeshPathStatus PathStatus = NavMeshPathStatus.PathInvalid;
    private NavMeshAgent _navAgent = null;


    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();

        if (WayPointNetwork == null) return;

        SetNextDestination(false);
    }


    void SetNextDestination(bool increment)
    {
        if (!WayPointNetwork) return;

        int incStep = increment ? 1 : 0;
        Transform nextWaypointTransform = null;
     

            int nextWayPoint = (CurrentIndex +incStep >= WayPointNetwork.Waypoints.Count) ?0:CurrentIndex + incStep;
            nextWaypointTransform = WayPointNetwork.Waypoints[nextWayPoint];

            if (nextWaypointTransform!=null)
            {
                CurrentIndex = nextWayPoint;
                _navAgent.destination = nextWaypointTransform.position;
                return;
            }
        

        CurrentIndex=nextWayPoint;

    }

    void Update()
    {
        HasPath = _navAgent.hasPath;
        PathPending = _navAgent.pathPending;
        PathStale = _navAgent.isPathStale;
        PathStatus = _navAgent.pathStatus;


        if ((_navAgent.remainingDistance <=_navAgent.stoppingDistance && !PathPending) || PathStatus==NavMeshPathStatus.PathInvalid || PathStatus == NavMeshPathStatus.PathPartial)
        {
            SetNextDestination(true);
        }


        if((!HasPath && !PathPending) || PathStatus==NavMeshPathStatus.PathInvalid || PathStatus==NavMeshPathStatus.PathPartial)
            SetNextDestination(true);
        else if (_navAgent.isPathStale)
            SetNextDestination(false);
    }
}