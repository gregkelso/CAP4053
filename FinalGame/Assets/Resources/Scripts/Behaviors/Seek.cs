using UnityEngine;
using System;

//A behavior used to seek a target position
public class Seek : MonoBehaviour {
    //PRIVATE
    private Controller controller; //Allows full control of agent

    //PUBLIC
    public GridManager manager;
    public bool targetSet; //While true, agent is currently seeking to destination
    public float arrivalRadius = 20; //How close to target till considered arrived at target
    public Vector3 target; //Specific Target node to face and move to
    public PathFinder pathFinder; //Calculate path from start to goal
    public Path path; //Store a path created from path finder or manually placed points

    //Debug flags
    public bool debugGrid; //Displays grid debug
    public bool debugPath; //Display visual debug path
    public bool smoothen; //Toggle path smoothening feature

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("Background").GetComponent<GridManager>(); 
        controller = GetComponent<Controller>(); //Obtain agent controller for movement
        pathFinder = new PathFinder(manager); //Initialize PathFinder
        targetSet = false; //A target hasn't been selected
	}
	
	// Update is called once per frame
	void Update () {
        checkInput(); //Handle User input
        seek(); //Seek target destination based on path selected
    }

    //Check for and process user input
    private void checkInput() {
        if (pathFinder != null) {
            //Trigger debug display
            if (debugPath == false)
                pathFinder.disableDebug();
            else
                pathFinder.enableDebug();
        }   
    }

    //Return 3D world point based on mouse position
    private Vector3 getMouseCoordinates() {
        //Convert mouse position to world coordinate
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Strip z coordinate
        point = new Vector3(point.x, point.y, 0);

        //Return coordinate
        return point;
    }

    //Seek toward a target
    private void seek() {
        //Check if a path exists and that a target is chosen
        if(targetSet && path != null) {
            //Only seek if not at destination
            if (!arrived()) {
                //Continue seeking target
                controller.setHeading(target); //Face target
                controller.moveForward(); //Move to target
            }
            else {
                try {
                    //Arrived at target           
                    path.nextNode(); //Discard node 
                    targetSet = false; //Disable target till next tick
                }
                catch(Exception) {
                    //Do Nothing
                }
            }
        }
        else if(path != null)  {
            //Target was not set, but a path exists
            try {
                //Peek at next node, but don't remove so line is drawn correctly
                target = path.peek();

                //Re-engage targeting settings
                targetSet = true;
            }
            catch (Exception) {
                //Occurs when no waypoints are nearby
                //DO NOTHING
            }
        }
    }

    //Check if agent arrived at current target and return boolean
    private bool arrived() {
        //Obtain colliders within radius surrounding target position
        Collider2D[] nodes = Physics2D.OverlapCircleAll(target, arrivalRadius);

        //Iterate through colliders near target
        for (int i = 0; i < nodes.Length; i++) {
            //Test if current collider matches parent agent
            if (nodes[i].gameObject == gameObject)
                return true; //Agent arrived at target
        }
      
        //Agent has not arrived at target position
        return false;
    }  

    //Draw Debug Grid
    void OnDrawGizmos() {
        //Only draw if available and enabled
        if (pathFinder != null && debugGrid == true) {
            //Obtain grid reference
            Grid grid = manager.getGrid();

            //Draw outline
            Gizmos.DrawWireCube(grid.getWorldPosition(), new Vector3(grid.getWorldSize().x, grid.getWorldSize().y, 0));

            //Iterate through each cell
            foreach (GridNode n in grid.getGrid()) {
                //Select Color and Draw Reference square
                Gizmos.color = n.isWalkable() ? Color.white : Color.red;

                //Modify Color if player is in node
                if (grid.getNode(transform.position) == n)
                    Gizmos.color = Color.cyan;

                Gizmos.DrawCube(n.getPosition(), new Vector3(1, 1, 0) * (grid.getNodeDiameter() - .5f));
            }

            //Draw Debug Path
            if (debugPath == true && path != null) {
                foreach (Vector3 n in path.getPath()) {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n, new Vector3(1, 1, 0) * (grid.getNodeDiameter() - .5f));
                }
            }
        }
    }

    public void setArrivalRadius(float radius) {
        this.arrivalRadius = radius;
    }

    //Set a target from current position
    public void setTarget(Vector3 target) {
        //Delete path if one exists
        if (path != null)
            path.Destroy();

        //Generate path to target
        path = pathFinder.findPath(transform.position, target);

        //Set parent object if path is available
        if (path != null) {
            //Apply basic path smoothener if debug is enabled
            if (smoothen)
                path.quickSmooth(manager.getGrid());

            //Set object as parent of path
            path.pathObj.transform.parent = transform;

            //Disable target till next tick
            targetSet = false;
        }
    }

    public void setDebug(bool debugGrid, bool debugPath, bool smoothen) {
        this.debugGrid = debugGrid;
        this.debugPath = debugPath;
        this.smoothen = smoothen;

        checkInput();
    }

    public Path getPath() {
        return path;
    }
}           