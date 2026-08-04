using UnityEngine;

namespace EventHorizon.Characters
{
    public class EnemyEntityController : MovementController
    {
        protected EnemyController enemy;
        [SerializeField] protected EnemyStates state;

        protected Transform target;

        protected Vector3 moveDirection;

        protected virtual void Start()
        {
            enemy = GetComponent<EnemyController>();
        }

        protected override void Update()
        {
            target = enemy.FindPlayer();

            print(target);

            switch (state)
            {
                case EnemyStates.Seek: SeekMovement(); break;
                case EnemyStates.Follow: FollowMovement(); break;
                case EnemyStates.Attack: AttackMovement(); break;
            }

            Vector3 velocity = CurrentSpeed * moveDirection;
            velocity.y = VerticalSpeed;

            Velocity = velocity;

            base.Update();
        }

        protected virtual void SeekMovement()
        {

        }

        protected virtual void FollowMovement()
        {

        }

        protected virtual void AttackMovement()
        {

        }
    }
}
