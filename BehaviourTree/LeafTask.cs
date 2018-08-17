using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourTree
{
    /// <summary>
    /// Used as a non-abstract parent for leaf nodes.
    /// </summary>
    class LeafTask : Task
    {
        public LeafTask(string taskName = "") : base(taskName)
        {
        }

        public override string ToString()
        {
            return $"{taskName}";
        }
    }
}
