using UnityEngine;

//Generic Sensor which acts on a controller
public class Sensor : MonoBehaviour {
    //Global Variables
    //PROTECTED
    protected Controller obj;

    //Store agent controller
    protected virtual void Start() {
        obj = GetComponent<Controller>();
    }
}
