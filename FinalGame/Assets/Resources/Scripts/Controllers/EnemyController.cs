using UnityEngine;
using System.Collections;

//Basic Enemy Controller
public class EnemyController : Controller {
    protected override void Awake() {
        base.Awake();

        //SetOpponent - Used in sensors to detect players and summons
        setOpponent(1 << LayerMask.NameToLayer("Players") | 1 << LayerMask.NameToLayer("Summons"));
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
