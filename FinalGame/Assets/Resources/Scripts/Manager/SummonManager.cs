﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummonManager : MonoBehaviour {
    //Prefab definitions
    private static GameObject summons; //Gameobject parent of all summons
    private static GameObject attacker;
    private static GameObject defender;
    private static GameObject bomber;

    //List of summons
    public List<GameObject> attackers;
    public List<GameObject> defenders;
    public List<GameObject> bombers;

    void Awake() {
        //Get Objects and load prefabs
        summons = GameObject.Find("Summons");
        attacker = Resources.Load<GameObject>("Prefabs/Attacker");
        defender = Resources.Load<GameObject>("Prefabs/Defender");
        bomber = Resources.Load<GameObject>("Prefabs/Bomber");
    }

	// Use this for initialization
	void Start () {
        attackers = new List<GameObject>();
        defenders = new List<GameObject>();
        bombers = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject createAttacker() {
        //Instantiate Attacker
        GameObject obj = Instantiate<GameObject>(attacker);
        obj.transform.parent = summons.transform;
        obj.transform.localPosition = transform.position;
        obj.GetComponent<SummonController>().setSummonType(SummonType.ATTACKER);

        //Add to list
        attackers.Add(obj);

        //Return gameobject
        return obj;
    }

    public GameObject createDefender() {
        //Instantiate Defender
        GameObject obj = Instantiate<GameObject>(defender);
        obj.transform.parent = transform;
        obj.GetComponent<SummonController>().setSummonType(SummonType.DEFENDER);

        //Add to list
        defenders.Add(obj);

        //Return gameobject
        return obj;
    }

    public GameObject createBomber() {
        //Instantiate Bomber
        GameObject obj = Instantiate<GameObject>(bomber);
        obj.transform.parent = summons.transform;
        obj.transform.localPosition = transform.position;
        obj.GetComponent<SummonController>().setSummonType(SummonType.BOMBER);

        //Add to list 
        bombers.Add(obj);

        //Return gameobject
        return obj;
    }

    public void destroySummon(GameObject obj, SummonType type) {
        if(type == SummonType.ATTACKER) {
            Destroy(obj);
        }
        else if(type == SummonType.DEFENDER) {
            Destroy(obj);
        }
        else if(type == SummonType.BOMBER) {
            Destroy(obj);
        }
    }
}