﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    public class Decorator : Task
    {

        protected Task childTask;

        public Decorator(Task child, string taskName = "") : base(taskName)
        {
            this.childTask = child;
        }

        public override bool Check()
        {
            return childTask.Check();
        }

        public override void Start(BlackBoard blackboard)
        {
            childTask.Start(blackboard);
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard)
        {
            return childTask.Update(blackboard);
        }

        public override void End(BlackBoard blackboard)
        {
            childTask.End(blackboard);
        }

        public override void Reset(BlackBoard blackboard)
        {
            childTask.Reset(blackboard);
        }

        public override string ToString()
        {
            return $"{taskName} decorating {childTask}";
        }
    }
}
