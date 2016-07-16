using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game
{
    public class Player : MonoBehaviour
    {
        private const float TIME_TO_ACCEL = 0.5f;
        private const float GRAVITY = 20f;
        [SerializeField]
        private CharacterController _Body;

        private Vector3 _StartPosition;
        private float _ForwardSpeed;
        private float _DeltaTime;
        void Start()
        {
            _StartPosition = transform.position;
            _DeltaTime = 0;
        }
        void Update()
        {
            if (GameMainController.Instance.GameIsPaused)
            {
                return;
            }
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
            _Body.Move((Vector3.right * _ForwardSpeed + Vector3.down * GRAVITY) * Time.deltaTime);
        }
    }
}