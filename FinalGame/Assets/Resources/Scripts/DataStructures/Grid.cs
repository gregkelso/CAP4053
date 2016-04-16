using UnityEngine;
using System.Collections.Generic;

//Store and manipulate a grid of gridNodes used in pathfinding A*
public class Grid {
    private GridNode[,] grid; //2D Array of gridNodes which represent the grid
    private Vector3 worldPosition; //Location of the center of the grid in world space
    private Vector2 worldSize; //Size of the grid in pixels
    private Point gridSize; //Dimension of grid in units
    private float nodeRadius; //Size of each node in the grid
    private float nodeDiameter; //Diameter of each node in the grid
    private LayerMask unwalkableMask; //Specifies layers considered while generating grid
    private Vector3 bottomLeft; //Store bottom left corner of grid in 3d world points

    //Return maximum number of nodes (used to create heap)
    public int MaxSize {
        get { return gridSize.x * gridSize.y; }
    }

    //Instantiate a grid
    public Grid(Vector3 worldPosition, Vector2 worldSize, float nodeRadius, LayerMask unwalkableMask) {
        //Store values      
        this.worldPosition = worldPosition;
        this.worldSize = worldSize;
        this.nodeRadius = nodeRadius;
        this.unwalkableMask = unwalkableMask;

        //Init Required Vals
        nodeDiameter = nodeRadius * 2; //Compute Diameter based on radius
        gridSize.x = Mathf.RoundToInt(worldSize.x / nodeDiameter); //Calculate # of nodes in x direction
        gridSize.y = Mathf.RoundToInt(worldSize.y / nodeDiameter); //Calculate # of nodes in y direction

        //Create Grid
        generate();
    }

    //Create grid
    public GridNode[,] generate() {
        //Instantiate 2D Node Array
        grid = new GridNode[gridSize.x, gridSize.y];

        //Calculate 3D location of bottom left grid node
        bottomLeft = worldPosition - (Vector3.right * worldSize.x / 2) - (Vector3.up * worldSize.y / 2);
        
        //Iterate through entire grid
        for(int x = 0; x < gridSize.x; x++) {
            for(int y = 0; y < gridSize.y; y++) {
                //Calculate Node parameters
                Vector3 pos = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius); //Position of Node in 3d Space
                bool walkable = !(Physics2D.OverlapCircle(pos, nodeRadius, unwalkableMask)); //Check if node is traversable

                //Create and store node with specified parameters 
                grid[x, y] = new GridNode(pos, walkable, new Point(x, y));
            }
        }

        //Return Grid
        return grid;
    }

    //Return a list of neighboring nodes of a specified node
    public List<GridNode> GetNeighbors(GridNode node) {
        //Instantiate an empty list of neighboring nodes
        List<GridNode> neighbors = new List<GridNode>();

        //Iterate through all possible surrounding nodes
        for(int x = -1; x <= 1; x++) {
            for(int y = -1; y <= 1; y++) {
                //Skip Center node in 3 x 3 search block
                if (x == 0 && y == 0)
                    continue;

                //Calculate Potential Neighbor's Grid Position
                int gridX = node.getGridPosition().x + x;
                int gridY = node.getGridPosition().y + y;

                //Verify Position Validity
                if (gridX >= 0 && gridX < gridSize.x && gridY >= 0 && gridY < gridSize.y)
                    neighbors.Add(grid[gridX, gridY]);
            }
        }

        //Return list
        return neighbors;
    }

    //Return GridNode at this position
    public GridNode getNode(int x, int y) {
        return grid[x, y];
    }

    //Convert a 3D world point into a grid coordinate and return node which resides at that position
    public GridNode getNode(Vector3 pos) {
        //Calculate grid coordinates
        //Subtract 1 since array is 0 based
        int x = Mathf.RoundToInt(Mathf.Abs(pos.x - bottomLeft.x) / nodeDiameter);
        int y = Mathf.RoundToInt(Mathf.Abs(pos.y - bottomLeft.y) / nodeDiameter);

        //Return GridNode
        if (x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y)
            return getNode(x, y);
        else
            return null; //GridNode at this position is unavailable
    }

    //Return actual 2D array which stores the grid
    public GridNode[,] getGrid() {
        return grid;
    }

    //Return the size of grid
    public Point getGridSize() {
        return gridSize;
    }

    //Return size of the grid in pixels (width, height)
    public Vector2 getWorldSize() {
        return worldSize;
    }

    //Return the diameter of each node in pixels
    public float getNodeDiameter() {
        return nodeDiameter;
    }

    //Return the center point of the grid in pixels (3d position)
    public Vector3 getWorldPosition() {
        return worldPosition;
    }
}
