using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConditions
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct |
                           System.AttributeTargets.Method |
                           System.AttributeTargets.Constructor,
                           AllowMultiple = true)
    ]
    public class PreCondition : PostCondition
    {
        public PreCondition(int number, string condition):base(condition)
        {
            this.Condition = condition;
            this.number = number;
        }
    }

}
