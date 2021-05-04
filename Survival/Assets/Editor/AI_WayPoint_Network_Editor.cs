using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(AI_WayPoint_Network))]
public class AI_WayPoint_Network_Editor : Editor
{

    public override void OnInspectorGUI()
    {
        AI_WayPoint_Network network = (AI_WayPoint_Network)target;

      
        network.DisplayMode = (PathDisplayMode)EditorGUILayout.EnumPopup( "Display Mode", network.DisplayMode );

        if (network.DisplayMode == PathDisplayMode.Paths)
        {
            network.UIstart = EditorGUILayout.IntSlider("Waypoint Start", network.UIstart, 0, network.Waypoints.Count - 1);
            network.UIEnd = EditorGUILayout.IntSlider("Waypoint End", network.UIEnd, 0, network.Waypoints.Count - 1); 
        }
       

        DrawDefaultInspector();
    }




    void OnSceneGUI()
    {
        AI_WayPoint_Network network = (AI_WayPoint_Network)target;

        for (int i = 0; i < network.Waypoints.Count; i++)
        {
            Handles.Label(network.Waypoints[i].position, "Waypoint" + i.ToString());
        }
        if (network.DisplayMode == PathDisplayMode.Connections)
        {
            Vector3[] linePoints = new Vector3[network.Waypoints.Count + 1];

            for (int i = 0; i < network.Waypoints.Count; i++)
            {
                int index = i != network.Waypoints.Count ? i : 0;
                if (network.Waypoints[index] != null)
                    linePoints[i] = network.Waypoints[index].position;
                else
                    linePoints[i] = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
            }
            Handles.color = Color.red;
            Handles.DrawPolyLine(linePoints);
        }
        else
        if(network.DisplayMode == PathDisplayMode.Paths)
        {
            NavMeshPath path = new NavMeshPath();
            Vector3 from = network.Waypoints[network.UIstart].position;
            Vector3 to = network.Waypoints[network.UIEnd].position;

            NavMesh.CalculatePath(from, to, NavMesh.AllAreas, path);

            Handles.color = Color.yellow;
            Handles.DrawPolyLine(path.corners);

        }


    }

}
