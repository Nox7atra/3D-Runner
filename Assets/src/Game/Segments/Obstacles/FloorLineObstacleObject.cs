using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game.Segments.Obstacles
{
    public class FloorLineObstacleObject : ObstacleObject
    {
        private const float MAX_HEIGHT = 1f;
        private float _Speed;
        void Update()
        {
            if (!GameMainController.Instance.GameIsPaused && _IsActivated)
            {
                Move();
            }
        }
        private void Move()
        {
            if (transform.position.y >= MAX_HEIGHT)
            {
                transform.position = new Vector3(
                    transform.position.x, 
                    MAX_HEIGHT,
                    transform.position.z);
            }
            else
            {
                transform.position += Vector3.up * _Speed;
            }
        }
        public void SetPropeties(float speed,  int triggerRange)
        {
            _Speed = speed;
            _Trigger.transform.localPosition = Vector3.left * triggerRange;
        }
    }
}