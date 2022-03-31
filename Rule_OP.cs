using System;
using System.Collections.Generic;
using System.Linq;

public class Rule_OP
{
    public List<List<string>> antecendants = new List<List<string>>();
    public Type consequentState;
    public Predicate compare;

    public enum Predicate
    { And, Or, And_nAnd, nAnd, And_nOr }

    // And &&
    // Or ||
    // Not And ! && !

    public Rule_OP(List<List<string>> antecendants, Type consequentState, Predicate compare){
        this.antecendants = antecendants;
        this.consequentState = consequentState;
        this.compare = compare;

    }

    public Type CheckRule(Dictionary<string, bool> stats){

        List<List<bool>> antecendantBools = new List<List<bool>>();
        foreach(List<string> list in antecendants){
            
            if(list.Count > 0){
                antecendantBools.Add(new List<bool>{});

                for(int i = 0; i < list.Count; ++i){
                    antecendantBools[antecendants.IndexOf(list)].Add(stats[list[i]]);
                }
            }

        }
        
        switch(compare){

            case Predicate.And:
                if(antecendantBools.First().All(x => x == true) && antecendantBools.Last().All(x => x == true)){ // if antecendant contains any false values, then it is true. then doing !true will make it false.
                    
                    return consequentState;

                } 
                else {

                    return null;

                }

            case Predicate.Or:

                if(antecendantBools.First().Contains(true) || antecendantBools.Last().Contains(true)){

                    return consequentState;
                }
                else{
                    
                    return null;
                }

            case Predicate.nAnd:
                if((antecendantBools.First().All(x => x == false) && antecendantBools.Last().All(x => x == false))){
                    
                    return consequentState;
                }
                else{

                    return null;
                    
                }

            case Predicate.And_nAnd:

                if(antecendantBools.First().All(x => x == true) && !antecendantBools.Last().All(x => x == false)){
                    return consequentState;

                } 
                else{
            
                    return null;
                
                }

            case Predicate.And_nOr:

                    if(antecendantBools.First().All(x => x == true)){
                        
                        if(antecendantBools.Last().Contains(true)){

                            return null;
                        
                        } 
                        else{

                            return consequentState;

                        }

                    }
                    else{
                        
                        return null;
                    }

            default:
                return null;
            
        }
    }



}
