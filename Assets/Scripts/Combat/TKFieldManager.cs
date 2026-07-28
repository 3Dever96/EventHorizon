using UnityEngine;

namespace EventHorizon.Combat
{
    public class TKFieldManager : MonoBehaviour
    {
        private TKController controller;

        private void Start()
        {
            controller = GetComponentInParent<TKController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            IGrabable debris = other.GetComponent<IGrabable>();

            if (debris != null)
            {
                if (controller.AddToQueue(debris))
                {
                    debris.Grab(controller.GetParent());
                }
            }
        }
    }
}
