using RpgCore.Interface;

namespace RpgCore.StateMachine
{
    public class StateMachineSystem
    {
        public IState CurrentState => currentlyRunningState;

        private IState currentlyRunningState;
        private IState previousState;

        public void ChangeState(IState newState)
        {
            if (this.currentlyRunningState != null)
            {
                this.currentlyRunningState.Exit();
            }
            this.previousState = this.currentlyRunningState;

            this.currentlyRunningState = newState;
            this.currentlyRunningState.Enter();
        }

        public void ExecuteStateUpdate()
        {
            var runningState = this.currentlyRunningState;
            if (runningState != null)
            {
                this.currentlyRunningState.Execute();
            }
        }

        public void Switch2PreviousState()
        {
            this.currentlyRunningState.Exit();
            if (this.previousState != null)
            {
                this.currentlyRunningState = this.previousState;
            }
            this.currentlyRunningState.Enter();
        }
    }
}
