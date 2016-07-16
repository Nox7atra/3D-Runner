using UnityEngine;
using System.Collections;
using System;
namespace Runner.Game.Segments.Obstacles
{
    public class TriggerableLineObstacle : LineObstacle
    {
        //Расстояние до линии на которой срабатывает триггер
        protected int _TriggerRange;

        public int TriggerRange
        {
            get
            {
                return _TriggerRange;
            }
            set
            {
                _TriggerRange = value;
            }
        }
        public override ObstacleObject Create(Transform parentTransform,
            Vector3 localPosition) 
        {
            throw new NotImplementedException();
        }
    }
}