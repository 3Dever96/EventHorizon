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
            Attack atk = other.GetComponent<Attack>();

            if (atk != null)
            {
                if (atk.Atk != 0f)
                {
                    health.TakeDamage(atk.Atk);
                }
            }
        }
    }
}
