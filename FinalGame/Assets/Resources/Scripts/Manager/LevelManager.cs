using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    GameObject player;
    SummonManager summonManager;
    SpawnerManager spawnerManager; 

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
        try {
            player.transform.position = Vector3.zero;
            player.GetComponent<PlayerController>().setHeading(0);
        }
        catch(System.Exception) {

        }
    }

    //Destroy all enemies
    private void destroyEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(GameObject enemy in enemies) {
            EnemyController cont = enemy.GetComponent<EnemyController>();
            if(cont != null)
                cont.destroyself();
        }
    }

    //Destroy all summons
    private void destroySummons() {
        //if(summonManager != null)
            summonManager.destroyAll();
    }

    //Deactivate all spawners
    private void deactivateSpawners() {
        //if(spawnerManager != null)
            spawnerManager.deactivateAll();
    }
}