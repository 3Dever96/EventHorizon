using UnityEngine;

namespace EventHorizon.Characters
{
    public class EnemyController : MonoBehaviour
    {
        private Transform player;

        [SerializeField] private float scanDistance;

        private void Start()
        {
            player = FindAnyObjectByType<PlayerController>().transform;
        }

        public void FindPlayer()
        {
            Ray ray = new Ray(transform.position, ((player.position + Vector3.up) - transform.position).normalized);

            if (Physics.Raycast(ray, scanDistance, LayerMask.GetMask("Player")))
            {
                BroadcastMessage("OnFindPlayer", player);
            }
            else
            {
                BroadcastMessage("OnFindPlayer", transform);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            FindPlayer();
        }

        private void OnTriggerExit(Collider other)
        {
            FindPlayer();
        }
    }
}
