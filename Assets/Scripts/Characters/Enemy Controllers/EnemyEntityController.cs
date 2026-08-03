using UnityEngine;

namespace EventHorizon.Characters
{
    public class EnemyEntityController : MovementController
    {
        [SerializeField] protected EnemyStates state;

        protected Transform target;

        protected Vector3 moveDirection;

        public virtual void OnFindPlayer(Transform newTarget)
        {
            target = newTarget;
        }

        protected override void Update()
        {
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
