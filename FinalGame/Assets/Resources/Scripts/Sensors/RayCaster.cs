using UnityEngine;

//Cast a ray and return the distance to an object if there
public class RayCaster : Sensor {
    //Global Variables
    //PUBLIC
    public string id; //Used to identify ray caster
    public float angle; //Angle offsetted from agent rotation
    public float rayDistance; //Max distance of ray
    public bool debug; //Draw debug line
    public float value; //Store distance value of ray

    //PRIVATE
    private int obstacleLayer = 9;
    private int layerMask;

    //Initialize Defaults
    protected override void Start() {
        //Call Parent start 
        base.Start();

        //Configure
        layerMask = 1 << obstacleLayer;
    }

    void Update() {
        value = castRay();
    }

	//Cast ray and return distance to object or -1 if no collision
	public float castRay () {
        //Calculate Ray dir from heading
        Vector3 direction = obj.getDirectionRadians(angle);

        // Draw debug ray (current position, direction, color)
        if (debug) 
            Debug.DrawRay(obj.transform.position, direction * rayDistance, Color.green);  

        //Cast Ray
        RaycastHit2D hit = Physics2D.Raycast(obj.transform.position, direction, rayDistance, layerMask);

		if (hit.collider != null) {
			if (hit.collider.tag == "Obstacle") {
                //Draw debug ray
                if(debug)
				    Debug.DrawRay (obj.transform.position, direction * rayDistance, Color.yellow);

                //Return hit distance
				return hit.distance;
			} 
		}

		//No collision occured
		return -1.0f;
	}

    public float getRayDistance() {
        return rayDistance;
    }

    public void setRayDistance(float rayDistance) {
        this.rayDistance = rayDistance;
    }

    public float getRayAngle() {
        return angle;
    }

    public void setRayAngle(float angle) {
        this.angle = angle;
    }

    public void setDebug(bool debug) {
        this.debug = debug;
    }

    public float getValue() {
        return value;
    }
}


