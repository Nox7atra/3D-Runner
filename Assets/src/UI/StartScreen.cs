using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.UI
{
    public class StartScreen : MonoBehaviour
    {
        public void OnStartButton()
        {
            GameMainController.Instance.StartGame();
            gameObject.SetActive(false);
        }
    }
}