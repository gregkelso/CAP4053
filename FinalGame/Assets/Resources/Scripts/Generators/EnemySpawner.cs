using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    private GameObject enemies; //Used as folder to store instantiated enemies
    public GameObject enemyPrefab; //Prefab of all enemies to be instantiated

    void Awake() {
        //Find parent enemies object (used as folder)
        enemies = GameObject.Find("Enemies");
    }

	// Use this for initialization
	void Start () {
        //Invoke function on delay
        float initDelay = 1;
        float delay = 1;       
        InvokeRepeating("spawnEnemy", initDelay, delay);
    }

    void spawnEnemy() {
        GameObject obj = Instantiate<GameObject>(enemyPrefab); //Create based on prefab
        obj.transform.parent = enemies.transform; //Set enemies folder as parent
        obj.transform.localPosition = transform.position; //Set position onto spawner
    }
}