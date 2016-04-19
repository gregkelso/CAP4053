using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    private Seek seek;
    public GameObject target;
    
    void Awake() {
        //Add seek behavior if it doesn't exist
        seek = gameObject.AddComponent<Seek>();
        target = GameObject.Find("Player");
    }

	// Use this for initialization
	void Start () {
        float initialDelay = Random.Range(0, 5); 
        float delay = Random.Range(1, 5);
        InvokeRepeating("RepeatingFunction", 2, 2);
    }

    void RepeatingFunction() {
        //If a target exists, actively seek it
        if (target != null)
            seek.setTarget(target.transform.position);
    }
}
