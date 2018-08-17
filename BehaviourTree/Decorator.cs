using System;
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

        public override void Start()
        {
            childTask.Start();
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard)
        {
            return childTask.Update(blackboard);
        }

        public override void End()
        {
            childTask.End();
        }

        public override void Reset()
        {
            childTask.Reset();
        }

        public override string ToString()
        {
            return $"{taskName} decorating {childTask}";
        }
    }
}
