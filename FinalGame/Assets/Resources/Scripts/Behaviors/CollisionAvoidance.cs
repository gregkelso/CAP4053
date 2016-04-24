using UnityEngine;

//Basic collision avoidance behavior
public class CollisionAvoidance : MonoBehaviour {
    //Global Variables
    public Controller controller;
    public SensorPack sensors;
 
	// Use this for initialization
	void Start () {
        //Obtain agent controller
        controller = GetComponent<Controller>();
        sensors = GetComponent<SensorPack>();
    }

	
	// Update is called once per frame
	void Update () {
        //If hit on left, turn right
        if (sensors.left.getValue() != -1) {
            controller.moveBackward(20);
            controller.rotateRight(45);
            controller.moveForward(5);
        }

        //If hit on right, turn left
        else if (sensors.right.getValue() != -1) {
            controller.moveBackward(20);
            controller.rotateLeft(45);
            controller.moveForward(5);
        }
	}
}
