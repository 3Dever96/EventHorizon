using UnityEngine;

namespace EventHorizon.Characters
{
    public class EnemyController : MonoBehaviour
    {
        private Transform player;

        private void Start()
        {
            player = FindAnyObjectByType<PlayerController>().transform;
        }

        public void FindPlayer()
        {
            Ray ray = new Ray(transform.position, (player.position + Vector3.up) - transform.position);

            if (Physics.Raycast(ray, 15f, LayerMask.GetMask("Player")))
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
            if (other.gameObject.tag == "Player")
            {
                FindPlayer();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                FindPlayer();
            }
        }
    }
}
