using UnityEngine;
using System.Collections;
namespace Runner.Game.Segments.Obstacles {
    public class ObstacleTrigger : MonoBehaviour {

        [SerializeField]
        private ObstacleObject _Obstacle;
        [SerializeField]
        private BoxCollider _BoxCollider;
        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ActivateObstacle();
            }
        }
        private void ActivateObstacle()
        {
            _Obstacle.IsActivated = true;
        }
    }
}