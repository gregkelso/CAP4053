using UnityEngine;

//Basic collision avoidance behavior
public class CollisionAvoidance : MonoBehaviour {
    //Global Variables
    public Controller controller;
    public RayCaster front;
    public RayCaster left;
    public RayCaster right;
 
	// Use this for initialization
	void Start () {
        //Obtain agent controller
        controller = GetComponent<Controller>();

        //Obtain agent sensors
        foreach(RayCaster ray in GetComponents<RayCaster>()) {
            if (ray.id.Equals("FRONT"))
                front = ray;
            else if (ray.id.Equals("LEFT")) 
                left = ray;
            else if (ray.id.Equals("RIGHT"))
                right = ray;
        }
    }

	
	// Update is called once per frame
	void Update () {
        //If hit on left, turn right
        if (left.getValue() != -1) {
            controller.moveBackward(20);
            controller.rotateRight(45);
            controller.moveForward(5);
        }

        //If hit on right, turn left
        else if (right.getValue() != -1) {
            controller.moveBackward(20);
            controller.rotateLeft(45);
            controller.moveForward(5);
        }
	}
}
