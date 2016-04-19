using UnityEngine;
using System.Collections;

//Basic Enemy Controller
public class SummonController : Controller
{
    private SummonManager summoner;
    private SummonType type;

    protected override void Awake() {
        base.Awake();

        //SetOpponent - Used in sensors to detect players and summons
        setOpponent(1 << LayerMask.NameToLayer("Enemies"));
    }
    
    //Initialize controller and parent
    protected override void Start() {
        base.Start();

        summoner = GameObject.Find("Player").GetComponent<SummonManager>();
    }

    //Update is called once per frame
    protected override void Update() {
        base.Update(); //Call parent update
    }

    public SummonType getSummonType() {
        return type;
    }

    public void setSummonType(SummonType type) {
        this.type = type;
    }

    public void destroy() {
        summoner.destroySummon(gameObject, type);
    }
}
