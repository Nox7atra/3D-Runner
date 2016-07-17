using UnityEngine;
using System.Collections;
using Runner.Core;
using Runner.Core.User;
namespace Runner.Game
{
    public class Player : MonoBehaviour
    {
        private const float TIME_TO_ACCEL = 1f;
        [SerializeField]
        private CharacterController _Body;
        [SerializeField]
        private GameObject _CorridorGraph;
        private Vector3 _StartPosition;
        private float _ForwardSpeed;
        private float _SideSpeed;
        private float _DeltaTime;
        void Start()
        {
            _StartPosition = transform.position;
            _ForwardSpeed = BalanceManager.Instance.StartForwardSpeed;
            _SideSpeed = 0;
            _DeltaTime = 0;
            
        }
        void Update()
        {
            if (GameMainController.Instance.GameIsPaused)
            {
                return;
            }
            
            Controls();
            UserData.Instance.SetCurrentScore(
                transform.position.x - _StartPosition.x);
            //Ускорение в течением времени
            _DeltaTime += Time.deltaTime;
            if(_DeltaTime > TIME_TO_ACCEL)
            {
                if (_ForwardSpeed < BalanceManager.Instance.MaxForwardSpeed)
                {
                    _ForwardSpeed += BalanceManager.Instance.Acceleration;
                }
                else
                {
                    _ForwardSpeed = BalanceManager.Instance.MaxForwardSpeed;
                }
                _DeltaTime = 0;
            }
            //Передвижение
            _Body.Move((Vector3.right * _ForwardSpeed
                + Vector3.forward * _SideSpeed) * Time.deltaTime);
            //Двигаем коридорную графику за персонажем
            _CorridorGraph.transform.position
                = Vector3.right * transform.position.x
                + Vector3.up * _CorridorGraph.transform.position.y
                + Vector3.forward * _CorridorGraph.transform.position.z;
        }
        private void Controls()
        {
#if UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.LeftArrow) 
                || Input.GetKeyUp(KeyCode.A)
                || Input.GetKeyUp(KeyCode.RightArrow)
                || Input.GetKeyUp(KeyCode.D))
            {
                _SideSpeed = 0;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                _SideSpeed = BalanceManager.Instance.SideSpeed;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                _SideSpeed = -BalanceManager.Instance.SideSpeed;
            }
            
#endif
        }
    }
}