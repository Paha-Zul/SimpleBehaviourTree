namespace BehaviourTree
{
    public class SelectorTask : CompositeTask
    {
        public SelectorTask(string taskName = "") : base(taskName)
        {
        }

        protected override BehaviourTreeStatus ChildSucceeded()
        {
            return BehaviourTreeStatus.Success; //Simply return success and be done for SelectorTask
        }

        protected override BehaviourTreeStatus ChildFailed()
        {
            children[currIndex].End(); //End the current child

            //Keep looping until either we pass the Check() of the new child or we exceed the children count
            do
            {
                currIndex++; //Increment the index

                if (currIndex >= children.Count) //If the index is out of the bounds of our children, return success because we're done with the selctor
                    return BehaviourTreeStatus.Failure; //Return success
            } while (!children[currIndex].Check());

            children[currIndex].Start(); //Start the child

            return BehaviourTreeStatus.Running; //Return that we are still running
        }
    }
}
