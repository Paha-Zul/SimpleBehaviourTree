using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree.Tests
{
    class TestFailureLeaf : LeafTask
    {
        public TestFailureLeaf(string taskName = "") : base(taskName)
        {
        }

        public override BehaviourTreeStatus Update(BlackBoard blackboard)
        {
            Console.WriteLine($"I am {taskName}");
            return BehaviourTreeStatus.Failure;
        }
    }
}
