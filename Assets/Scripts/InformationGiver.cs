using UnityEngine;

namespace EventHorizon
{
    public class InformationGiver : MonoBehaviour
    {
        [SerializeField, TextArea] private string explanation;
        [SerializeField] private Information information;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                information.DisplayText(explanation);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                information.HideText();
            }
        }
    }
}
