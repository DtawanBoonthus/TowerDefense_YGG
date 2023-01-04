namespace TowerDefense.Manager.GameManager.Runtime.State
{
    public abstract class BaseGameState
    {
        public GameState State;
        protected GameManager GameManager;
        
        /// <summary>
        /// Do the enter state process.
        /// </summary>
        public abstract void EnterState();
        
        /// <summary>
        /// Do the update state process.
        /// </summary>
        public abstract void UpdateState();
        
        /// <summary>
        /// Do the exit state process.
        /// </summary>
        public abstract void ExitState();
    }
}