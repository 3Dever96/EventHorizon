using UnityEngine;

namespace EventHorizon
{
    public class Information : MonoBehaviour
    {
        [SerializeField] private GameObject backDrop;
        [SerializeField] private TMPro.TMP_Text text;

        public void DisplayText(string newText)
        {
            backDrop.SetActive(true);
            text.text = newText;
        }

        public void HideText()
        {
            backDrop.SetActive(false);
        }
    }
}
