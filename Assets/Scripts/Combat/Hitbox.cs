using UnityEngine;

namespace EventHorizon.Combat
{
    public class Hitbox : MonoBehaviour
    {
        private HealthSystem health;

        private void Start()
        {
            health = GetComponentInParent<HealthSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            health.TakeDamage(5f);
        }
    }
}
