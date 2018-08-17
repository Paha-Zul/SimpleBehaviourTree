using System;

namespace BehaviourTree
{
    class QuickLeafTask : LeafTask
    {
        private Func<BlackBoard, float, BehaviourTreeStatus> fn;

        public QuickLeafTask(Func<BlackBoard, float, BehaviourTreeStatus> fn, string taskName = "") : base(taskName)
        {
            this.fn = fn;
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float delta)
        {
            return fn(blackboard, delta);
        }
    }
}
