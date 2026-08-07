using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace EventHorizon
{
    public class StartScreen : MonoBehaviour
    {
        public GameObject startingSelection;

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(startingSelection);
        }

        public void StartTutorial()
        {
            SceneManager.LoadSceneAsync("SCN_Tutorial");
        }

        public void StartNewRun()
        {

        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
