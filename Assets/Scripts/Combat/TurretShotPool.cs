using System.Collections.Generic;
using UnityEngine;

namespace EventHorizon.Combat
{
    public class TurretShotPool : MonoBehaviour
    {
        public static TurretShotPool Instance;

        private Queue<Bullet> bullets = new Queue<Bullet>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }

            Bullet[] childBullets = GetComponentsInChildren<Bullet>();

            for (var i = 0; i < childBullets.Length; i++)
            {
                AddBullet(childBullets[i]);
                childBullets[i].gameObject.SetActive(false);
            }
        }

        public void AddBullet(Bullet bullet)
        {
            if (!bullets.Contains(bullet))
            {
                bullets.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            return bullets.Dequeue();
        }
    }
}
