
using System.Collections.Generic;

namespace BehaviourTree
{
    public abstract class Decorator : Task
    {

        protected Task childTask;

        protected Decorator(Task child, string taskName = "") : base(taskName)
        {
            this.childTask = child;
        }

        public override bool Check(BlackBoard blackboard)
        {
            return childTask.Check(blackboard);
        }

        public override void Start(BlackBoard blackboard)
        {
            childTask.Start(blackboard);
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            return childTask.Update(blackboard, deltaTime);
        }

        public override void End(BlackBoard blackboard)
        {
            childTask.End(blackboard);
        }

        public override void Reset(BlackBoard blackboard)
        {
            childTask.Reset(blackboard);
        }

        public override string ToString()
        {
            return $"{taskName} decorating {childTask}";
        }

        public override Task GetCurrentChildTask() => childTask;

        public override void SetCurrIndex(int index){} //We just want it to do nothing here

        public override int GetCurrIndex() => -1; //We don't have a valid index to return
        
        public override List<Task> GetChildren() => new List<Task>();
    }
}
