using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game
{
    public class Player : MonoBehaviour
    {
        private const float TIME_TO_ACCEL = 0.5f;
        private const float GRAVITY = 20f;
        private const float SIDE_SPEED = 5f;
        [SerializeField]
        private CharacterController _Body;

        private Vector3 _StartPosition;
        private float _ForwardSpeed;
        private float _SideSpeed;
        private float _DeltaTime;
        void Start()
        {
            _StartPosition = transform.position;
            _DeltaTime = 0;
            _SideSpeed = 0;
        }
        void Update()
        {
            if (GameMainController.Instance.GameIsPaused)
            {
                return;
            }
            Controls();
            _DeltaTime += Time.deltaTime;
            if(_DeltaTime > TIME_TO_ACCEL)
            {
                if (_ForwardSpeed < BalanceManager.Instance.MaxSpeed)
                {
                    _ForwardSpeed += BalanceManager.Instance.Acceleration;
                }
                else
                {
                    _ForwardSpeed = BalanceManager.Instance.MaxSpeed;
                }
                _DeltaTime = 0;
            }
            _Body.Move((Vector3.right * _ForwardSpeed
                + Vector3.forward * _SideSpeed) * Time.deltaTime);
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
                _SideSpeed = SIDE_SPEED;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                _SideSpeed = -SIDE_SPEED;
            }
            
#endif
        }
    }
}