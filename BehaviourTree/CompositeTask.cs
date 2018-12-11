using System.Collections.Generic;

namespace BehaviourTree
{
    public abstract class CompositeTask : Task
    {
        protected List<Task> children = new List<Task>();
        protected int currIndex;

        protected CompositeTask(string taskName = "") : base(taskName)
        {
        }

        public override void Start(BlackBoard blackboard)
        {
            base.Start(blackboard);

            children[currIndex].Start(blackboard);
            //TODO What happens here with an empty sequence?
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            base.Update(blackboard, deltaTime);

            if (currIndex < 0 || currIndex >= children.Count)
                return BehaviourTreeStatus.Failure;

            var childStatus = children[currIndex].Update(blackboard, deltaTime);
            if (childStatus == BehaviourTreeStatus.Success)
                return ChildSucceeded(blackboard);

            if (childStatus == BehaviourTreeStatus.Failure)
                return ChildFailed(blackboard);

            return childStatus;
        }

        /// <summary>
        /// Called when the current child succeeds
        /// </summary>
        protected abstract BehaviourTreeStatus ChildSucceeded(BlackBoard blackboard);

        /// <summary>
        /// Called when the current child fails
        /// </summary>
        protected abstract BehaviourTreeStatus ChildFailed(BlackBoard blackboard);

        public void AddChildTask(Task task)
        {
            children.Add(task);
        }

        public override void End(BlackBoard blackboard)
        {
            if(currIndex >= 0)
                //End each child from the current index forward. We already ended ones before that so ignore
                for(var i=currIndex;i<children.Count;i++)
                    children[i].End(blackboard);
        }

        public override void Reset(BlackBoard blackboard)
        {
            base.Reset(blackboard);
            children.ForEach(x => x.Reset(blackboard));
            currIndex = 0;
        }

        public override string ToString()
        {
            return $"{taskName} / {children[currIndex]}";
        }

        public override Task GetCurrentChildTask() => currIndex >= 0 ? children[currIndex] : null;

        public override void SetCurrIndex(int index) => currIndex = index;

        public override int GetCurrIndex() => currIndex;

        public override List<Task> GetChildren() => children;
    }
}
