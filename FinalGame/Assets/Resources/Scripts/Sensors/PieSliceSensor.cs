using UnityEngine;

public class PieSliceSensor : Sensor {
    //Global variables
    //PRIVATE  
    private AdjacentAgentSensor adjacent;
    private float radius;

    //PUBLIC
    public string id;
    public float firstAngle;
    public float secondAngle;
    public bool debug;
    public int value;

    protected override void Start() {
        //Call Parent start 
        base.Start();

        //Obtain Adjacent Agent Sensor
        adjacent = GetComponent<AdjacentAgentSensor>();
    }

    void Update() {
        radius = adjacent.getRadius();
        value = castPieSlice();
    }

	public int castPieSlice() {
        //Get Angle directions
        Vector3 a = obj.getDirectionRadians(firstAngle).normalized;
        Vector3 c = obj.getDirectionRadians(secondAngle).normalized;
        int count = 0;

        //Cast adjacent agent sensor to get list of agents within radius
        AdjacentData[] list = adjacent.castRadius();

        //Iterate through list of agents
        for(int i = 0; i < list.Length; i++) {
            //Check agent is within radius
            if(list[i].getDistance() < radius) {

                Vector3 b = (list[i].obj.transform.position - obj.transform.position).normalized;

                if (Vector3.Dot(Vector3.Cross(a, b), Vector3.Cross(a, c)) >= 0 && Vector3.Dot(Vector3.Cross(c, b), Vector3.Cross(c, a)) >= 0)
                    count++;
            }
        }

        //Draw visual debug lines if enabled
		if (debug) {
			Debug.DrawRay (obj.transform.position, a * radius, Color.black);
			Debug.DrawRay (obj.transform.position, c * radius, Color.black);
		}

        //Return number of enemies in pie slice
		return count;
	}

	//Getters
	public float getRadius() {
		return radius;
	}

	public float getFirstAngle() {
		return firstAngle;
	}

	public float getSecondAngle() {
		return secondAngle;
	}

	//Setters
	public void setRadius(float radius) {
		this.radius = radius;
	}

	public void setFirstAngle(float firstAngle) {
		this.firstAngle = firstAngle;
	}

	public void setSecondAngle(float secondAngle) {
		this.secondAngle = secondAngle;
	}
}