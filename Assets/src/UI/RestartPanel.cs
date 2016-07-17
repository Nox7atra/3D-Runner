using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Runner.Core;
using Runner.Core.User;
namespace Runner.UI
{
    public class RestartPanel : MonoBehaviour
    {
        [SerializeField]
        private Text _CurrentScore;
        [SerializeField]
        private Text _BestScore;
        public void Show()
        {
            GameMainController.Instance.GameIsPaused = true;
            _CurrentScore.text 
                = "Current Score: " + UserData.Instance.CurrentScore.ToString();
            _BestScore.text
                = "Best Score: " + UserData.Instance.BestScore.ToString();
            gameObject.SetActive(true);
        }
        public void OnRestartButton()
        {
            GameMainController.Instance.Restart();
        }
    }
}