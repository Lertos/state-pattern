namespace state_pattern
{
    //A demonstration of the State pattern in C#
    internal class Program
    {
        static void Main(string[] args)
        {
            Computer computer = new Computer();

            computer.PressPowerButton();
            computer.PressPowerButton();
            computer.PressPowerButton();

            /* OUTPUT:
             
                Turning power ON
                Turning power OFF
                Turning power ON
             
             */
        }
    }

    public partial class Computer
    {
        private State state = new Off();

        private void SetState(State state)
        {
            this.state = state;
        }

        //Since we hold state internally, all the client knows is they are pressing a button - the rest happens under the hood
        public void PressPowerButton()
        {
            state.PressPowerButton(this);
        }
    }

    public partial class Computer
    {
        private interface State
        {
            void PressPowerButton(Computer computer);
        }

        //All states implement the same PressPowerButton method so external doesn't care which one it is
        private class Off : State
        {
            public void PressPowerButton(Computer computer)
            {
                Console.WriteLine("Turning power ON");
                computer.SetState(new On());
            }
        }

        private class On : State
        {
            private bool charging;

            public void PressPowerButton(Computer computer)
            {
                if (charging)
                {
                    Console.WriteLine("Turning power to STANDBY");
                    computer.SetState(new Standby());
                }
                else
                {
                    Console.WriteLine("Turning power OFF");
                    computer.SetState(new Off()); 
                }
            }
        }

        private class Standby: State
        {
            public void PressPowerButton(Computer computer)
            {
                Console.WriteLine("Turning power OFF");
                computer.SetState(new Off());
            }
        }
    }


}
