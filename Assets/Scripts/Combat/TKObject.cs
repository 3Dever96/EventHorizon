using UnityEngine;

namespace EventHorizon.Combat
{
    public class TKObject : MonoBehaviour
    {
        private Rigidbody body;

        public Transform anchorPoint;

        [SerializeField]
        private float anchorSpeed;

        private void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (anchorPoint != null)
            {
                body.MovePosition(body.position + ((anchorPoint.position - body.position) * anchorSpeed) * Time.deltaTime);
                transform.Rotate(new Vector3(45f, 45f, 45f) * Time.deltaTime);
            }
        }

        public void SetAnchor(Transform newAnchor)
        {
            anchorPoint = newAnchor;
            body.useGravity = false;

            gameObject.layer = 7;
        }
    }
}
