using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules
{
    public void AddRule(Rule rule) 
    {
        GetRules.Add(rule);
    }
    public List<Rule> GetRules { get; } = new List<Rule>();
}

public class Rule
{
    public string atcA;
    public string atcB;

    public Type consState;
    public Predicate compare;
    public enum Predicate { And, Or, nAnd };

    public Rule(string atcA, string atcB, Type consState, Predicate compare) 
    {
        this.atcA = atcA;
        this.atcB = atcB;
        this.consState = consState;
        this.compare = compare;
    }

    public Type CheckRule(Dictionary<string, bool> stats) 
    {
        bool atcABool = stats[atcA];
        bool atcBBool = stats[atcB];

        switch (compare)
        {
            case Predicate.And:
                if (atcABool && atcABool)
                {
                    return consState;
                }
                else 
                {
                    return null;
                }
            case Predicate.Or:
                if(atcABool || atcBBool) 
                {
                    return consState;
                }
                else 
                {
                    return null;
                }
            case Predicate.nAnd:
                if(!atcABool && !atcBBool) 
                {
                    return consState;
                }
                else 
                {
                    return null;
                }
            default:
                return null;
        }
    }
}