using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game.Segments.Obstacles
{
    public class MovingLineObstacleObject : ObstacleObject
    {
        private float _Speed;
        private Vector3 _Direction;

        public bool IsActivated;
        
        void Update()
        {
            if(!GameMainController.Instance.GameIsPaused && IsActivated)
            {
                Move();
            }
        }
        private void Move()
        {
            transform.position += _Direction * _Speed;
        }
    }
}