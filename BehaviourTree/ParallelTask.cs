using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    /// <summary>
    /// A Composite task that runs all child tasks at once.
    /// </summary>
    public class ParallelTask : CompositeTask
    {
        public int numNeededToPass;
        public int numNeededToFail;

        /// <summary>
        /// A Composite task that runs all child tasks at the same time. There are optional parameters to specify when this task should end.
        /// </summary>
        /// <param name="numRequiredToFail"> The number of child tasks that are needed to fail in order to end this ParallelTask</param>
        /// <param name="numRequiredToSucceed">The number of child tasks that are needed to succeed in order to end this ParallelTask</param>
        /// <param name="taskName">The name of the task</param>
        public ParallelTask(int numRequiredToFail = 99999, int numRequiredToSucceed = 99999, string taskName = "") : base(taskName)
        {
            this.numNeededToPass = numRequiredToSucceed;
            this.numNeededToFail = numRequiredToFail;
        }

        public override void Start(BlackBoard blackboard)
        {
            //base.Start(blackboard);
            //We don't call base.Start() because Parallel is special and doesn't need the typical Composite start

            children.ForEach(x => x.Start(blackboard));
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            var numSucceeded = 0; //A counter for succeeded tasks
            var numFailed = 0; //A counter for failed tasks 
            foreach(Task task in children) //Loop through and update each task.
            {
                var status = task.Update(blackboard, deltaTime);
                if (status == BehaviourTreeStatus.Success) //If it succeeds, increment counter
                    numSucceeded++;
                if (status == BehaviourTreeStatus.Failure) //If it fails, increment counter
                    numFailed++;
            }

            //If either of the checks pass then we are successful!
            if (numSucceeded >= numNeededToPass || numFailed >= numNeededToFail)
                return BehaviourTreeStatus.Success;

            //Otherwise keep running
            return BehaviourTreeStatus.Running;
            
        }

        public override void End(BlackBoard blackboard)
        {
            base.End(blackboard);
            children.ForEach(task => task.End(blackboard));
        }

        protected override BehaviourTreeStatus ChildFailed(BlackBoard blackboard)
        {
            throw new NotImplementedException();
        }

        protected override BehaviourTreeStatus ChildSucceeded(BlackBoard blackboard)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return children.ToString();
        }
    }
}
