using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

//PathFinding Behavior which implements A* Algorithm to steer an agent from one location to another 
public class PathFinder {
    private Grid grid; //Grid used to generate path
    private bool debug;

    //Initialize Path Finder
    public PathFinder(float nodeRadius, LayerMask mask) {
        //Configure path finder
        Vector3 bgScale = GameObject.Find("Background").transform.localScale; //Obtain Background Size to calibrate grid
        Vector2 worldSize = new Vector2(bgScale.x, bgScale.y);
        
        //Initialize Grid
        grid = new Grid(Vector3.zero, worldSize, nodeRadius, mask);
        debug = false;
    }

    //Generate path from start to destination using A*
    public Path findPath(Vector3 startPos, Vector3 targetPos) {
        //Convert 3D Positions to GridNodes
        GridNode startNode = grid.getNode(startPos);
        GridNode targetNode = grid.getNode(targetPos);

        List<GridNode> openSet = new List<GridNode>();
        HashSet<GridNode> closedSet = new HashSet<GridNode>();
        openSet.Add(startNode);

        while(openSet.Count > 0) {
            GridNode currentNode = openSet[0];

            for(int i = 1; i < openSet.Count; i++) {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode) {
                List<GridNode> points = retracePath(startNode, targetNode);
                return getPath(points);
            }

            //Iterate through all neighbor nodes
            foreach (GridNode neighbor in grid.GetNeighbors(currentNode)) {
                //Skip node if not walkable or already used
                if (!neighbor.isWalkable() || closedSet.Contains(neighbor))
                    continue;

                //Calculate the new move cost to the neighbor nodes
                float moveCost = currentNode.gCost + GridNode.getDistance(currentNode, neighbor);

                //Calculate neighbor's costs
                if (moveCost < neighbor.gCost || !openSet.Contains(neighbor)) {
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
    private Path getPath(List<GridNode> nodes) {
        //Instantiate a path
        Path path = new Path(debug);

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

    public void enableDebug() {
        debug = true;
    }

    public void disableDebug() {
        debug = false;
    }
}