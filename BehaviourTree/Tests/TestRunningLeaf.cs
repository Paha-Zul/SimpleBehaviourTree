using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree.Tests
{
    class TestRunningLeaf : LeafTask
    {
        public override BehaviourTreeStatus Update(BlackBoard blackboard)
        {
            return BehaviourTreeStatus.Running;
        }
    }
}
