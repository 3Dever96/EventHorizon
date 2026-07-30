using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EventHorizon.Combat
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private float maxHp;
        private float currentHp;

        [SerializeField] private bool isInvincible;
        [SerializeField] private float hitStopTime;
        [SerializeField] private float iFrames;
        [SerializeField] private float flashTime;

        private Renderer[] avatar;

        public UnityEvent OnAlive;
        public UnityEvent OnHit;
        public UnityEvent OnDeath;

        private void Start()
        {
            currentHp = maxHp;

            avatar = GetComponentsInChildren<Renderer>();
        }

        private void OnEnable()
        {
            OnAlive?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (!isInvincible)
            {
                isInvincible = true;

                currentHp -= damage;

                StartCoroutine(HitStop());

                OnHit?.Invoke();
            }
        }

        public void Die()
        {
            OnDeath?.Invoke();
        }

        private IEnumerator HitStop()
        {
            Time.timeScale = 0f;

            float currentTime = 0f;

            Color[] colors = new Color[avatar.Length];

            for (var i = 0; i < avatar.Length; i++)
            {
                colors[i] = avatar[i].materials[0].color;
            }

            while (currentTime < hitStopTime)
            {
                currentTime += Time.unscaledDeltaTime;

                foreach (Renderer r in avatar)
                {
                    r.materials[0].color = Color.white;
                }

                yield return null;
            }

            for (var i = 0; i < avatar.Length; i++)
            {
                avatar[i].materials[0].color = colors[i];
            }

            Time.timeScale = 1f;

            if (currentHp <= 0f)
            {
                Die();
            }
            else
            {
                StartCoroutine(ITime());
            }
        }

        private IEnumerator ITime()
        {
            float currentTime = 0f;

            while (currentTime < iFrames)
            {
                currentTime += Time.deltaTime;

                foreach (Renderer r in avatar)
                {
                    Color c = r.materials[0].color;

                    c.a -= 0.5f;

                    if (c.a <= 0f)
                    {
                        c.a = 1f;
                    }

                    r.materials[0].color = c;
                }

                yield return new WaitForSeconds(flashTime);
            }

            foreach (Renderer r in avatar)
            {
                Color c = r.materials[0].color;

                c.a = 1f;

                r.materials[0].color = c;
            }

            isInvincible = false;
        }
    }
}
