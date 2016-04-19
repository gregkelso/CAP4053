using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
    private Seek seek;
    public AdjacentAgentSensor adjacent;
    public GameObject nearest;
    public bool attacking;

	// Use this for initialization
	void Start () {
        //Obtain aa sensor
        adjacent = gameObject.GetComponent<AdjacentAgentSensor>();

        //Add seek behavior if it doesn't exist
        seek = gameObject.AddComponent<Seek>();
        attacking = false;
        InvokeRepeating("attack", 0, 1);
    }
	
	// Update is called once per frame
	void Update () {
        //Check if there are nearby enemies
        if (adjacent.values != null && adjacent.values.Length >= 1) {
            //Initial min distance
            nearest = adjacent.values[0].getGameObject();
            float minDistance = adjacent.values[0].getDistance();
            
            //Find the closest enemy
            foreach (AdjacentData d in adjacent.values) {
                if (d.getDistance() < minDistance) {
                    nearest = d.getGameObject();
                    minDistance = d.getDistance();
                }
            }

            //Attack enemy
            if (attacking == false && nearest.gameObject != null) {
                seek.setTarget(nearest.transform.position);
                attacking = true;
            }
        }
	}

    void attack() {
        if (attacking && nearest.gameObject != null)
            seek.setTarget(nearest.transform.position);
    }
}
