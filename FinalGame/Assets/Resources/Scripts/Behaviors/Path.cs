using UnityEngine;
using System.Collections.Generic;
using System;

//Stores and manipulates a path of 3d coordinates used for navigation
public class Path {
    //Global Variables
    public GameObject pathObj; //object which stores visual path
    public GameObject wayPointPrefab; //Prefab of waypoint
    public Queue<Vector3> path; //List of points which make the path
    public Queue<GameObject> wayPoints; //List of gameobjects which display path
    public LineRenderer lineRenderer; //Line which draws path 
    
    //Instantiate path
    public Path(bool debug) {
        //Init Queue and waypoints list
        path = new Queue<Vector3>();
        wayPoints = new Queue<GameObject>();

        //Cache Waypoint prefab
        wayPointPrefab = Resources.Load("Prefabs/Waypoint") as GameObject;

        //If Debug is enabled, show path
        if (debug) {
            //Init GameObject
            pathObj = new GameObject("Path");

            //Config Line Renderer
            lineRenderer = pathObj.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.Load("Materials/Path") as Material;
            lineRenderer.SetWidth(10, 10); //Set width of line
        }
        else
            disableDebug(); //Disable debug mode
    }

    //Add Node to end of path
    public void addNode(Vector3 node) {
        //Add vector to queue
        path.Enqueue(node);

        //Create WayPoint
        GameObject wp = MonoBehaviour.Instantiate(wayPointPrefab) as GameObject;
        //wp.transform.parent = obj.transform;
        wp.transform.position = node; //Set waypoint position
        wayPoints.Enqueue(wp); //Add waypoint to object list

        //Update line renderer to draw path
        updateRenderer();
    }

    //Peek at next node in list without removing it
    public Vector3 peek() {
        try {
            //Return point at top of queue
            return path.Peek();
        }
        catch(Exception) {
            //Only occurs when path is empty
            throw new Exception();
        }
    }

    //Return next point and remove from queue
    public Vector3 nextNode() {
        //Configure linerenderer
        updateRenderer();

        try {
            //Remove next Waypoint from game to prevent mem leak
            MonoBehaviour.Destroy(wayPoints.Dequeue());
            
            //Return point
            return path.Dequeue();     
        }
        catch(Exception) {
            //Only happens when list is empty
            throw new Exception();
        }
    }

    //Return # Nodes in path
    public int size() {
        return path.ToArray().Length;
    }

    //Destroy entire path - lines, waypoints and queue
    public void clear() {
        path.Clear(); //Clear Queue
        updateRenderer(); //Update drawn lines
        DestroyWayPoints(); //Destroy waypoint objects
    }

    //Update drawn path 
    private void updateRenderer() {
        //If line renderer exists
        if (lineRenderer != null) {
            //Convert queue to array
            Vector3[] points = path.ToArray();

            //Set number of vertices and points to draw
            lineRenderer.SetVertexCount(points.Length);
            lineRenderer.SetPositions(points);
        } 
    }

    public void enableDebug() {
        lineRenderer.enabled = true;
    }

    public void disableDebug() {
        lineRenderer.enabled = false;
    }

    //Destroy Entire path and all associated game objects
    public void Destroy() {
        DestroyWayPoints();

        if(pathObj != null)
            MonoBehaviour.Destroy(pathObj);
    }

    //Iterate through all waypoint gameobjects and remove from game
    public void DestroyWayPoints() {
        foreach (GameObject g in wayPoints.ToArray())
            MonoBehaviour.Destroy(g);
    }
}