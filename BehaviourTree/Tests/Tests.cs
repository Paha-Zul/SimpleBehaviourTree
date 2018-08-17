using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BehaviourTree.Tests
{
    class Tests
    {
        private static System.Timers.Timer aTimer;

        static void Main(string[] args)
        {
            Console.WriteLine("Hey");

            TestSelectorOfSequences();

            Console.Read();
        }

        private static void TestSimpleSequence()
        {
            var bb = new BlackBoard();

            var sequence = new SequenceTask("Main Sequence");
            sequence.AddChildTask(new TestSuccessLeaf("1"));
            sequence.AddChildTask(new TestSuccessLeaf("2"));
            sequence.AddChildTask(new TestSuccessLeaf("3"));
            sequence.AddChildTask(new TestFailureLeaf("4"));
            sequence.AddChildTask(new TestSuccessLeaf("5"));

            SetTimer(sequence, bb);
        }

        private static void TestSequenceInSequence()
        {
            var bb = new BlackBoard();

            var main = new SequenceTask("Main Sequence");
            var s2 = new SequenceTask("Child Sequence");

            main.AddChildTask(new TestSuccessLeaf("1"));
            main.AddChildTask(new TestSuccessLeaf("2"));
            main.AddChildTask(new TestSuccessLeaf("3"));

            s2.AddChildTask(new TestSuccessLeaf("c 1"));
            s2.AddChildTask(new TestSuccessLeaf("c 2"));
            main.AddChildTask(s2);

            main.AddChildTask(new TestSuccessLeaf("4"));

            SetTimer(main, bb);
        }

        private static void TestSimpleSelector()
        {
            var bb = new BlackBoard();

            var selector = new SelectorTask("Main Sequence");
            selector.AddChildTask(new TestFailureLeaf("Fail 1"));
            selector.AddChildTask(new TestFailureLeaf("Fail 2"));
            selector.AddChildTask(new TestSuccessLeaf("Success 3"));
            selector.AddChildTask(new TestFailureLeaf("Fail 4"));
            selector.AddChildTask(new TestFailureLeaf("Fail 5"));

            SetTimer(selector, bb);
        }

        private static void TestSelectorOfSequences()
        {
            var bb = new BlackBoard();

            var main = new SelectorTask("Main Selector");

            var s1 = new SequenceTask("Sequence 1");
            var s2 = new SequenceTask("Sequence 2");
            var s3 = new SequenceTask("Sequence 3");

            s1.AddChildTask(new TestSuccessLeaf("1"));
            s1.AddChildTask(new TestSuccessLeaf("2"));
            s1.AddChildTask(new AlwaysTrueTask(new TestFailureLeaf("3")));

            s2.AddChildTask(new TestSuccessLeaf("1"));
            s2.AddChildTask(new TestFailureLeaf("2"));
            s2.AddChildTask(new TestSuccessLeaf("3"));

            s3.AddChildTask(new TestSuccessLeaf("1"));
            s3.AddChildTask(new TestSuccessLeaf("2"));
            s3.AddChildTask(new TestSuccessLeaf("3"));

            main.AddChildTask(s1);
            main.AddChildTask(s2);
            main.AddChildTask(s3);

            SetTimer(main, bb);
        }

        private static void SetTimer(Task mainTask, BlackBoard bb)
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(16); //16ms for 60 fps

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (source, e) => 
            {
                Console.WriteLine(mainTask.ToString());
                var status = mainTask.Update(bb);

                if (status != BehaviourTreeStatus.Running)
                {
                    Console.WriteLine("Test is done");
                    aTimer.Stop();
                }
            };

            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

    }
}
