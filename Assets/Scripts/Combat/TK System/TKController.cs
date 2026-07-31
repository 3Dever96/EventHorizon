using System.Collections.Generic;
using UnityEngine;
using EventHorizon.Managers;

namespace EventHorizon.Combat
{
    public class TKController : MonoBehaviour
    {
        [SerializeField] private GameObject tkField;
        [SerializeField] private Transform tkParent;
        private OrbitAnchor[] anchor;
        [SerializeField] private Transform throwPoint;

        public float grabRadius;

        private int maxDebris;

        private bool canPush;
        private Queue<TKObject> tkObjects = new Queue<TKObject>();

        [SerializeField] private float throwSpeed;

        private void Start()
        {
            anchor = new OrbitAnchor[tkParent.childCount];
            maxDebris = tkParent.childCount;

            for (var i = 0; i < tkParent.childCount; i++)
            {
                anchor[i] = new OrbitAnchor(tkParent.GetChild(i).transform);
            }
        }

        private void Update()
        {
            // Grab Objects
            if (InputHub.Instance.Grab)
            {
                tkField.SetActive(true);
            }
            else
            {
                tkField.SetActive(false);
            }

            // Throw Object
            if (tkObjects.Count > 0)
            {
                if (InputHub.Instance.Push && canPush)
                {
                    TKObject tk = tkObjects.Dequeue();

                    tk.anchor.isEmpty = true;
                    tk.anchor = null;

                    tk.Throw(transform.position, transform.forward, throwSpeed);

                    canPush = false;
                }
            }

            if (!InputHub.Instance.Push && !canPush)
            {
                canPush = true;
            }
        }

        private void LateUpdate()
        {
            // Orbit Objects
            tkParent.Rotate(Vector3.up, 360f * Time.deltaTime);
            tkParent.position = transform.position + Vector3.up;
        }

        public OrbitAnchor GetNextAnchor()
        {
            for (var i = 0; i < anchor.Length; i++)
            {
                if (anchor[i].isEmpty)
                {
                    anchor[i].isEmpty = false;
                    return anchor[i];
                }
            }

            return null;
        }

        public void AddObject(TKObject newObject)
        {
            if (tkObjects.Count < maxDebris)
            {
                tkObjects.Enqueue(newObject);
            }
        }
    }
}
