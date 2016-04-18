using UnityEngine;

//Data structure to store and manipulate 3D edges
public class Edge {
    //Store the source and destination point of an edge
    private Vector3 source;
    private Vector3 destination;

    public Edge(Vector3 source, Vector3 destination) {
        this.source = source;
        this.destination = destination;
    }

    public Vector3 getSource() {
        return source;
    }

    public void setSource(Vector3 source) {
        this.source = source;
    }

    public Vector3 getDestination() {
        return destination;
    }

    public void setDestination(Vector3 destination) {
        this.destination = destination;
    }

    public string toString() {
        return "Edge: " + source + ", " + destination;
    }
}
