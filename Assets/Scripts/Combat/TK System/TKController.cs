using System.Collections.Generic;
using UnityEngine;
using EventHorizon.Managers;

namespace EventHorizon.Combat
{
    public class TKController : MonoBehaviour
    {
        [SerializeField] private GameObject tkField;
        [SerializeField] private Transform tkParent;
        [SerializeField] private Transform throwPoint;

        private Animator animator;

        public float grabRadius;

        private int maxDebris;

        private bool canPush;
        private Queue<TKObject> tkObjects = new Queue<TKObject>();
        private Queue<OrbitAnchor> anchors = new Queue<OrbitAnchor>();

        [SerializeField] private float throwSpeed;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();


            maxDebris = tkParent.childCount;

            for (var i = 0; i < maxDebris; i++)
            {
                anchors.Enqueue(new OrbitAnchor(tkParent.GetChild(i)));
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

            animator.SetBool("IsGathering", InputHub.Instance.Grab);

            // Throw Object
            if (tkObjects.Count > 0)
            {
                if (InputHub.Instance.Push && canPush)
                {
                    animator.Play("Throw");
                    animator.SetBool("MirrorThrow", Random.value > 0.5f);

                    TKObject tk = tkObjects.Dequeue();

                    anchors.Enqueue(tk.Throw(transform.position, transform.forward, throwSpeed));

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
            if (anchors.Count > 0)
            {
                return anchors.Dequeue();
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
