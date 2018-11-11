using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    public abstract class Decorator : Task
    {

        protected Task childTask;

        protected Decorator(Task child, string taskName = "") : base(taskName)
        {
            this.childTask = child;
        }

        public override bool Check(BlackBoard blackboard)
        {
            return childTask.Check(blackboard);
        }

        public override void Start(BlackBoard blackboard)
        {
            childTask.Start(blackboard);
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            return childTask.Update(blackboard, deltaTime);
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
