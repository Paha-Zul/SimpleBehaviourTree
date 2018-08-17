using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    abstract class CompositeTask : Task
    {
        protected List<Task> children = new List<Task>();
        protected int currIndex = 0;

        public CompositeTask(string taskName = "") : base(taskName)
        {
        }

        public override void Start()
        {
            base.Start();

            children[currIndex].Start();
            //TODO What happens here with an empty sequence?
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard)
        {
            base.Update(blackboard);

            var childStatus = children[currIndex].Update(blackboard);
            if (childStatus == BehaviourTreeStatus.Success)
                return ChildSucceeded();

            if (childStatus == BehaviourTreeStatus.Failure)
                return ChildFailed();

            return childStatus;
        }

        /// <summary>
        /// Called when the current child succeeds
        /// </summary>
        protected abstract BehaviourTreeStatus ChildSucceeded();

        /// <summary>
        /// Called when the current child fails
        /// </summary>
        protected abstract BehaviourTreeStatus ChildFailed();

        public void AddChildTask(Task task)
        {
            children.Add(task);
        }

        public override void Reset()
        {
            base.Reset();
            children.ForEach(x => x.Reset());
            currIndex = 0;
        }

        public override string ToString()
        {
            return $"{taskName} / {children[currIndex]}";
        }
    }
}
