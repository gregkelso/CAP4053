using UnityEngine;
using System.Collections;

//Basic Control of asteroids - Rotate in place
public class AsteroidController : MonoBehaviour {
    //Movement configurations
    public Vector3 rotateSpeed;
    public float min = 0.1f;
    public float max = 2.5f;
    public int direction;

	// Use this for initialization
	void Start () {
        //Select random direction using +1 / -1
        direction = (Random.Range(0, 10) > 5) ? 1 : -1;

        //Set a random speed
        rotateSpeed = new Vector3(0, 0, direction * Random.Range(min, max));
	}
	
	// Update is called once per frame
	void Update () {
        //Rotate once per frame
        transform.Rotate(rotateSpeed);
	}
}
