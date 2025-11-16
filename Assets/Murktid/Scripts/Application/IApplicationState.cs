namespace AutoZombie {
    public enum ApplicationState {
        Invalid,
        Splash,
        Loading,
        MainMenu,
        GameMode
    }

    public interface IApplicationState {
        public void EnterApplicationState();
        public ApplicationState Tick();
        public void LateTick();
        public void ExitApplicationState();
        void Dispose();
        bool IsApplicationStateInitialized { get; }
    }

}
