namespace BehaviourTree
{
    /// <summary>
    /// Used as a non-abstract parent for leaf nodes.
    /// </summary>
    public class LeafTask : Task
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
