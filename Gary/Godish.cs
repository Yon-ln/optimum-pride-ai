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
        
    }

    /*******************************************************************************************************       
    WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
    *******************************************************************************************************/
    public override void AIOnCollisionEnter(Collision collision)
    {

    }
}
