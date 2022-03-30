using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GodishTank_OP_FSMRBS : AITank
{
    public Dictionary<GameObject, float> targetTanksFound = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> consumablesFound = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> basesFound = new Dictionary<GameObject, float>();

    public List<GameObject> potConsumableLocation = new List<GameObject>();


    public GameObject targetTankPosition;
    public GameObject consumablePosition;
    public GameObject basePosition;
    public List<GameObject> fixedPoints = new List<GameObject>();

    private List<Vector3> points = new List<Vector3>() { new Vector3(-65,0,49), new Vector3(-65,0,-71), new Vector3(57,0,-71), new Vector3(57,0,60) };
    private int curPointLoc = 0;

    public Dictionary<string, bool> stats = new Dictionary<string, bool>();
    public Rules_OP rules = new Rules_OP();

    private void StartStateMachine() 
    {
        Dictionary<Type, BaseState_OPRBS> states = new Dictionary<Type, BaseState_OPRBS>
        {
            { typeof(Wander_OPRBS), new Wander_OPRBS(this) },
            { typeof(Escape_OPRBS), new Escape_OPRBS(this) },
            { typeof(Follow_OPRBS), new Follow_OPRBS(this) },
            { typeof(Shoot_OPRBS), new Shoot_OPRBS(this) },
            { typeof(FindAmmo_OPRBS), new FindAmmo_OPRBS(this) },
            { typeof(FindFuel_OPRBS), new FindFuel_OPRBS(this) },
            { typeof(FindHealth_OPRBS), new FindHealth_OPRBS(this) }
        };

        GetComponent<FSM_OPRBS>().setStates(states);
    }

    /*******************************************************************************************************      
    WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
    *******************************************************************************************************/

    public override void AITankStart()
    {
        StartStateMachine();
        fixedPoints.Add(new GameObject("Point_0_OP"));
        fixedPoints[0].transform.position = points[0];
        fixedPoints.Add(new GameObject("Point_1_OP"));
        fixedPoints[1].transform.position = points[1];
        fixedPoints.Add(new GameObject("Point_2_OP"));
        fixedPoints[2].transform.position = points[2];
        fixedPoints.Add(new GameObject("Point_3_OP"));
        fixedPoints[3].transform.position = points[3];

        stats.Add("Enemy Found", false);
        stats.Add("Has Ammo", false);
        stats.Add("Low Fuel", false);
        stats.Add("Low Ammo", false);
        stats.Add("Low Health", false);
        stats.Add("High Fuel", false);
        stats.Add("High Ammo", false);
        stats.Add("High Health", false);
        stats.Add("Find Base", false);
        stats.Add("Ammo Found", false);
        stats.Add("Health Found", false);
        stats.Add("Fuel Found", false);

        stats.Add("Follow State", false);
        stats.Add("Wander State", false);
        stats.Add("Find Ammo State", false);
        stats.Add("Find Fuel State", false);
        stats.Add("Find Health State", false);
        stats.Add("Shoot State", false);
        stats.Add("Escape State", false);

        rules.AddRule(new Rule_OP(new List<string> { "Enemy Found", "No Ammo" }, typeof(Escape_OPRBS), Rule_OP.Predicate.And));
        rules.AddRule(new Rule_OP(new List<string> { "Enemy Found", "Has Ammo" }, typeof(Shoot_OPRBS), Rule_OP.Predicate.And));
        rules.AddRule(new Rule_OP(new List<string> { "Enemy Found", "Enemy Is Far", "Ammo", "High Fuel", "High Health" }, typeof(Follow_OPRBS), Rule_OP.Predicate.And));
    }

    /*******************************************************************************************************       
    WARNING, do not include void Update(), use AITankUpdate() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AITankUpdate()
    {
        //On every update the tank will stored last known location of consumables which will be stored as pot dictionary members
        consumablesFound = GetAllConsumablesFound;
        targetTanksFound = GetAllTargetTanksFound;
        bool duplicate = false;
        foreach(GameObject consumable in consumablesFound.Keys) //It iterates through all the consumables found and stores them into it as long its not a dupe
        {
            foreach(GameObject item in potConsumableLocation) 
            {
                if(Vector3.Distance(item.gameObject.transform.position, consumable.transform.position) < 5f) 
                {
                    duplicate = true;
                }
            }
            if (!duplicate) 
            {
                GenerateLastKnownPoint(consumable);
                duplicate = false;
            }
        }
        consumablesFound = null;

        foreach (GameObject item in potConsumableLocation)
        {
            if (Vector3.Distance(item.gameObject.transform.position, transform.position) < 5f)
            {
                Destroy(item);
                potConsumableLocation.Remove(item);
                Debug.Log("destroyed");
            }
        }

        InventoryCheck();
        checkAmmo();
        checkFuel();
        checkHealth();
    }

    /*******************************************************************************************************       
    WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AIOnCollisionEnter(Collision collision)
    {
    }
    //Gary
    private void GenerateLastKnownPoint(GameObject thing) //This adds the item to the dictionary by comparing game object names
    {
        GameObject point;
        if (thing.name == "Fuel") 
        { 
            point = new GameObject("FuelLocation_Loc");
            point.transform.position = thing.transform.position;
            potConsumableLocation.Add(point);
        }
        else if(thing.name == "Ammo") 
        {
            point = new GameObject("AmmoLocation_Loc");
            point.transform.position = thing.transform.position;
            potConsumableLocation.Add(point);
        }
        else if (thing.name == "Health")
        {
            point = new GameObject("HealthLocation_Loc");
            point.transform.position = thing.transform.position;
            potConsumableLocation.Add(point);
        }
    }

    void InventoryCheck() 
    {
        foreach(GameObject item in potConsumableLocation) 
        {
            if(item.gameObject.name == "AmmoLocation_Loc") { stats["Ammo Found"] = true; }
            else { stats["Ammo Found"] = false; }
            if (item.gameObject.name == "FuelLocation_Loc") { stats["Fuel Found"] = true; }
            else { stats["Fuel Found"] = false; }
            if (item.gameObject.name == "HealthLocation_Loc") { stats["Health Found"] = true; }
            else { stats["Health Found"] = false; }
        }
    }

    //Gary
    private void CirculateTank() 
    {
        FollowPathToPoint(fixedPoints[curPointLoc], 1f);
        if (Vector3.Distance(transform.position, fixedPoints[curPointLoc].transform.position) < 10f)
        {
            if (curPointLoc < 3)
            {
                curPointLoc += 1;
            }
            else
            {
                curPointLoc = 0;
            }
        }
    }

    //Gary
    public void Wander() 
    {
        CirculateTank();
        stats["Enemy Found"] = false;
        targetTanksFound = GetAllTargetTanksFound;
        if(targetTanksFound.Count > 0 && targetTanksFound.First().Key != null) 
        {
            targetTankPosition = targetTanksFound.First().Key;
        }
    }

    //Gary
    public void Follow() 
    {
        stats["Enemy Found"] = true;
        FollowPathToPoint(targetTankPosition,1f);
    }

    //George
    public void findAmmo()
    {
        GameObject loc = null;
        float lowestDist = 1000.0f;
        foreach (GameObject item in potConsumableLocation)
        {
            if (item.name == "AmmoLocation_Loc" && Vector3.Distance(transform.position, item.transform.position) < lowestDist)
            {
                lowestDist = Vector3.Distance(transform.position, item.transform.position);
                loc = item;
            }
        }
        if (loc != null)
        {
            FollowPathToPoint(loc, 1f);
        }
    }

    //George
    public void findFuel()
    {
        GameObject loc = null;
        float lowestDist = 1000.0f;
        foreach (GameObject item in potConsumableLocation)
        {
            if (item.name == "FuelLocation_Loc" && Vector3.Distance(transform.position, item.transform.position) < lowestDist)
            {
                lowestDist = Vector3.Distance(transform.position, item.transform.position);
                loc = item;
            }
        }
        if (loc != null)
        {
            FollowPathToPoint(loc, 1f);
        }
    }

    //George
    public void findHealth()
    {
        GameObject loc = null;
        float lowestDist = 1000.0f;
        foreach (GameObject item in potConsumableLocation)
        {
            if (item.name == "HealthLocation_Loc" && Vector3.Distance(transform.position, item.transform.position) < lowestDist)
            {
                lowestDist = Vector3.Distance(transform.position, item.transform.position);
                loc = item;
            }
        }
        if (loc != null)
        {
            FollowPathToPoint(loc, 1f);
        }
    }
    //George
    public float checkAmmo()
    {   
        if(GetAmmoLevel >= 10) { stats["High Ammo"] = true; }
        else { stats["High Ammo"] = false; }
        if(GetAmmoLevel == 0) { stats["Has Ammo"] = false; }
        else { stats["Has Ammo"] = true; }

        return GetAmmoLevel;
    }
    //George
    public float checkFuel()
    {
        if(GetFuelLevel > 75) { stats["High Fuel"] = true; }
        else { stats["High Fuel"] = false; }
        if (GetFuelLevel < 25){ stats["Low Fuel"] = true; }
        else { stats["Low Fuel"] = false; }
        return GetFuelLevel;
    }
    //George
    public float checkHealth()
    {
        if(GetHealthLevel > 75) { stats["High Health"] = true; }
        else { stats["High Health"] = false; }
        if (GetHealthLevel < 25) { stats["Low Health"] = true; }
        else { stats["Low Health"] = false; }

        return GetHealthLevel;
    }
    //Robbie
    public void shoot()
    {
        if (targetTankPosition != null)
        {
            FireAtPoint(targetTankPosition);
        }
    }
    public void escape()
    {
        FollowPathToRandomPoint(1f);
        targetTanksFound = GetAllTargetTanksFound;
        if (targetTanksFound.Count > 0 && targetTanksFound.First().Key != null)
        {
            targetTankPosition = targetTanksFound.First().Key;
        }
    }
}
