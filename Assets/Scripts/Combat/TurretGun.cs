using UnityEngine;

namespace EventHorizon.Combat
{
    public class TurretGun : MonoBehaviour
    {
        public void OnFire()
        {
            Bullet newBullet = TurretShotPool.Instance.GetBullet();

            newBullet.gameObject.SetActive(true);

            newBullet.Fire(transform.position, transform.forward);
        }
    }
}
