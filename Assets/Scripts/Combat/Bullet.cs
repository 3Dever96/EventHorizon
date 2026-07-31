using UnityEngine;

namespace EventHorizon.Combat
{
    public class Bullet : Attack
    {
        private Rigidbody body;
        [SerializeField] private float speed;

        private bool fired;

        private void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (fired)
            {
                body.MovePosition(body.position + transform.forward * speed * Time.deltaTime);
            }
        }

        private void OnDisable()
        {
            TurretShotPool.Instance.AddBullet(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (fired)
            {
                fired = false;
                gameObject.SetActive(false);
            }
        }

        public void Fire(Vector3 origin, Vector3 direction)
        {
            transform.position = origin;
            transform.forward = direction;
            fired = true;
        }
    }
}
