using UnityEngine;

//Basic player controller
public class PlayerController : Controller {
    //Global Variables
    private static GameObject summons; //Gameobject parent of all summons
    private static GameObject attacker;
    private static GameObject defender;
    private static GameObject bomber;

    protected override void Awake() {
        base.Awake();

        //SetOpponent - Used in sensors
        setOpponent(1 << LayerMask.NameToLayer("Enemies"));

        //Get Objects and load prefabs
        summons = GameObject.Find("Summons"); 
        attacker = Resources.Load<GameObject>("Prefabs/Attacker");
        defender = Resources.Load<GameObject>("Prefabs/Defender");
        bomber = Resources.Load<GameObject>("Prefabs/Bomber");
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


        //Summon Keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //Instantiate Attacker
            GameObject obj = Instantiate<GameObject>(attacker);
            obj.transform.parent = summons.transform;   
            obj.transform.localPosition = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            //Instantiate Defender
            GameObject obj = Instantiate<GameObject>(defender);
            obj.transform.parent = summons.transform;
            obj.transform.localPosition = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            //Instantiate Bomber
            GameObject obj = Instantiate<GameObject>(bomber);
            obj.transform.parent = summons.transform;
            obj.transform.localPosition = transform.position;
        }
    }
}