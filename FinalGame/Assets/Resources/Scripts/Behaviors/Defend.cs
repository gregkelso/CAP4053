using UnityEngine;
using System.Collections;

public class Defend : MonoBehaviour {
    private GameObject player;
    public float defendRadius = 20;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = player.transform.position + new Vector3(0, defendRadius, 0);
	}
}
