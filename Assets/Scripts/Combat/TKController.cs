using System.Collections.Generic;
using UnityEngine;
using EventHorizon.Managers;

namespace EventHorizon.Combat
{
    public class TKController : MonoBehaviour
    {
        [SerializeField] private SphereCollider tkField;
        [SerializeField] private Transform tkParent;

        [SerializeField] private float grabRadius;

        [SerializeField] private int maxDebris;
        private Queue<IGrabable> debris = new Queue<IGrabable>();

        private bool canPush;

        private void Update()
        {
            // Grab Objects
            if (InputHub.Instance.Grab)
            {
                if (tkField.radius < grabRadius)
                {
                    tkField.radius += 2f * Time.deltaTime;
                }
                else
                {
                    tkField.radius = grabRadius;
                }
            }
            else
            {
                if (tkField.radius > 0.01f)
                {
                    tkField.radius -= 50f * Time.deltaTime;
                }
                else
                {
                    tkField.radius = 0f;
                }
            }

            // Orbit Objects
            tkParent.Rotate(new Vector3(0f, 180f, 0f) * Time.deltaTime);

            // Throw Objects
            if (debris.Count > 0f)
            {
                if (InputHub.Instance.Push && canPush)
                {
                    IGrabable newObject = debris.Dequeue();

                    newObject.Throw(Camera.main.transform.forward * 20f + Camera.main.transform.up * 5f);

                    canPush = false;
                }
            }

            if (!InputHub.Instance.Push && !canPush)
            {
                canPush = true;
            }
        }

        public Transform GetParent()
        {
            return tkParent;
        }

        public bool AddToQueue(IGrabable grabable)
        {
            if (debris.Count < maxDebris && !debris.Contains(grabable))
            {
                debris.Enqueue(grabable);
                return true;
            }

            return false;
        }
    }
}
