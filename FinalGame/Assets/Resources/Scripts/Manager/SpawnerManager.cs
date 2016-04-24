using UnityEngine;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour {
    //Keep track of spawners
    List<GameObject> inactive;
    List<GameObject> active;

	// Use this for initialization
	void Awake () {
	    inactive = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawners"));
        active = new List<GameObject>();

        foreach (GameObject obj in inactive)
            obj.SetActive(false);

        InvokeRepeating("activateRandomSpawner", 0, 1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void activateRandomSpawner() {
        if(inactive.Count > 0) {
            //Get random number in order to spawn random spawner
            int random = Random.Range(0, inactive.Count - 1);

            //spawn that spawner on the inactive list
            activateSpawner(random);
        }
    }

    void activateSpawner(int i) {
        if(i >= 0 && i < inactive.Count) {
            //Get spawner from inactive list
            GameObject obj = inactive[i];

            //Remove spawner from inactive list
            inactive.Remove(obj);

            //Add spawner to active list
            active.Add(obj);

            //Set Visible
            obj.SetActive(true);
        }
    }

    void deactivateSpawner(int i) {
        if(i >= 0 && i < inactive.Count) {
            //Get spawner from active list
            GameObject obj = active[i];

            //Remove spawner from active list
            active.Remove(obj);

            //Add spawner to inactive list
            inactive.Add(obj);

            //Set not active
            obj.SetActive(false);
        }
    }

    void deactivateSpawner(GameObject obj) {
        //Remove spawner from active list
        active.Remove(obj);

        //Add spawner to inactive list
        inactive.Add(obj);

        //Set not active
        obj.SetActive(false);      
    }

    public void deactivateAll() {
        List<GameObject> delete = new List<GameObject>();

        foreach(GameObject obj in active) {
            delete.Add(obj);
        }


        foreach(GameObject obj in delete) {
            deactivateSpawner(obj);
        }

        delete.Clear();
    }
}
