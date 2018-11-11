using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    public class SequenceTask : CompositeTask
    {
        public SequenceTask(string taskName = "") : base(taskName)
        {
        }

        protected override BehaviourTreeStatus ChildSucceeded(BlackBoard blackboard)
        {
            children[currIndex].End(blackboard); //End the current child
            currIndex++;

            if (currIndex >= children.Count) //If the index is out of the bounds of our children, return success because we're done with the sequence
                return BehaviourTreeStatus.Success; //Return success

            //If the child fails the check, return with failure
            if (!children[currIndex].Check(blackboard))
                return BehaviourTreeStatus.Failure;

            children[currIndex].Start(blackboard); //Start the child

            return BehaviourTreeStatus.Running; //Return that we are still running
        }

        protected override BehaviourTreeStatus ChildFailed(BlackBoard blackboard)
        {
            children[currIndex].End(blackboard); //End the child
            return BehaviourTreeStatus.Failure;
        }
    }
}
