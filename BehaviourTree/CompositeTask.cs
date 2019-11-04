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
        
        public override bool Check(BlackBoard blackboard)
        {
            return currIndex < children.Count;
        }

        public override void Start(BlackBoard blackboard)
        {
            base.Start(blackboard);

            if (children[currIndex].Check(blackboard))
                children[currIndex].Start(blackboard);
            //TODO What happens here with an empty sequence?
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            base.Update(blackboard, deltaTime);

            if (currIndex < 0 || currIndex >= children.Count)
                return BehaviourTreeStatus.Failure;

            var childStatus = RunChildTask(blackboard, deltaTime);

            return childStatus;
        }

        private BehaviourTreeStatus RunChildTask(BlackBoard blackboard, float deltaTime)
        {
            while (true)
            {
                var childStatus = children[currIndex].Update(blackboard, deltaTime);

                //This will start the next child in our children list
                if (childStatus == BehaviourTreeStatus.Success)
                {
                    childStatus = ChildSucceeded(blackboard);
                    if (childStatus == BehaviourTreeStatus.Running)
                        continue;
                }
                else if (childStatus == BehaviourTreeStatus.Failure)
                {
                    childStatus = ChildFailed(blackboard);
                    if (childStatus == BehaviourTreeStatus.Running)
                        continue;
                }

                return childStatus;
            }
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
            if (currIndex < 0) return;
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
            var childName = currIndex < children.Count ? children[currIndex].ToString() : "end"; //This accounts for tasks that finish and the index is out of bounds
            return $"{taskName} / {childName}";
        }

        public override Task GetCurrentChildTask() => currIndex >= 0 && currIndex < children.Count ? children[currIndex] : null;

        public override void SetCurrIndex(int index) => currIndex = index;

        public override int GetCurrIndex() => currIndex;

        public override List<Task> GetChildren() => children;
    }
}
