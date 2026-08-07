using EventHorizon.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace EventHorizon
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void SetValue(HealthSystem health)
        {
            slider.value = health.currentHp;
        }
    }
}
