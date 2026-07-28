using UnityEngine;
using EventHorizon.Combat;

namespace EventHorizon
{
    public class Crate : MonoBehaviour, IGrabable
    {
        private bool inOrbit;
        private bool thrown;

        private Rigidbody body;

        private void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (inOrbit)
            {
                if (Vector3.Distance(transform.position, transform.parent.position) > 2.5f)
                {
                    Vector3 direction = transform.parent.position - transform.position;
                    direction = direction.normalized;


                    transform.Translate(direction * 5f * Time.deltaTime);
                }
            }
        }

        public void Grab(Transform newParent)
        {
            transform.parent = newParent;

            inOrbit = true;

            Collider myCollider = GetComponent<Collider>();

            myCollider.enabled = false;

            body.useGravity = false;
        }

        public void Throw(Vector3 velocity)
        {
            inOrbit = false;
            transform.parent = null;

            thrown = true;

            body = GetComponent<Rigidbody>();

            body.useGravity = true;

            body.linearVelocity = velocity;

            Collider myCollider = GetComponent<Collider>();

            myCollider.enabled = true;

            myCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (thrown)
            {
                Destroy(gameObject);
            }
        }
    }
}
