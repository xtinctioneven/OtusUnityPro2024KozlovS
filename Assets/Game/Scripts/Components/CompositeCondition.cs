using System;
using System.Collections.Generic;

public class CompositeCondition
{
    private readonly HashSet<Func<bool>> _conditions = new();

    public HashSet<Func<bool>> Append(Func<bool> condition)
    {
        _conditions.Add(condition);
        return _conditions;
    }

    public bool IsTrue()
    {
        foreach (var condition in _conditions)
        {
            if (!condition.Invoke())
            {
                return false;
            }
        }
        return true;
    }
}