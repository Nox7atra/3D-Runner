using UnityEngine;
using System.Collections;
using Runner.Game.Segments.Obstacles;
namespace Runner.Game.Segments
{
    public abstract class LineObstacle
    {
        //Позиция на линии в сегменте
        public int PositionOnLine;
        public abstract ObstacleObject Create(Transform parentTransform,
            Vector3 localPosition);
    }
}