using UnityEngine;

namespace EventHorizon.Characters
{
    public class BrawlerController : EnemyEntityController
    {
        [Header("Seek State")]
        [SerializeField] private float turnTime;
        private float currentTurnTime;

        [Header("Follow State")]
        [SerializeField] private float chaseSpeed;
        [SerializeField] private float attackDistance;

        [Header("Attack State")]
        [SerializeField] private GameObject hurtbox;
        [SerializeField] private float windUpTime;
        [SerializeField] private float attackTime;

        private float currentWindUpTime;
        private float currentAttackTime;

        private bool canAttack;

        protected override void Start()
        {
            hurtbox.SetActive(false);

            base.Start();
        }

        protected override void SeekMovement()
        {
            CurrentSpeed = 0f;

            currentTurnTime += Time.deltaTime;

            if (currentTurnTime >= turnTime)
            {
                Vector3 direction = transform.right + transform.forward;

                Direction = direction;

                currentTurnTime = 0f;
            }

            FaceDirection(Direction, 20f);

            if (target != transform)
            {
                state = EnemyStates.Follow;

                currentTurnTime = 0f;
            }
        }

        protected override void FollowMovement()
        {
            if (target != transform)
            {
                Vector3 direction = target.position - transform.position;
                direction.y = 0f;
                Direction = direction.normalized;
                moveDirection = Direction;

                Vector3 distanceCheckVec = target.position - transform.position;
                distanceCheckVec.y = 0f;

                if (distanceCheckVec.magnitude > attackDistance)
                {
                    CurrentSpeed = chaseSpeed;
                }
                else
                {
                    CurrentSpeed = 0f;
                }

                FaceDirection(Direction);

                VerticalSpeed = -5f;

                if (Vector3.Distance(target.position, transform.position) <= attackDistance)
                {
                    state = EnemyStates.Attack;
                }
            }
            else
            {
                state = EnemyStates.Seek;
            }
        }

        protected override void AttackMovement()
        {
            if (!canAttack)
            {
                currentWindUpTime += Time.deltaTime;

                if (currentWindUpTime >= windUpTime)
                {
                    canAttack = true;
                }
            }
            else
            {
                hurtbox.SetActive(true);

                currentAttackTime += Time.deltaTime;

                if (currentAttackTime >= attackTime)
                {
                    hurtbox.SetActive(false);
                    state = EnemyStates.Follow;

                    currentWindUpTime = 0f;
                    currentAttackTime = 0f;
                    canAttack = false;
                }
            }
        }
    }
}
