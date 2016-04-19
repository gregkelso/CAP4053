using UnityEngine;

//Basic player controller
public class PlayerController : Controller {
    //Global Variables    
    private SummonManager summoner;

    protected override void Awake() {
        base.Awake();

        //Add summon manager
        summoner = gameObject.AddComponent<SummonManager>();

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


        //Summon Key
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //Instantiate Attacker
            summoner.createAttacker(getHeading());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //Instantiate Defender
            summoner.createDefender(getDirectionRadians());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            //Instantiate Bomber
            summoner.createBomber(getHeading());
        }
    }
}