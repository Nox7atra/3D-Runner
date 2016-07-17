using UnityEngine;
using UnityEngine.SceneManagement;
namespace Runner.Core
{
    public class GameMainController
    {
        #region singleton description
        private static GameMainController _Instance;
        public static GameMainController Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GameMainController();
                }
                return _Instance;
            }
        }
        private GameMainController()
        {
            GameIsPaused = true;
        }
        #endregion
        private bool _GameIsPaused;
        public bool GameIsPaused
        {
            get
            {
                return _GameIsPaused;
            }
            set
            {
                if (value)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
                _GameIsPaused = value;
            }
        }
        public void StartGame()
        {
            GameIsPaused = false;
            Controls.InitGyro();
        }
        public void GameOver()
        {
            User.UserData.Instance.ApplyBestScore();
        }
        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}