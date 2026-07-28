using UnityEngine;

namespace EventHorizon.Characters
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        public CharacterController Controller { get; private set; }

        public bool IsGrounded {  get; protected set; }
        public float CurrentSpeed { get; protected set; }
        public float VerticalSpeed {  get; protected set; }
        public Vector3 Direction { get; protected set; }
        public Vector3 Velocity {  get; protected set; }

        protected virtual void Awake()
        {
            Controller = GetComponent<CharacterController>();

            Direction = transform.forward;
        }

        protected virtual void Update()
        {
            Controller.Move(Velocity * Time.deltaTime);
        }

        protected void FaceDirection(Vector3 direction, float turnSpeed = 500f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        }
    }
}
