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
    public class PostCondition : System.Attribute
    {
        public string Condition {get; set; }
        public string ConstructorVariable { get; set; }
        public int number { get; set; }

        public PostCondition(string condition)
        {
            this.Condition = condition;
        }
    }
}
