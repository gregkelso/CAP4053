using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    public GameObject target;
    private Seek seek;

	// Use this for initialization
	void Start () {
        //Add seek behavior if it doesn't exist
        seek = gameObject.AddComponent<Seek>();

        float initialDelay = Random.Range(0,5); 
        float delay = Random.Range(1, 6);
        InvokeRepeating("RepeatingFunction", initialDelay, delay);
    }

    void RepeatingFunction() {
        //If a target exists, actively seek it
        if (target != null)
            seek.setTarget(target.transform.position);
    }
}
