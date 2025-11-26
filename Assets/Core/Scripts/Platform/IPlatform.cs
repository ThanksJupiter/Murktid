using System.Collections;

namespace Murktid {

    public enum DevicePlatform {
        Desktop,
        Android
    }

    public interface IPlatform {
        IEnumerator Initialize(ApplicationData applicationData);
        IApplicationLifecycle InputHandler();
        void Tick();
        void LateTick();
        void Dispose();
        void OnApplicationQuit();
    }
}
