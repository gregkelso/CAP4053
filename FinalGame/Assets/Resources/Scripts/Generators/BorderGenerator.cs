using UnityEngine;
using System.Collections.Generic;

public class BorderGenerator : MonoBehaviour {
    //PRIVATE
    private Vector2 bgDimensions;

    //PUBLIC
    public GameObject prefab;
    public Sprite[] sprites;
    public List<GameObject> asteroids;

	// Use this for initialization
	void Start () {
        //Calculate Game Dimensions based on background quad
        Vector3 background = GameObject.Find("Background").transform.localScale;
        bgDimensions = new Vector2(background.x, background.y);

        //Init List
        asteroids = new List<GameObject>();

        //Generate asteroid game objects
        generate();
	}
	
    //Generate border filled with asteroids
	private void generate() {
        //Variables
        Vector3 center = transform.position;
        float width = bgDimensions.x;
        float height = bgDimensions.y;

        //Iterate through entire quadrant, create four
        for (int i = -45; i < 45; i+=7) {
            //Calculate Position 
            Vector3 pos1 = new Vector3(height / 2 * Mathf.Tan(Mathf.Deg2Rad * i), height / 2, 0);
            Vector3 pos2 = new Vector3(height / 2 * Mathf.Tan(Mathf.Deg2Rad * i), -height / 2, 0);
            Vector3 pos3 = new Vector3(width / 2, width / 2 * Mathf.Tan(Mathf.Deg2Rad * i), 0);
            Vector3 pos4 = new Vector3(-width / 2, width / 2 * Mathf.Tan(Mathf.Deg2Rad * i), 0);

            //Instantiate
            createAsteroid(center + pos1);
            createAsteroid(center + pos2);
            createAsteroid(center + pos3);
            createAsteroid(center + pos4);
        }
    }

    //Return copy of asteroid prefab
    private GameObject createAsteroid(Vector3 pos) {
        GameObject asteroid = Instantiate(prefab) as GameObject; //Instantiate asteroid
        SpriteRenderer renderer = asteroid.GetComponent<SpriteRenderer>(); //Obtain asteroid's renderer
        renderer.sprite = sprites[Random.Range(0, sprites.Length)]; //Randomly select asteroid and set

        //Resize Box Collider
        asteroid.GetComponent<BoxCollider2D>().size = renderer.bounds.size; //Set collider size equal to sprite bounds
        asteroid.transform.parent = this.transform; //Parent asteroid 
        asteroid.transform.position = pos; //Save Position of asteroid

        //Add Reference to collection
        asteroids.Add(asteroid);

        //Return Reference
        return asteroid;
    }
}