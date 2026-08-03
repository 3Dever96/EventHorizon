using UnityEngine;
using EventHorizon.Managers;

namespace EventHorizon.Characters
{
    public class PlayerController : MovementController
    {
        [Header("Horizontal Movement")]
        [SerializeField] private float moveSpeed;

        [Header("Vertical Movement")]
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float gravity;
        [SerializeField] private float fallSpeed;

        protected override void Update()
        {
            // Set IsGrounded
            IsGrounded = VerticalSpeed < 0f && Physics.CheckSphere(transform.position + Vector3.up * 0.9f, 1.1f, LayerMask.GetMask("Solid"));

            Animator.SetBool("IsGrounded", IsGrounded);

            // Get Input Direction
            Vector3 direction = Camera.main.transform.right * InputHub.Instance.Move.x + Camera.main.transform.forward * InputHub.Instance.Move.y;
            direction.y = 0f;
            direction = direction.normalized;

            // Set Horizontal Speed
            if (InputHub.Instance.Move != Vector2.zero)
            {
                CurrentSpeed = moveSpeed * InputHub.Instance.Move.magnitude;
                Direction = direction;
            }
            else
            {
                CurrentSpeed = 0f;
            }

            // Face Direction
            FaceDirection(Direction);

            // Vertical Movement
            if (InputHub.Instance.Jump)
            {
                VerticalSpeed = jumpSpeed;
            }
            else
            {
                if (IsGrounded)
                {
                    VerticalSpeed = -5f;
                }
                else
                {
                    if (VerticalSpeed > fallSpeed)
                    {
                        VerticalSpeed += gravity * Time.deltaTime;
                    }
                }
            }

            Animator.SetFloat("CurrentSpeed", CurrentSpeed);
            Animator.SetFloat("VerticalSpeed", VerticalSpeed);

            // Apply Movement
            Vector3 velocity = CurrentSpeed * Direction;
            velocity.y = VerticalSpeed;

            Velocity = velocity;

            base.Update();
        }
    }
}
