using UnityEngine;

namespace EventHorizon
{
    public class LockCounter : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text counter;

        private int remainingLocks;
        private int maxLocks;

        public void IncreaseLocks()
        {
            remainingLocks++;
            maxLocks++;

            counter.text = remainingLocks.ToString() + "/" + maxLocks.ToString();
        }

        public void DecreaseLocks()
        {
            remainingLocks--;

            counter.text = remainingLocks.ToString() + "/" + maxLocks.ToString();
        }
    }
}
