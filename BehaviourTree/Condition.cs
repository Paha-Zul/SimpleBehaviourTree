using System;

namespace BehaviourTree
{
    public class Condition : Decorator
    {
        private Func<BlackBoard, bool> _cond;
        public Condition(Task child, Func<BlackBoard, bool> cond, string taskName = "") : base(child, taskName)
        {
            _cond = cond;
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            return _cond(blackboard) ? base.Update(blackboard, deltaTime) : BehaviourTreeStatus.Failure;
        }
    }
}