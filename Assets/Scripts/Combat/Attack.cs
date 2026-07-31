using UnityEngine;

namespace EventHorizon.Combat
{
    public class Attack : MonoBehaviour
    {
        public float Atk { get { return atk; } }
        [SerializeField] protected float atk;
    }
}
