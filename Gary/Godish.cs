using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Godish : AITank
{
    public Dictionary<GameObject, float> targetTanksFound = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> consumablesFound = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> basesFound = new Dictionary<GameObject, float>();

    public Dictionary<GameObject, float> potConsumableLocation = new Dictionary<GameObject, float>();

    public GameObject targetTankPosition;
    public GameObject consumablePosition;
    public GameObject basePosition;

    private void StartStateMachine() 
    {
        Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>
        {
            { typeof(Wander), new Wander(this) },
            { typeof(Escape), new Escape(this) },
            { typeof(Follow), new Follow(this) },
            { typeof(Shoot), new Shoot(this) },
            { typeof(FindAmmo), new FindAmmo(this) },
            { typeof(FindFuel), new FindFuel(this) },
            { typeof(FindHealth), new FindHealth(this) }
        };

        GetComponent<FSM>().setStates(states);
    }

    /*******************************************************************************************************      
    WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
    *******************************************************************************************************/

    public override void AITankStart()
    {
        StartStateMachine();
    }

    /*******************************************************************************************************       
    WARNING, do not include void Update(), use AITankUpdate() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AITankUpdate()
    {
        //On every update the tank will stored last known location of consumables which will be stored as pot dictionary members
        consumablesFound = GetAllConsumablesFound;
        bool duplicate = false;
        foreach(KeyValuePair<GameObject,float> consumable in consumablesFound) //It iterates through all the consumables found and stores them into it as long its not a dupe
        {
            foreach(GameObject item in potConsumableLocation.Keys) 
            {
                if(item.transform.position == consumable.Key.transform.position) 
                {
                    duplicate = true;
                }
            }
            if (!duplicate) 
            {
                GenerateLastKnownPoint(consumable.Key, consumable.Value);
                duplicate = false;
            }
        }
        consumablesFound = null;
    }

    /*******************************************************************************************************       
    WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AIOnCollisionEnter(Collision collision)
    {

    }

    private void GenerateLastKnownPoint(GameObject thing, float distance) //This adds the item to the dictionary by comparing game object names
    {
        GameObject point;
        if (thing.name == "Fuel") 
        { 
            point = new GameObject("FuelLocation");
            point.transform.position = thing.transform.position;
            potConsumableLocation.Add(point, distance);
        }
        else if(thing.name == "Ammo") 
        {
            point = new GameObject("AmmoLocation");
            point.transform.position = thing.transform.position;
            potConsumableLocation.Add(point, distance);
        }
        else if (thing.name == "Health")
        {
            point = new GameObject("HealthLocation");
            point.transform.position = thing.transform.position;
            potConsumableLocation.Add(point, distance);
        }
    }

    public void Wander() 
    {
        FollowPathToRandomPoint(1f);
    }
}
