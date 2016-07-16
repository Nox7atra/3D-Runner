using UnityEngine;
using System.Collections;
namespace Runner.Game.Segments
{
    public abstract class LineObstacle
    {
        //Позиция на линии в сегменте
        public int PositionOnLine;
        public abstract GameObject Create(Transform parentTransform,
            Vector3 localPosition);
    }
}