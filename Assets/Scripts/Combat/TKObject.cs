using UnityEngine;

namespace EventHorizon.Combat
{
    public class TKObject : MonoBehaviour
    {
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
            if (anchor != null)
            {
                body.MovePosition(body.position + ((anchor.orbitPoint.position - body.position) * anchorSpeed) * Time.deltaTime);
                transform.Rotate(new Vector3(45f, 45f, 45f) * Time.deltaTime);
            }
            else if (gameObject.layer == 8)
            {
                body.MovePosition(body.position + direction * speed * Time.deltaTime);
            }
        }

        public void SetAnchor(OrbitAnchor newAnchor)
        {
            if (newAnchor != null)
            {
                gameObject.layer = 7;

                anchor = newAnchor;

                body.useGravity = false;
            }
        }

        public void Throw(Vector3 newDirection, float newSpeed)
        {
            anchor = null;
            gameObject.layer = 8;

            direction = newDirection;
            direction.y = 1f;
            direction = direction.normalized;

            speed = newSpeed;

            body.useGravity = true;
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
