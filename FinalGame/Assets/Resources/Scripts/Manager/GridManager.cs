using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {
    public Grid grid;

	// Use this for initialization
	void Start () {
        float nodeRadius = 20;
        LayerMask mask = 1 << LayerMask.NameToLayer("Obstacles");
        grid = new Grid(Vector3.zero, gameObject.transform.localScale, nodeRadius, mask);

        //Invoke Function to regenerate grid once per second
        InvokeRepeating("regenerate", 0, 1);
    }

    public Grid getGrid() {
        return grid;
    }

    void regenerate() {
        grid.generate();
    }
}
