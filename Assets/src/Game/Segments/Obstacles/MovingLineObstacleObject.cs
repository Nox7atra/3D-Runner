using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game.Segments.Obstacles
{
    public class MovingLineObstacleObject : ObstacleObject
    {
        private float _Speed;
        private Vector3 _Direction;

        void Update()
        {
            if(!GameMainController.Instance.GameIsPaused && _IsActivated)
            {
                Move();
            }
        }
        private void Move()
        {
            transform.position += _Direction * _Speed;
        }
        public void SetPropeties(float speed, Vector3 direction, int triggerRange)
        {
            _Speed = speed;
            _Direction = direction;
            _Trigger.transform.localPosition = Vector3.left * triggerRange;
        }
    }
}
