using UnityEngine;
using System.Collections;
using System;
namespace Runner.Game.Segments.Obstacles
{
    public class TriggerableLineObstacle : LineObstacle
    {
        //Расстояние до линии на которой срабатывает триггер
        public int TriggerRange;
        public override GameObject Create(Transform parentTransform,
            Vector3 localPosition) 
        {
            throw new NotImplementedException();
        }
    }
}