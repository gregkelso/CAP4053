using UnityEngine;

//Generic Sensor which acts on a controller
public class Sensor : MonoBehaviour {
    //Global Variables
    //PROTECTED
    protected Controller obj;
    public string id;

    //Store agent controller
    protected virtual void Start() {
        obj = GetComponent<Controller>();
    }

    public void setID(string id) {
        this.id = id;
    }

    public string getID() {
        return id;
    }
}
