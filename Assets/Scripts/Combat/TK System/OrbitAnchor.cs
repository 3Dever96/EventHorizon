using UnityEngine;

namespace EventHorizon.Combat
{
    public class OrbitAnchor
    {
        public Transform orbitPoint;
        public bool isEmpty;

        public OrbitAnchor(Transform point)
        {
            orbitPoint = point;
            isEmpty = true;
        }
    }
}
