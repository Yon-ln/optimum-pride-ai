using System.Collections.Generic;


public class Rules_OP
{
    public void AddRule(Rule_OP rule){
        GetRules.Add(rule);
    }

    public List<Rule_OP> GetRules { get; } = new List<Rule_OP>();
}
