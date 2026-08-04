using UnityEngine;

namespace EventHorizon.Characters
{
    public class ScoutController : EnemyEntityController
    {
        [SerializeField] private float altSpeed;
        [SerializeField] private float altAccel;
        private Vector3 originPoint;

        [SerializeField] private GameObject hurtbox;

        [Header("Seek State")]
        [SerializeField] private float roamSpeed;

        [Header("Follow State")]
        [SerializeField] private float chaseSpeed;
        [SerializeField] private float attackCharge;
        [SerializeField] private float chargeSpeed;
        private float currentCharge;

        [Header("Attack State")]
        [SerializeField] private float ramSpeed;
        [SerializeField] private float delayTime;
        [SerializeField] private float ramTime;

        private bool lockRotation;
        private bool canRam;

        private float currentDelay;
        private float currentRam;

        protected override void Start()
        {
            originPoint = transform.position + Vector3.up;

            target = transform;

            hurtbox.SetActive(false);

            base.Start();
        }

        protected override void SeekMovement()
        {
            if (Vector2.Distance(new Vector2(originPoint.x, originPoint.z), new Vector2(transform.position.x, transform.position.z)) > 0.5f)
            {
                CurrentSpeed = roamSpeed;
                Vector3 direction = originPoint - transform.position;
                direction.y = 0f;
                Direction = direction.normalized;

                FaceDirection(Direction);
            }
            else
            {
                CurrentSpeed = 0f;

                Vector3 lookDirection = transform.forward + transform.right;

                Direction = lookDirection;

                FaceDirection(Direction, 25f);
            }

            moveDirection = Direction;

            NormalVerticalMovement(originPoint.y);

            if (target != transform)
            {
                state = EnemyStates.Follow;
            }
        }

        protected override void FollowMovement()
        {
            if (target != transform)
            {
                if (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(transform.position.x, transform.position.z)) > 5f)
                {
                    CurrentSpeed = chaseSpeed;
                    Vector3 direction = (target.position + Vector3.up) - transform.position;
                    direction.y = 0f;
                    Direction = direction.normalized;

                    moveDirection = Direction;
                }
                else if (Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(transform.position.x, transform.position.z)) < 4.75f)
                {
                    CurrentSpeed = -chaseSpeed;
                    Vector3 direction = (target.position + Vector3.up) - transform.position;
                    direction.y = 0f;
                    Direction = direction.normalized;

                    moveDirection = Direction;
                }
                else 
                {
                    CurrentSpeed = roamSpeed;

                    Vector3 lookDirection = (target.position + Vector3.up) - transform.position;

                    Direction = lookDirection;

                    moveDirection = transform.right;
                }

                FaceDirection(Direction, 1000f);

                currentCharge += Time.deltaTime * chargeSpeed;

                if (currentCharge >= attackCharge)
                {
                    state = EnemyStates.Attack;
                }
            }
            else
            {
                state = EnemyStates.Seek;
            }

            NormalVerticalMovement(target.position.y + 1f);
        }

        protected override void AttackMovement()
        {
            if (canRam == false)
            {
                moveDirection = transform.forward;

                CurrentSpeed = -roamSpeed;

                currentDelay += Time.deltaTime;

                if (currentDelay >= delayTime)
                {
                    canRam = true;
                }
            }
            else
            {
                hurtbox.SetActive(true);
                CurrentSpeed = ramSpeed;

                currentRam += Time.deltaTime;

                if (currentRam >= ramTime)
                {
                    currentDelay = 0f;
                    currentRam = 0f;
                    currentCharge = 0f;

                    canRam = false;

                    hurtbox.SetActive(false);

                    state = EnemyStates.Follow;
                }
            }
        }

        private void NormalVerticalMovement(float targetHeight)
        {
            if (transform.position.y < targetHeight)
            {
                if (VerticalSpeed < altSpeed)
                {
                    VerticalSpeed += altAccel * 2f * Time.deltaTime;
                }
                else
                {
                    VerticalSpeed = altSpeed;
                }
            }
            else
            {
                if (VerticalSpeed > -altSpeed)
                {
                    VerticalSpeed -= altAccel * Time.deltaTime;
                }
                else
                {
                    VerticalSpeed = -altSpeed;
                }
            }
        }
    }
}
