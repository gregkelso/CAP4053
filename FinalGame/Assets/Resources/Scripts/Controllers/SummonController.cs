using UnityEngine;
using System.Collections;

//Basic Enemy Controller
public class SummonController : Controller
{
    protected override void Awake() {
        base.Awake();

        //SetOpponent - Used in sensors to detect players and summons
        setOpponent(1 << LayerMask.NameToLayer("Enemies"));
    }
    
    //Initialize controller and parent
    protected override void Start() {
        base.Start();
    }

    //Update is called once per frame
    protected override void Update() {
        base.Update(); //Call parent update
    }
}
