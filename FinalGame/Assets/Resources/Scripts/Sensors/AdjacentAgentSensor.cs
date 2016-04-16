using UnityEngine;
using System;

//Collect information about surrounding agents 
public class AdjacentAgentSensor : Sensor {
    //Global Variables
    //PRIVATE 

    //PUBLIC
    public GameObject circle;
    public float radius;
    public bool debug;
    public AdjacentData[] values;
    public LayerMask mask; //Layer in which the sensor acts on

    //Initialize Defaults
    protected override void Start() {
        //Call Parent start 
        base.Start();

        circle = Resources.Load("Prefabs/Circle") as GameObject;

        //Init circle if debug is enabled
        if (debug)
            initializeCircle();
    }

    //Update each frame
    void Update() {
        //Reconfigure Circle
        resizeCircle();

        //Analyze Radius
        values = castRadius();
    }

    //Cast sensor and update display
	public AdjacentData[] castRadius() {
        //Store 2D Position
        Vector2 pos = new Vector2(obj.transform.position.x, obj.transform.position.y);

        // Create the collider array of agents to be stored when they hit the overlapSphere
		Collider2D[] adjacentAgents = Physics2D.OverlapCircleAll(pos, radius, mask);

        //Create list of AdjacentSensorData to store important information
        AdjacentData[] list = new AdjacentData[adjacentAgents.Length];

        //Iterate through all agents inside sensor radius
        for(int i = 0; i < adjacentAgents.Length; i++) {
            //Store adjacent gameobject
            GameObject adj = adjacentAgents[i].gameObject;

            //Calculate
            float distance = (adj.transform.position - obj.transform.position).magnitude;  
            float relativeHeading = obj.getRelativeAngle(adj.transform.position);

            //Store Adjacent Data in list
            list[i] = new AdjacentData(adj, distance, relativeHeading);
        }

        //Return list of adjacent agents and sensor data
        return list;
	}

    //Get radius of sensor
    public float getRadius() {
        return radius;
    }

    //Set radius of sensor
    public void setRadius(float radius) {
        this.radius = radius;

        //Reconfigure Circle
        resizeCircle();
    }

    public void setLayerMask(LayerMask mask) {
        this.mask = mask;
    }

    //Instantiate and Initialize Circle
    private void initializeCircle() {     
        circle = Instantiate(circle) as GameObject; //Create based on prefab
        circle.transform.parent = obj.transform; //Set obj as circle's parent   
        circle.transform.localPosition = Vector3.zero;   
        resizeCircle(); //Resize Circle arround object to match radius
    }

    //Remove Circle from object
    private void removeCircle() {
        circle.transform.parent = null; //Unparent circle from object
        Destroy(circle);
    }

    private void resizeCircle() {
        //Set circle scale based on radius
        try {
            circle.transform.localScale = new Vector3(radius / 100, radius / 100, 0);
        }
        catch(Exception) {

        }
    }
}

//Store sensor data
[System.Serializable]
public class AdjacentData
{
    //Global variables
    public GameObject obj;
    public float distance;
    public float relativeHeading;

    //Primary constructor 
    public AdjacentData(GameObject obj, float distance, float relativeHeading) {
        this.obj = obj;
        this.distance = distance;
        this.relativeHeading = relativeHeading;
    }

    //return id of adjacent object
    public string getName() {
        return obj.ToString();
    }

    //return the distance of adjacent object
    public float getDistance() {
        return distance;
    }

    //Return the heading of the adjacent object
    public float getRelativeHeading() {
        return relativeHeading;
    }

    public GameObject getGameObject() {
        return obj;
    }

    //Return all data properly formatted
    public override string ToString() {
        return "Name: " + getName() + "\tDistance: " + getDistance() + "\tRelative Headin: " + getRelativeHeading();
    }
}