

namespace BehaviourTree
{
    public abstract class Task
    {

        public static bool DEBUG_BEHAVIOURS = false;

        protected string taskName = "";
        protected string failRason = "";

        protected Task(string taskName = "")
        {
            this.taskName = taskName != "" ? taskName : this.GetType().Name;
        }

        /// <summary>
        /// Used to check any special conditions before Start() is called.
        /// </summary>
        /// <returns>True if the task is ready to be started, false otherwise</returns>
        public virtual bool Check()
        {
            return true;
        }

        /// <summary>
        /// Used to initialize anything like variables for use in the Update() method
        /// </summary>
        public virtual void Start(BlackBoard blackboard)
        {
            
        }

        /// <summary>
        /// The update method of the Task. Called to execute the behaviour.
        /// </summary>
        /// <param name="blackboard">The BlackBoard to use for data.</param>
        /// <returns>The status of the Task. Success, Running, or Failure.</returns>
        public virtual BehaviourTreeStatus Update(BlackBoard blackboard, float delta)
        {
            return BehaviourTreeStatus.Success;
        }

        /// <summary>
        /// Called when the Task completes (either Success or Failure)
        /// </summary>
        public virtual void End(BlackBoard blackboard)
        {
        }

        /// <summary>
        /// Called when the task is reset for reuse.
        /// </summary>
        public virtual void Reset(BlackBoard blackboard)
        {
        }

        public override string ToString()
        {
            return this.taskName;

        }
    }
}
