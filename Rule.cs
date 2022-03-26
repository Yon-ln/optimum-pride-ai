using System;
using System.Collections.Generic;

public class Rule
{
    public string antA;
    public string antB;
    public List<string> antecendants = new List<string>();
    public Type consequentState;
    public Predicate compare;

    public enum Predicate
    { And, Or, nAnd }

    public Rule(List<string> antecendants, Type consequentState, Predicate compare){
        this.antecendants = antecendants;
        this.consequentState = consequentState;
        this.compare = compare;

    }

    public Type CheckRule(Dictionary<string, bool> stats){
        List<bool> antecendantBools = new List<bool>();
        foreach(string i in antecendants){
            antecendantBools.Add(stats[i]);
        }
        
        switch(compare){

            case Predicate.And:
                if(!antecendantBools.Contains(false)){ // if antecendant contains any false values, then it is true. then doing !true will make it false.
                    return consequentState;

                } else
                {
                    return null;
                }

            case Predicate.Or:
            if(antecendantBools.Contains(true)){
                return consequentState;
            }
            else{
                return null;
            }

            case Predicate.nAnd:
            if(!antecendantBools.Contains(true)){
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
