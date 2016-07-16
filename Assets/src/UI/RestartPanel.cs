using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.UI
{
    public class RestartPanel : MonoBehaviour
    {
        public void Show()
        {
            GameMainController.Instance.GameIsPaused = true;
            gameObject.SetActive(true);
        }
        public void OnRestartButton()
        {
            GameMainController.Instance.Restart();
        }
    }
}