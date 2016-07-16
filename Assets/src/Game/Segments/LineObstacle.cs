using UnityEngine;
using System.Collections;
using Runner.Game.Segments.Obstacles;
using Runner.Core;
namespace Runner.Game.Segments
{
    public class LineObstacle
    {
        //Позиция на линии в сегменте
        public int PositionOnLine;
        public virtual ObstacleObject Create(Transform parentTransform,
            Vector3 localPosition)
        {
            GameObject obj;
            obj = ObjectsBuilder.Instance
                .CreateObject(
                ObjectsBuilder.ObjectType.StaticObstacle);
            obj.transform.SetParent(parentTransform.transform);
            //Добавка на вектор ап, чтобы поднять над полом
            obj.transform.localPosition
                = localPosition + Vector3.up;

            ObstacleObject staticObj
                = obj.GetComponent<ObstacleObject>();
            return staticObj;
        }
    }
}