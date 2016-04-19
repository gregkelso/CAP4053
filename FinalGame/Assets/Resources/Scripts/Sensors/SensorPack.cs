using UnityEngine;
using System.Collections;

//Script to add all main sensors to a game object
public class SensorPack : MonoBehaviour {
    private Controller controller;

    //Sensors being added
    private RayCaster front;
    private RayCaster left;
    private RayCaster right;
    private RayCaster back;
    private AdjacentAgentSensor adjacent;
    private PieSliceSensor frontSlice;
    private PieSliceSensor leftSlice;
    private PieSliceSensor rightSlice;
    private PieSliceSensor backSlice;

    //Debug flags for all sensor in the pack
    public bool debugRayCaster;
    public bool debugAdjacentSensor;
    public bool debugPieSensor;

	void Start () {
        //Obtain controller
        controller = gameObject.GetComponent<Controller>();

        //Init & config
        initRayCasters();
        initAASensor();
        initPieSlices();
	}

    void Update() {     
        debugAdjacent(debugAdjacentSensor);
        debugPieSliceSensor(debugPieSensor);
        debugRay(debugRayCaster);
    }

    private void initRayCasters() {
        //Add & Config ray casters
        front = gameObject.AddComponent<RayCaster>();
        front.setID("Front");
        front.setRayAngle(0);
        front.setRayDistance(50);

        left = gameObject.AddComponent<RayCaster>();
        left.setID("Left");
        left.setRayAngle(45);
        left.setRayDistance(50);

        right = gameObject.AddComponent<RayCaster>();
        right.setID("Right");
        right.setRayAngle(-45);
        right.setRayDistance(50);

        back = gameObject.AddComponent<RayCaster>();
        back.setID("Back");
        back.setRayAngle(180);
        back.setRayDistance(50);
    }

    private void initAASensor() {
        //Add and config aa sensor
        adjacent = gameObject.AddComponent<AdjacentAgentSensor>();
        adjacent.setID("Adjacent");
        adjacent.setRadius(150);

        //Retrieve opponent from controller - set as search parameter in sensor
        adjacent.setLayerMask(controller.getOpponent());
    }

    private void initPieSlices() {
        //Add and config pie slices
        frontSlice = gameObject.AddComponent<PieSliceSensor>();
        frontSlice.setID("Front");
        frontSlice.setFirstAngle(-45);
        frontSlice.setSecondAngle(45);

        leftSlice = gameObject.AddComponent<PieSliceSensor>();
        leftSlice.setID("Left");
        leftSlice.setFirstAngle(45);
        leftSlice.setSecondAngle(135);

        rightSlice = gameObject.AddComponent<PieSliceSensor>();
        rightSlice.setID("Right");
        rightSlice.setFirstAngle(225);
        rightSlice.setSecondAngle(315);

        backSlice = gameObject.AddComponent<PieSliceSensor>();
        backSlice.setID("Back");
        backSlice.setFirstAngle(135);
        backSlice.setSecondAngle(225);
    }

    public void debugRay(bool debug) {
        //Set Debug setting
        this.debugRayCaster = debug;

        //Set each sensor
        front.setDebug(debug);
        left.setDebug(debug);
        right.setDebug(debug);
        back.setDebug(debug);
    }

    public void debugAdjacent(bool debug) {
        //Set debug setting
        this.debugAdjacentSensor = debug;

        //Set each sensor
        adjacent.setDebug(debug);
    }

    public void debugPieSliceSensor(bool debug) {
        //Set debug setting
        this.debugPieSensor = debug;

        //Set each sensor
        frontSlice.setDebug(debug);
        leftSlice.setDebug(debug);
        rightSlice.setDebug(debug);
        backSlice.setDebug(debug);
    }
}
