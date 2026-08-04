using UnityEngine;

namespace EventHorizon.Combat
{
    public class TKObject : Attack
    {
        [SerializeField]
        private float orbitAtk;
        [SerializeField]
        private float thrownAtk;

        [Header("TK Information")]
        private Rigidbody body;

        public OrbitAnchor anchor;

        [SerializeField]
        private float anchorSpeed;

        private Vector3 direction;
        private float speed;

        private void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // 1. Cache the reference locally on frame zero to guarantee thread safety
            OrbitAnchor currentAnchor = anchor;

            // 2. Add an explicit check to verify that neither the anchor nor its target point is null!
            if (currentAnchor != null && currentAnchor.orbitPoint != null)
            {
                // Smoothly glide toward your tracking slot safely
                Vector3 pullStep = (currentAnchor.orbitPoint.position - body.position) * anchorSpeed * Time.fixedDeltaTime;
                body.MovePosition(body.position + pullStep);

                transform.Rotate(new Vector3(45f, 45f, 45f) * Time.fixedDeltaTime);
                atk = orbitAtk;
            }
            // 3. If it was thrown on the same frame, it jumps straight to your clean projectile trajectory
            else if (gameObject.layer == 8)
            {
                body.linearVelocity = direction * speed;
                atk = thrownAtk;
            }
            else
            {
                atk = 0f;
            }
        }

        public void SetAnchor(OrbitAnchor newAnchor)
        {
            if (newAnchor != null)
            {
                gameObject.layer = 7;

                anchor = newAnchor;
                anchor.isEmpty = false;

                body.useGravity = false;
            }
        }

        public OrbitAnchor Throw(Vector3 origin, Vector3 newDirection, float newSpeed)
        {
            OrbitAnchor lastAnchor = anchor;
            lastAnchor.isEmpty = true;
            anchor = null;

            gameObject.layer = 8;
            body.useGravity = true; // Keep true if you want it to drop over distance, or false for a straight line laser shot

            // Move to origin position instantly
            body.position = origin + Vector3.up * 1.5f;

            direction = (newDirection + Vector3.up * 0.1f).normalized;
            speed = newSpeed;

            // Apply the instantaneous physical kick velocity
            body.linearVelocity = direction * speed;

            return lastAnchor;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 3)
            {
                gameObject.layer = 6;
            }
        }
    }
}
