using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Runner.Core.User;
using Runner.Core;
namespace Runner.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private Text _Score;
        void Update()
        {
            _Score.text = "Score: " + UserData.Instance.CurrentScore.ToString();
        }

        public void OnPauseButton()
        {
            GameMainController.Instance.GameIsPaused 
                = !GameMainController.Instance.GameIsPaused;
        }
    }
}