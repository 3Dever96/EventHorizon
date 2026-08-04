using Codice.Client.Common.GameUI;
using UnityEngine;

namespace EventHorizon.Characters
{
    public class EnemyController : MonoBehaviour
    {
        private Transform player;

        [SerializeField] private Transform lookObject;
        [SerializeField] private float scanDistance;
        [SerializeField] private float viewAngle;

        private void Start()
        {
            player = FindAnyObjectByType<PlayerController>().transform;

            if (lookObject == null)
            {
                lookObject = transform;
            }
        }

        private bool IsPlayerInRange()
        {
            if (player == null) return false;

            // 1. Calculate the flat 2D tracking vector
            Vector3 heading = player.position - transform.position;
            heading.y = 0;

            // 2. Distance check boundary
            if (heading.magnitude > scanDistance) return false;

            // 3. Normalize vectors for Dot Product comparison
            Vector3 targetDirection = heading.normalized;
            Vector3 facingDirection = lookObject.forward;

            // 4. Run the Dot Product math
            float dotResult = Vector3.Dot(facingDirection, targetDirection);

            // 5. Convert your Inspector angle degrees into a Cosine threshold
            // We divide by 2 because the angle splits left and right of the forward line
            float cosThreshold = Mathf.Cos((viewAngle / 2f) * Mathf.Deg2Rad);

            // If the result is closer to 1.0 than our threshold, they are inside the cone!
            return dotResult >= cosThreshold;
        }

        public Transform FindPlayer()
        {
            if (IsPlayerInRange())
            {
                // STEP 2: Since they are in the cone, shoot a physical ray to check for walls
                Vector3 targetDirection = ((player.position + Vector3.up) - lookObject.position).normalized;
                Ray ray = new Ray(lookObject.position, targetDirection);
                RaycastHit hit;

                // Mask includes both the "Player" and your solid "Environment/Wall" layers
                int combinedMask = LayerMask.GetMask("Player", "Default", "Solid");

                if (Physics.Raycast(ray, out hit, scanDistance, combinedMask))
                {
                    // If the ray hits the player FIRST, the path is clear!
                    if (hit.collider.CompareTag("Player"))
                    {
                        return player;
                    }
                }
            }

            return transform;
        }
    }
}
