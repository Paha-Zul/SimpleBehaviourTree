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

        protected override BehaviourTreeStatus ChildSucceeded()
        {
            children[currIndex].End(); //End the current child
            currIndex++;

            if (currIndex >= children.Count) //If the index is out of the bounds of our children, return success because we're done with the sequence
                return BehaviourTreeStatus.Success; //Return success

            //If the child fails the check, return with failure
            if (!children[currIndex].Check())
                return BehaviourTreeStatus.Failure;

            children[currIndex].Start(); //Start the child

            return BehaviourTreeStatus.Running; //Return that we are still running
        }

        protected override BehaviourTreeStatus ChildFailed()
        {
            children[currIndex].End(); //End the child
            return BehaviourTreeStatus.Failure;
        }
    }
}
