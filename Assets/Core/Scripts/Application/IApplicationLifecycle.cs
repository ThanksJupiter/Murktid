namespace Murktid {
    public interface IApplicationLifecycle {
        public void Initialize();
        public void Tick();
        public void Dispose();
    }
}
