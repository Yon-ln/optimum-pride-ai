using System;
using System.Collections.Generic;

public class Rule_OP
{
    public List<string> antecendants = new List<string>();
    public Type consequentState;
    public Predicate compare;

    public enum Predicate
    { And, Or, nAnd, Implies }

    // And &&
    // Or ||
    // Not And ! && !
    // Implies A => B, this means if a is true, then b has to be true, if a is false, then b can be true or false
    // In this case {A, B, C, D} If a is true, then one of the values {B, C, D} has to be true.

    public Rule_OP(List<string> antecendants, Type consequentState, Predicate compare){
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
                if(antecendantBools.Contains(true)){
                    return null;
                }
                else{
                    return consequentState;
                }

            case Predicate.Implies:
                if(antecendantBools[0]){
                   List<bool> antecendantExclusions = new List<bool>(antecendantBools);
                   antecendantExclusions.RemoveAt(0);
                   
                   if(antecendantExclusions.Contains(true)){
                       return consequentState;
                   } else{
                       return null;
                   }

                }else{
                    return null;
                }
            

            default:
                return null;
            
        }
    }



}
