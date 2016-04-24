using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    GameObject player;
    SummonManager summonManager;
    SpawnerManager spawnerManager; 

    void Awake() {
    }

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        summonManager = player.GetComponent<SummonManager>();
        spawnerManager = GameObject.Find("Spawners").GetComponent<SpawnerManager>();      
    }


    public void ResetLevel() {
        destroyEnemies();
        destroySummons();
        deactivateSpawners();

        //Reposition Player to center
        player.transform.position = Vector3.zero;
        player.GetComponent<PlayerController>().setHeading(0);
    }

    //Destroy all enemies
    private void destroyEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(GameObject enemy in enemies) {
            enemy.GetComponent<EnemyController>().destroyself();
        }
    }

    //Destroy all summons
    private void destroySummons() {
        summonManager.destroyAll();
    }

    //Deactivate all spawners
    private void deactivateSpawners() {
        spawnerManager.deactivateAll();
    }
}