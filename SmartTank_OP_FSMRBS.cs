using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

class Temp{};

public class SmartTank_OP_FSMRBS : AITank
{
    public Dictionary<GameObject, float> targetTanksFound = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> consumablesFound = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> basesFound = new Dictionary<GameObject, float>();

    public Vector3 initialTankPosition;
    public Vector3 calculatedTankPosition;

    public GameObject targetTankPosition;
    public GameObject consumablePosition;
    public GameObject basePosition;

    private Dictionary<Type, BaseState> states;

    public Dictionary<string, bool> stats = new Dictionary<string, bool>();
    public Rules rules = new Rules();

    /*******************************************************************************************************      
    WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
    *******************************************************************************************************/
    public override void AITankStart()
    {
        //This method runs once at the beginning when pressing play in Unity.
        InitStates();
        stats.Add("Enemy Found", false);
        stats.Add("Enemy Is Found", false);
        stats.Add("Has Ammo", false);
        stats.Add("Low Fuel", false);
        stats.Add("Low Ammo", false);
        stats.Add("Low Health", false);
        stats.Add("High Fuel", false);
        stats.Add("High Ammo", false);
        stats.Add("High Health", false);
        stats.Add("Find Base", false);

        stats.Add("Follow State", false);
        stats.Add("Wander State", false);
        stats.Add("Find Ammo State", false);
        stats.Add("Find Fuel State", false);
        stats.Add("Find Health State", false);
        stats.Add("Shoot State", false);
        stats.Add("Escape State", false);

        rules.AddRule(new Rule(new List<string>{"Enemy Found", "No Ammo"}, typeof(Escape), Rule.Predicate.And));
        rules.AddRule(new Rule(new List<string>{"Enemy Found", "Has Ammo"}, typeof(Shoot), Rule.Predicate.And));
        rules.AddRule(new Rule(new List<string>{"Enemy Found", "Enemy Is Far", "Ammo", "High Fuel", "High Health"}, typeof(Temp), Rule.Predicate.And));

    }

    /*******************************************************************************************************       
    WARNING, do not include void Update(), use AITankUpdate() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AITankUpdate()
    {
        targetTanksFound = GetAllTargetTanksFound;
        consumablesFound = GetAllConsumablesFound;
        basesFound = GetAllBasesFound;

        if(targetTanksFound.Count > 0 && targetTanksFound.First().Key != null){

        
        }

        //This method runs once per frame.
    }

    /*******************************************************************************************************       
    WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AIOnCollisionEnter(Collision collision)
    {
        //This method is used for detecting collisions (unlikley you will need this).
    }

    void InitStates(){
        states = new Dictionary<Type, BaseState>();

        ///states.Add(typeof(SearchState), new SearchState(this));

        //GetComponent<StateMachine>().SetStates(states);
    }
}
