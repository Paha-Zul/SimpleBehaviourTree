using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree.Tests
{
    class TestRunningLeaf : LeafTask
    {
        public TestRunningLeaf(string taskName = "") : base(taskName)
        {
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard, float deltaTime)
        {
            Console.WriteLine($"I am {taskName}");
            return BehaviourTreeStatus.Running;
        }
    }
}
