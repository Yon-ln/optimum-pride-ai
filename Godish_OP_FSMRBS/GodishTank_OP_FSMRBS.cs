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
    public GameObject targetDodgePosition;
    public GameObject consumablePosition;
    public GameObject basePosition;
    public List<GameObject> fixedPoints = new List<GameObject>();
    public Vector3[] strafePositions = new Vector3[2];

    private int curPointLoc = 0;

    public Dictionary<string, bool> stats = new Dictionary<string, bool>();
    public List<bool> statsB = new List<bool>();
    public Rules_OP rules = new Rules_OP();
    private bool isMoving = false;
    public float a = 0.0f;

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

        GetComponent<StateMachine_OP_FSMRBS>().SetStates(states);
    }

    /*******************************************************************************************************      
    WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
    *******************************************************************************************************/

    public override void AITankStart()
    {
        StartStateMachine();
        fixedPoints.Add(new GameObject("Point_0_OP"));
        fixedPoints.Add(new GameObject("Point_1_OP"));
        fixedPoints.Add(new GameObject("Point_2_OP"));
        fixedPoints.Add(new GameObject("Point_3_OP"));
        RegneratePoints();

        targetDodgePosition = new GameObject("TDP");
        
        stats.Add("Enemy Found", false);        //0
        stats.Add("Enemy Nearby", false);       //1
        stats.Add("Has Ammo", false);           //2
        stats.Add("Low Fuel", false);           //3
        stats.Add("Low Ammo", false);           //4
        stats.Add("Low Health", false);         //5
        stats.Add("Enemy Base Found", false);   //6
        stats.Add("Ammo Found", false);         //7
        stats.Add("Health Found", false);       //8
        stats.Add("Fuel Found", false);         //9
        stats.Add("Turret Shot", false);        //10
        stats.Add("Follow State", false);       //11
        stats.Add("Wander State", true);        //12
        stats.Add("Find Ammo State", false);    //13
        stats.Add("Find Fuel State", false);    //14
        stats.Add("Find Health State", false);  //15
        stats.Add("Shoot State", false);        //16
        stats.Add("Escape State", false);       //17
        stats.Add("Strafing", false);           //18


        AddRule(new List<string>{"Wander State"}, new List<string>{"Fuel Found"}, typeof(FindFuel_OPRBS), Rule_OP.Predicate.And);
        AddRule(new List<string>{"Wander State", "Health Found", "Low Health"}, new List<string>{}, typeof(FindHealth_OPRBS), Rule_OP.Predicate.And);
        AddRule(new List<string>{"Wander State", "Ammo Found", "Low Ammo"}, new List<string>{}, typeof(FindAmmo_OPRBS), Rule_OP.Predicate.And);

        AddRule(new List<string>{"Shoot State"}, new List<string>{"Low Fuel", "Low Health", "Low Ammo"}, typeof(Escape_OPRBS), Rule_OP.Predicate.And_nOr);
        AddRule(new List<string>{"Shoot State", "Turret Shot"}, new List<string>{"Low Fuel"}, typeof(Escape_OPRBS), Rule_OP.Predicate.And_nAnd);
        AddRule(new List<string>{"Turret Shot", "Escape State"}, new List<string>{"Strafing"}, typeof(Shoot_OPRBS), Rule_OP.Predicate.And_nAnd); // If the turret is shooting and it knows where the fuel is, it will turn to the escape state to dodge bullets from the enemy.
        AddRule(new List<string>{"Enemy Found"}, new List<string>{"Low Fuel", "Low Health", "Low Ammo"}, typeof(Follow_OPRBS), Rule_OP.Predicate.And_nOr);

        Debug.Log(stats.Count);
        
    }

    /*******************************************************************************************************       
    WARNING, do not include void Update(), use AITankUpdate() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AITankUpdate()
    {
        //On every update the tank will stored last known location of consumables which will be stored as psot dictionary members
        consumablesFound = GetAllConsumablesFound;
        targetTanksFound = GetAllTargetTanksFound;
        bool duplicate = false;

        checkAmmo();
        checkFuel();
        checkHealth();
        statsB = stats.Values.ToList(); // Checking which stats are active
    
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

        for(int i =  0; i < potConsumableLocation.Count(); ++i){
            GameObject item = potConsumableLocation[i];
            if (Vector3.Distance(item.gameObject.transform.position, transform.position) < 5f)
            {   
                potConsumableLocation.RemoveAt(i);
                Destroy(item);
                i--;

                Debug.Log("Destroyed");
            }
        }




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
                RegneratePoints();
            }
        }
    }

    private void RegneratePoints() 
    {
        fixedPoints[0].transform.position = new Vector3(UnityEngine.Random.Range(-34,-47),0,UnityEngine.Random.Range(35,67));
        fixedPoints[1].transform.position = new Vector3(UnityEngine.Random.Range(-47, -70),0, UnityEngine.Random.Range(-34, -73));
        fixedPoints[2].transform.position = new Vector3(UnityEngine.Random.Range(50, 66),0, UnityEngine.Random.Range(-41, -71));
        fixedPoints[3].transform.position = new Vector3(UnityEngine.Random.Range(25, 60),0, UnityEngine.Random.Range(30, 67));
    }

    //Gary
    public void Wander() 
    {
        CirculateTank();

        targetTanksFound = GetAllTargetTanksFound;
        if(targetTanksFound.Count > 0 && targetTanksFound.First().Key != null) 
        {
            
            targetTankPosition = targetTanksFound.First().Key;
        }
    }

    //Gary
    public void Follow() 
    {
        FollowPathToPoint(targetTankPosition ,1f);
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

    public bool checkConsumables(string name){
        foreach(GameObject item in potConsumableLocation){
            if(item.name == name){
                return true;
            }
        }

        return false;
    }

    //George
    public float checkAmmo()
    {   
        if(GetAmmoLevel == 0) { stats["Has Ammo"] = false; }
        else { stats["Has Ammo"] = true; }

        return GetAmmoLevel;
    }
    //George
    public float checkFuel()
    {
        if (GetFuelLevel < 60){ stats["Low Fuel"] = true; }
        else { stats["Low Fuel"] = false; }
        return GetFuelLevel;
    }
    //George
    public float checkHealth()
    {
        if (GetHealthLevel < 40) { stats["Low Health"] = true; }
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

    public void Escape()
    {
        
        
    
    }


    //Yon
    public void Strafe()
    {   
        if(targetTankPosition != null){
            Debug.DrawLine(strafePositions[0], strafePositions[1], Color.red);

            targetDodgePosition.transform.position = strafePositions[0];

            FollowPathToPoint(targetDodgePosition, 0.25f);

            FaceTurretToPoint(targetTankPosition.transform.position);
        }

        if(Vector3.Distance(strafePositions[0], transform.position) < 3.0f) { 
            stats["Strafing"] = false;
        }

    }

    public void AddRule(List<string> A, List<string> B, Type state, Rule_OP.Predicate predicate){
         rules.AddRule(new Rule_OP(
             new List<List<string>> { A, B },
             state,
             predicate
         ));
    }
}