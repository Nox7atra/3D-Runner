using UnityEngine;
using Runner.Core;
namespace Runner.Game.Segments.Obstacles
{
    public class ObstacleObject : MonoBehaviour
    {
        void OnTriggerEnter(Collider collision)
        {
            Debug.Log("Bah");
            if (collision.gameObject.CompareTag("Player"))
            {
                GameMainController.Instance.GameOver();
            }
        }
    }
}