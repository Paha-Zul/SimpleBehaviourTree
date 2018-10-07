﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    public abstract class CompositeTask : Task
    {
        protected List<Task> children = new List<Task>();
        protected int currIndex = -1;

        protected CompositeTask(string taskName = "") : base(taskName)
        {
        }

        public override void Start(BlackBoard blackboard)
        {
            base.Start(blackboard);

            currIndex = 0;

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
    }
}
