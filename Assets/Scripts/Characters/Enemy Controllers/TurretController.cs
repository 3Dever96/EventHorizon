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
        private float currentCharge;

        [Header("Attack Information")]
        [SerializeField] private float attackDelay;
        private float currentDelay;

        [Header("Reset Time")]
        [SerializeField] private float resetTime;
        private float currentResetTime;

        private bool readyToReset;

        private bool canFindPlayer;

        private void FixedUpdate()
        {
            switch (state)
            {
                case EnemyStates.Seek: SeekMovement(); break;
                case EnemyStates.Follow: FollowMovement(); break;
                case EnemyStates.Attack: AttackState();  break;
            }
        }

        private void SeekMovement()
        {
            canFindPlayer = true;
            readyToReset = false;

            if (!startSeek)
            {
                transform.LookAt(startingAimPosition.position + Vector3.up);
                startSeek = true;
            }
            else
            {
                transform.Rotate(Vector3.up, 90f * Time.deltaTime, Space.World);
            }

            currentCharge = 0f;
            currentDelay = 0f;
        }

        private void FollowMovement()
        {
            canFindPlayer = false;

            if (target != null)
            {
                transform.LookAt(target.position + Vector3.up);

                currentCharge += chargeSpeed * Time.deltaTime;

                if (currentCharge >= attackCharge)
                {
                    state = EnemyStates.Attack;
                }
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
                    state = EnemyStates.Seek;
                }
            }
        }

        public void OnFindPlayer(Transform player)
        {
            if (!canFindPlayer) return;

            target = player;

            if (target != transform)
            {
                state = EnemyStates.Follow;
                startSeek = false;
            }
            else
            {
                state = EnemyStates.Seek;
            }
        }
    }
}
