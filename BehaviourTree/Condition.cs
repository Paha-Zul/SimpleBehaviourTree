using System;

namespace BehaviourTree
{
    /// <summary>
    /// A Decorator task that will check each update tick if the passed in condition is true. If true, the
    /// Condition task will return whatever the decorated task returns. Otherwise, it will return failure
    /// </summary>
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