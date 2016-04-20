using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    private Seek seek;
    public GameObject target;

    //Debug flags
    public bool debugGrid; //Displays grid debug
    public bool debugPath; //Display visual debug path
    public bool smoothen; //Toggle path smoothening feature

    void Awake() {
        //Add seek behavior if it doesn't exist
        seek = gameObject.AddComponent<Seek>();
        target = GameObject.Find("Player");
    }

	// Use this for initialization
	void Start () {
        float initialDelay = Random.Range(0, 2); 
        float delay = Random.Range(1, 3);
        InvokeRepeating("RepeatingFunction", initialDelay, delay);
    }

    void Update() {
        seek.setDebug(debugGrid, debugPath, smoothen);
    }

    void RepeatingFunction() {
        //If a target exists, actively seek it
        if (target != null)
            seek.setTarget(target.transform.position);
    }
}
