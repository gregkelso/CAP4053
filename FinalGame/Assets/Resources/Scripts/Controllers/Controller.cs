using UnityEngine;

// Basic agent controller 
public class Controller : MonoBehaviour {
    //Global Variables
    //PRIVATE
    protected Rigidbody2D rb; //Allow physics on agent

    //PUBLIC
    public float movementSpeed; 
    public float rotationSpeed;
    public float heading; //Store global heading in degrees

    void Awake() {
        //Configure RigidBody 
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.drag = 10;
        rb.angularDrag = 0;
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

	// Use this for initialization
	protected virtual void Start () {
    }
	
	// Update is called once per frame
	protected virtual void Update () {
    }

    //Move agent foward
    public virtual void moveForward(float offset = 0) {
        rb.AddForce(transform.up * (movementSpeed + offset) * Time.deltaTime);
    }

    //Move agent backward
    public virtual void moveBackward(float offset = 0) {
        rb.AddForce(-transform.up * (movementSpeed + offset) * Time.deltaTime);
    }


    //Rotate Left 
    public void rotateLeft(float offset = 0) {
        //Calculate new heading
        heading = (heading + (rotationSpeed + offset) * Time.deltaTime) % 360;

        //Set Angle
        setHeading();
    }

    //Rotate Right
    public void rotateRight(float offset = 0) {
        //Calculate new heading
        heading = (heading - (rotationSpeed - offset) * Time.deltaTime) % 360;

        //Set Angle
        setHeading();
    }

    //Realign Agent with current heading
    public void setHeading() {
        // Normalize heading
        if (heading < 0)
            heading = 360 - Mathf.Abs(heading);

        //Enforce Range of [0, 360] degrees
        heading %= 360;

        //Physically rotate agent
        transform.eulerAngles = new Vector3(0, 0, heading);
    }

    //Set global heading in degrees
    public void setHeading(float heading) {
        if (float.IsNaN(heading) == false) {
            this.heading = heading;
            setHeading();
        }
    }

    //Align heading with destination point
    public void setHeading(Vector3 targetPosition) {
        //Get Relative Angle difference
        float relativeAngle = getRelativeAngle(targetPosition);

        //Set heading based on offset
        setHeading(heading + relativeAngle);
    }

    //Return global heading in degrees
    public float getHeading() {
        return heading;
    }

    //Return global direction as unit vector with a magnitude of 1 in radians
    public Vector3 getDirectionRadians() {
        return (new Vector3(Mathf.Cos((90 + heading) * Mathf.Deg2Rad), Mathf.Sin((90 + heading) * Mathf.Deg2Rad), 0.0f)).normalized;
    }

    //Return global direction as unit vector with a magnitude of 1 in radians
    public Vector3 getDirectionRadians(float offSet) {
        return (new Vector3(Mathf.Cos((90 + offSet + heading) * Mathf.Deg2Rad), Mathf.Sin((90 + offSet + heading) * Mathf.Deg2Rad), 0.0f)).normalized;
    }

    //Get the relative angle between two points
    public float getRelativeAngle(Vector3 targetPosition) {
        Vector3 diff = targetPosition - transform.position;
        Vector3 heading = getDirectionRadians();

        //PREVENT DIVIDE BY ZERO ERRORS
        if (diff.magnitude == 0 || heading.magnitude == 0)
            return 0;

        float relativeHeading = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(diff, heading) / (diff.magnitude * heading.magnitude)); //Reverse dot product to calculate relative heading to enemy
        relativeHeading = (Vector3.Cross(diff, heading).z < 0) ? relativeHeading : -relativeHeading; //Crossproduct to determine relative direction of angle

        //Return Relative angle
        return relativeHeading;
    }
}