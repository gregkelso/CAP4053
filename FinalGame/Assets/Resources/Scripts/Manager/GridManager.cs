using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {
    public Grid grid;

	// Use this for initialization
	void Start () {
        float nodeRadius = 10;
        LayerMask mask = 1 << LayerMask.NameToLayer("Obstacles");
        grid = new Grid(Vector3.zero, gameObject.transform.localScale, nodeRadius, mask);
    }

    // Update is called once per frame
    void Update () {
        grid.generate();
	}

    public Grid getGrid() {
        return grid;
    }
}
