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

        public override BehaviourTreeStatus Update(BlackBoard blackboard)
        {
            Console.WriteLine($"I am {taskName}");
            return BehaviourTreeStatus.Success;
        }
    }
}
