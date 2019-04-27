using System;

namespace BehaviourTree
{
    public class RepeatUntilCondition : Decorator
    {
        private Func<BlackBoard, bool> _cond;

        
        public RepeatUntilCondition(Task child, Func<BlackBoard, bool> cond, string taskName = "") : base(child, taskName)
        {
            _cond = cond;
        }
        
        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            //If our condition returns true, then we're done with this task. Return success!
            if (_cond(blackboard))
                return BehaviourTreeStatus.Success;
            
            //Otherwise, update the child and get it's status
            var status = childTask.Update(blackboard, deltaTime);
            //If the status is not success... simply return the status of the child 
            if (status != BehaviourTreeStatus.Success && status != BehaviourTreeStatus.Failure)
                return status;
            
            //If the child status is success or failure, we need to repeat (so reset() and start()
            childTask.Reset(blackboard);
            childTask.Start(blackboard);
            return BehaviourTreeStatus.Running;
        }
    }
}