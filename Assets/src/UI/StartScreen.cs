using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.UI
{
    public class StartScreen : MonoBehaviour
    {
        public void OnStartButton()
        {
            ModulesContoller.Instance
                .Modules.GetModule<HUD>()
                .gameObject.SetActive(true);
            GameMainController.Instance.StartGame();
            gameObject.SetActive(false);
        }
    }
}