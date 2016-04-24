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
            //Signal to restart game
            GameObject player = other.gameObject;
            LevelManager manager = player.GetComponent<LevelManager>();

            UIController canvas = GameObject.Find("Canvas").GetComponent<UIController>();
            canvas.saveBest();
            canvas.resetTimer();

            manager.ResetLevel();
        }
        else if (other.gameObject.tag == "Summon") {
            destroyself();

            //Destroy other - through manager
            other.gameObject.GetComponent<SummonController>().destroy();
        }
    }

    public void destroyself() {
        //Destroy self
        Path p = gameObject.GetComponent<Seek>().getPath();
        if (p != null)
            p.Destroy();

        if(gameObject != null)
            Destroy(gameObject);
    }
}
