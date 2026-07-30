using System.Collections.Generic;
using UnityEngine;
using EventHorizon.Managers;

namespace EventHorizon.Combat
{
    public class TKController : MonoBehaviour
    {
        [SerializeField] private GameObject tkField;
        [SerializeField] private Transform tkParent;
        [SerializeField] private Transform[] anchor;
        [SerializeField] private bool[] emptyAnchor;

        [SerializeField] private float grabRadius;

        [SerializeField] private int maxDebris;

        private bool canPush;

        private void Start()
        {
            anchor = new Transform[tkParent.childCount];
            emptyAnchor = new bool[tkParent.childCount];

            for (var i = 0; i < tkParent.childCount; i++)
            {
                anchor[i] = tkParent.GetChild(i);
                emptyAnchor[i] = true;
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

            if (!InputHub.Instance.Push && !canPush)
            {
                canPush = true;
            }
        }

        private void LateUpdate()
        {
            // Orbit Objects
            tkParent.Rotate(Vector3.up, 180f * Time.deltaTime);
            tkParent.position = transform.position + Vector3.up;
        }

        public Transform GetNextAnchor()
        {
            for (var i = 0; i < emptyAnchor.Length; i++)
            {
                if (emptyAnchor[i])
                {
                    return anchor[i];
                }
            }

            return null;
        }
    }
}
