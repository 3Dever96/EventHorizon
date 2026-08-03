using UnityEngine;

namespace EventHorizon.Characters
{
    public class TurretController : MonoBehaviour
    {
        private EnemyStates state;
        private bool startSeek = false;
        private Transform target;

        [SerializeField] private Transform startingAimPosition;

        [Header("Charge Information")]
        [SerializeField] private float attackCharge;
        [SerializeField] private float chargeSpeed;
        public float currentCharge;

        [Header("Attack Information")]
        [SerializeField] private float attackDelay;
        public float currentDelay;

        [Header("Reset Time")]
        [SerializeField] private float resetTime;
        public float currentResetTime;

        private bool readyToReset;

        private void FixedUpdate()
        {
            if (target != null)
            {
                switch (state)
                {
                    case EnemyStates.Seek: SeekMovement(); break;
                    case EnemyStates.Follow: FollowMovement(); break;
                    case EnemyStates.Attack: AttackState(); break;
                }
            }
            else
            {
                target = transform;
            }
        }

        private void SeekMovement()
        {
            if (!startSeek)
            {
                transform.LookAt(startingAimPosition.position + Vector3.up);
                startSeek = true;
            }
            else
            {
                transform.Rotate(Vector3.up, 90f * Time.deltaTime, Space.World);
            }

            if (target != transform)
            {
                state = EnemyStates.Follow;
            }
        }

        private void FollowMovement()
        {
            if (target != transform)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((target.position + Vector3.up) - transform.position), 10f * Time.deltaTime);

                // transform.LookAt(target.position + Vector3.up);

                currentCharge += chargeSpeed * Time.deltaTime;

                if (currentCharge >= attackCharge)
                {
                    state = EnemyStates.Attack;
                }
            }
            else
            {
                state = EnemyStates.Seek;
            }
        }

        private void AttackState()
        {
            if (!readyToReset)
            {
                currentDelay += Time.deltaTime;

                if (currentDelay >= attackDelay)
                {
                    BroadcastMessage("OnFire");

                    readyToReset = true;
                }
            }
            else
            {
                currentResetTime += Time.deltaTime;

                if (currentResetTime >= resetTime)
                {
                    currentResetTime = 0f;
                    currentCharge = 0f;
                    currentDelay = 0f;

                    readyToReset = false;

                    state = EnemyStates.Follow;
                }
            }
        }

        public void OnFindPlayer(Transform player)
        {
            target = player;
        }
    }
}
