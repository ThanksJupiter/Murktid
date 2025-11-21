using System.Collections;

namespace Murktid {

    public enum DevicePlatform {
        Desktop,
        Android
    }

    public interface IPlatform {
        IEnumerator Initialize(object applicationData);

    }
}
