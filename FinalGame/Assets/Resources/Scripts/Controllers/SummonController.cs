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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            //Destroy self
            Destroy(other.gameObject);

            //Destroy enemy
            Destroy(gameObject);
        }
    }
}
