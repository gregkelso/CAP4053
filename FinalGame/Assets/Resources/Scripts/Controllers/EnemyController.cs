using UnityEngine;
using System.Collections;

//Basic Enemy Controller
public class EnemyController : Controller {
    //Initialize controller and parent
    protected override void Start() {
        base.Start();

        //SetOpponent - Used in sensors to detect players and summons
        setOpponent(1 << LayerMask.NameToLayer("Players") | 1 << LayerMask.NameToLayer("Summons"));
    }

    //Update is called once per frame
    protected override void Update() {
        base.Update(); //Call parent update
    }
}
