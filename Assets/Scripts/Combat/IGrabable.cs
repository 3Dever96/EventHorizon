using UnityEngine;

namespace EventHorizon.Combat
{
    public interface IGrabable
    {
        public abstract void Grab(Transform newParent);

        public abstract void Throw(Vector3 velocity);
    }
}
