using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    /// <summary>
    /// Always returns Success for a task.
    /// </summary>
    class AlwaysTrueTask : Decorator
    {
        public AlwaysTrueTask(Task child, string taskName = "") : base(child, taskName)
        {
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard)
        {
            var status = childTask.Update(blackboard);
            if (status != BehaviourTreeStatus.Running)
                return BehaviourTreeStatus.Success;

            return status;
        }
    }
}
