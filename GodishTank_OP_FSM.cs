using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GodishTank_OP_FSM : AITank
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
        Dictionary<Type, BaseState_OP> states = new Dictionary<Type, BaseState_OP>
        {
            { typeof(Wander_OP), new Wander_OP(this) },
            { typeof(Escape_OP), new Escape_OP(this) },
            { typeof(Follow_OP), new Follow_OP(this) },
            { typeof(Shoot_OP), new Shoot_OP(this) },
            { typeof(FindAmmo_OP), new FindAmmo_OP(this) },
            { typeof(FindFuel_OP), new FindFuel_OP(this) },
            { typeof(FindHealth_OP), new FindHealth_OP(this) }
        };

        GetComponent<FSM_OP>().setStates(states);
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
        if (collision.gameObject.name.Contains("_Loc")) 
        {
            Destroy(collision.gameObject);
        }
    }

    private void GenerateLastKnownPoint(GameObject thing, float distance) //This adds the item to the dictionary by comparing game object names
    {
        GameObject point;
        if (thing.name == "Fuel") 
        { 
            point = new GameObject("FuelLocation_Loc");
            point.transform.position = thing.transform.position;
            point.AddComponent<BoxCollider>();
            potConsumableLocation.Add(point, distance);
        }
        else if(thing.name == "Ammo") 
        {
            point = new GameObject("AmmoLocation_Loc");
            point.transform.position = thing.transform.position;
            point.AddComponent<BoxCollider>();
            potConsumableLocation.Add(point, distance);
        }
        else if (thing.name == "Health")
        {
            point = new GameObject("HealthLocation_Loc");
            point.transform.position = thing.transform.position;
            point.AddComponent<BoxCollider>();
            potConsumableLocation.Add(point, distance);
        }
    }

    public void Wander() 
    {
        FollowPathToRandomPoint(1f);
    }

    public void findAmmo()
    {
        GameObject loc = null;
        float lowestDist = 1000.0f;
        foreach(KeyValuePair<GameObject,float> item in potConsumableLocation)
        {
            if(item.Key.name == "AmmoLocation_Loc" && item.Value < lowestDist)
            {
                lowestDist = item.Value;
                loc = item.Key;
            } 
        }
        if (loc != null)
        {
            FollowPathToPoint(loc, 1f);
        }
        
    }

    public void findFuel()
    {
        GameObject loc = null;
        float lowestDist = 1000.0f;
        foreach (KeyValuePair<GameObject, float> item in potConsumableLocation)
        {
            if (item.Key.name == "FuelLocation_Loc" && item.Value < lowestDist)
            {
                lowestDist = item.Value;
                loc = item.Key;
            }
        }
        if (loc != null)
        {
            FollowPathToPoint(loc, 1f);
        }
    }

    public void findHealth()
    {
        GameObject loc = null;
        float lowestDist = 1000.0f;
        foreach (KeyValuePair<GameObject, float> item in potConsumableLocation)
        {
            if (item.Key.name == "HealthLocation_Loc" && item.Value < lowestDist)
            {
                lowestDist = item.Value;
                loc = item.Key;
            }
        }
        if (loc != null)
        {
            FollowPathToPoint(loc, 1f);
        }
    }

    public float checkAmmo()
    {
        return GetAmmoLevel;
    }

    public float checkFuel()
    {
        return GetFuelLevel;
    }

    public float checkHealth()
    {
        return GetHealthLevel;
    }
}
