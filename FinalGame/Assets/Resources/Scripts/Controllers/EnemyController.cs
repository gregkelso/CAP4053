using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            //Destroy self
            Destroy(gameObject);

            //Destroy other 
            Destroy(other.gameObject);

            //Signal to restart game
            SceneManager.LoadScene("GameStagingScene");
        }
        else if(other.gameObject.tag == "Summon") {
            //Destroy self
            //if (gameObject != null ) {
                Path p = gameObject.GetComponent<Seek>().getPath();
                if (p != null)
                    p.Destroy();

                Destroy(gameObject);

                //Destroy other - through manager
                other.gameObject.GetComponent<SummonController>().destroy();
            //}
        }
    }
}
