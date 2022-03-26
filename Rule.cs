using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{
    public string antA;
    public string antB;
    public Type consequentState;
    public Predicate compare;

    public enum Predicate
    { And, Or, nAnd }

    public Rule(string antA, string antB, Type consequentState, Predicate compare){
        this.antA = antA;
        this.antB = antB;
        this.consequentState = consequentState;
        this.compare = compare;

    }

    public Type CheckRule(Dictionary<string, bool> stats){
        bool antABool = stats[antA];
        bool antBBool = stats[antB];
        
        switch(compare){

            case Predicate.And:
                if(antABool && antBBool){
                    return consequentState;

                } else
                {
                    return null;
                }

            case Predicate.Or:
            if(antABool || antBBool){
                return consequentState;
            }
            else{
                return null;
            }

            case Predicate.nAnd:
            if(!antABool || !antBBool){
                return consequentState;
            }
            else{
                return null;
            }

            default:
                return null;
            
        }
    }

}
