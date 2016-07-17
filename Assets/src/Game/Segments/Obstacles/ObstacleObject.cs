using UnityEngine;
using Runner.Core;
using Runner.UI;
namespace Runner.Game.Segments.Obstacles
{
    public class ObstacleObject : MonoBehaviour
    {
        private Vector3 _StartLocalPosition;
        [SerializeField]
        protected GameObject _Trigger;
        protected bool _IsActivated;

        public bool IsActivated
        {
            get
            {
                return _IsActivated;
            }
            set
            {
                _IsActivated = value;
            }
        }
        protected virtual void Start()
        {
            _StartLocalPosition = transform.localPosition;
        }
        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameMainController.Instance.GameOver();
                ModulesContoller.Instance.
                    Modules.GetModule<RestartPanel>().Show();
            }
        }
        public virtual void Reset()
        {
            _IsActivated = false;
            transform.localPosition = _StartLocalPosition;
        }
    }
}