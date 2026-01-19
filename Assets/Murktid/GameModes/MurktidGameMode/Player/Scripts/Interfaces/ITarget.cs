using UnityEngine;

namespace Murktid {

    public interface ITarget {
        void Hit(float damage);
        void Stagger();
    }
}
