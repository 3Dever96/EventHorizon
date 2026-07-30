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

        private void Update()
        {
            if (transform.localScale.x < controller.grabRadius)
            {
                Vector3 addScale = Vector3.one * 2f;

                transform.localScale += addScale * Time.deltaTime;
            }
            else
            {
                transform.localScale = Vector3.one * controller.grabRadius;
            }
        }

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
        }

        private void OnTriggerEnter(Collider other)
        {
            TKObject newObject = other.GetComponent<TKObject>();

            if (newObject != null)
            {
                newObject.SetAnchor(controller.GetNextAnchor());
                controller.AddObject(newObject);
            }
        }
    }
}
