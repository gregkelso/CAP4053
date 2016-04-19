using UnityEngine;
using System.Collections;

public class Defend : MonoBehaviour {
    private GameObject player;
    Vector3 v;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        v = transform.position - player.transform.position;
    }

    void FixedUpdate() {
        if (gameObject != null) {
            float speed = 125f;
            v = Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.forward) * v;
            transform.position = player.transform.position + v;

        }
    }
}
