using UnityEngine;
using System.Collections.Generic;

//PathFinding Behavior which implements A* Algorithm to steer an agent from one location to another 
public class PathFinder {
    private Grid grid; //Grid used to generate path

    //Initialize Path Finder
    public PathFinder(float nodeRadius, LayerMask mask) {
        //Configure path finder
        Vector3 bgScale = GameObject.Find("Background").transform.localScale; //Obtain Background Size to calibrate grid
        Vector2 worldSize = new Vector2(bgScale.x, bgScale.y);
        
        //Initialize Grid
        grid = new Grid(Vector3.zero, worldSize, nodeRadius, mask);
    }

    //Generate path from start to destination using A*
    public Path findPath(Vector3 startPos, Vector3 targetPos) {
        //Convert 3D Positions to GridNodes
        GridNode startNode = grid.getNode(startPos);
        GridNode targetNode = grid.getNode(targetPos);

        //Exit if nodes are out of bounds
        if (startNode == null || targetNode == null)
            return null;

        //Instantiate Open and Closed set DataStructures for A* Algorithm
        HashSet<GridNode> closedSet = new HashSet<GridNode>();
        Heap<GridNode> openSet = new Heap<GridNode>(grid.MaxSize);        
        openSet.Add(startNode); //Add start node to the open set

        //Iterate through as long as openset is not empty
        while(openSet.Count > 0) {
            //Store first node in collection 
            GridNode currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            //Check if arrived at target node
            if(currentNode == targetNode) {
                //Retrace and get coordinates
                List<GridNode> points = retracePath(startNode, targetNode);

                //Convert to a path
                return getPath(points);
            }

            //Iterate through all neighbor nodes
            foreach(GridNode neighbor in grid.GetNeighbors(currentNode)) {
                //Skip node if not walkable or already used
                if (!neighbor.isWalkable() || closedSet.Contains(neighbor))
                    continue;

                //Calculate the move cost to the neighbor nodes
                int moveCost = currentNode.gCost + GridNode.getDistance(currentNode, neighbor);

                //Calculate neighbor's costs
                if(moveCost < neighbor.gCost || !openSet.Contains(neighbor)) {
                    //Set Costs
                    neighbor.gCost = moveCost;
                    neighbor.hCost = GridNode.getDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    //If the open set doesn't already contain the neighboring node, add it
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        //Unable to find a path
        return null;
    }

    //Trace a path backwards and return a list of nodes which make up the path
    private List<GridNode> retracePath(GridNode startNode, GridNode endNode) {
        //Instantiate list holding the path
        List<GridNode> path = new List<GridNode>();

        //Traversal Variable
        GridNode currentNode = endNode; 

        //Iterate through path until end was reached
        while(currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        //Reverse List
        path.Reverse();

        //Return List
        return path;
    }

    //Return a Path from a list of points
    private static Path getPath(List<GridNode> nodes) {
        //Instantiate a path
        Path path = new Path(true);

        //Iterate through all points in the path
        foreach (GridNode node in nodes) 
            path.addNode(node.getPosition());

        //Return Path
        return path;
    }

    //Return reference of pathfinding grid
    public Grid getGrid() {
        return grid;
    }
}