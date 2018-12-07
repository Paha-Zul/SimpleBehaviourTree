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

        public override Task GetCurrentChildTask() => null;

        public override void SetCurrIndex(int index){} //Do nothing here

        public override int GetCurrIndex() => -1; //No valid index to return
    }
}
