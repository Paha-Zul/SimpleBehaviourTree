using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree.Tests
{
    class TestSuccessLeaf : LeafTask
    {
        public TestSuccessLeaf(string taskName = "") : base(taskName)
        {
        }

        public override void Start(BlackBoard blackboard)
        {
            base.Start(blackboard);
            Console.WriteLine("Started");
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            Console.WriteLine($"I am {taskName}");
            return BehaviourTreeStatus.Success;
        }

        public override void End(BlackBoard blackboard)
        {
            base.End(blackboard);
            Console.WriteLine("Ended");
        }
    }
}
