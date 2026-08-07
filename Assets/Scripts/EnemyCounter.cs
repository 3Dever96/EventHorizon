using UnityEngine;
using UnityEngine.Events;

namespace EventHorizon
{
    public class EnemyCounter : MonoBehaviour
    {
        private int currentEnemy;

        public UnityEvent OnEnemyClear;

        public void IncreaseEnemies()
        {
            currentEnemy++;
        }

        public void DecreaseEnemeis()
        {
            currentEnemy--;

            if (currentEnemy <= 0f)
            {
                OnEnemyClear?.Invoke();
            }
        }
    }
}
