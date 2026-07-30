using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventHorizon
{
    public class Gate : MonoBehaviour
    {
        public int locks;

        public void AddLock()
        {
            locks++;

            transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        }

        public void RemoveLock()
        {
            locks--;

            if (locks <= 0f)
            {
                transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && locks <= 0f)
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }
}
