using UnityEngine;

//Node generated in A* pathfinding
public class GridNode : IHeapItem<GridNode> {
    private Vector3 position; //Exact position in 3D Coordinates
    private bool walkable; //Is node traversable
    private Point gridPosition; //Grid Cell which contains this position
    public int HeapIndex;

    public int heapIndex {
        get { return HeapIndex; }
        set { HeapIndex = value; }
    }

    //Costs and Links
    public GridNode parent; //Parent of this grid node
    public int gCost; //Distance from start node
    public int hCost; //Heuristic distance from target node (Manhattan Distance)
    public int fCost { //Sum of costs
        get { return gCost + hCost; }
    }

    //Instantiate a node
    public GridNode(Vector3 position, bool walkable, Point gridPosition) {
        this.position = position;
        this.walkable = walkable;
        this.gridPosition = gridPosition;
    }

    //Return precise 3d position of grid node
    public Vector3 getPosition() {
        return position;
    }

    //Return whether this grid node is traversable
    public bool isWalkable() {
        return walkable;
    }

    //Return grid cell location
    public Point getGridPosition() {
        return gridPosition;
    }

    //Return the manhattan distance between two gridnodes
    public static int getDistance(GridNode nodeA, GridNode nodeB) {
        //Number of grid hops in x and y directions
        int xDistance = Mathf.Abs(nodeA.gridPosition.x - nodeB.gridPosition.x);
        int yDistance = Mathf.Abs(nodeA.gridPosition.y - nodeB.gridPosition.y);

        //Return correct distance
        //Cost = 10 for Up,Down,Left,Right
        //Cost = 14 for Diagonals 
        if (xDistance > yDistance)
            return 14 * yDistance + 10 * (xDistance - yDistance);
        else 
            return 14 * xDistance + 10 * (yDistance - xDistance);
    }

    public int CompareTo(GridNode nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);

        if(compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}