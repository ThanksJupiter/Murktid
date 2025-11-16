using System.Collections;

namespace AutoZombie {

    public enum DevicePlatform {
        Desktop,
        Android
    }

    public interface IPlatform {
        IEnumerator Initialize(object applicationData);

    }
}
