using UnityEngine;

//Basic player controller
public class PlayerController : Controller {
    //Global Variables

    protected override void Awake() {
        base.Awake();

        //SetOpponent - Used in sensors
        setOpponent(1 << LayerMask.NameToLayer("Enemies"));
    }

    //Initialize controller and parent
    protected override void Start() {
        base.Start();
    }

    //Update is called once per frame
    protected override void Update () {
		processInput(); //Process Keyboard Input
        base.Update(); //Call parent update
    }

    //Process keyboard input
    void processInput() {
        //Up Arrow
        if (Input.GetKey(KeyCode.UpArrow))
            moveForward();

        //Down Arrow
        if (Input.GetKey(KeyCode.DownArrow))
            moveBackward();

        //Left Arrow
        if (Input.GetKey(KeyCode.LeftArrow))
            rotateLeft();

        //Right Arrow
        if (Input.GetKey(KeyCode.RightArrow))
            rotateRight();
    }
}