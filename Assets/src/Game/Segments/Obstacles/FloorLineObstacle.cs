using UnityEngine;
using Runner.Core;

namespace Runner.Game.Segments.Obstacles
{
    public class FloorLineObstacle : TriggerableLineObstacle
    {
        private float _Speed;
        public float Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
            }
        }
        public override ObstacleObject Create(Transform parentTransform,
            Vector3 localPosition)
        {
            GameObject obj;
            obj = ObjectsBuilder.Instance
                .CreateObject(
                ObjectsBuilder.ObjectType.FloorObstacle);
            obj.transform.SetParent(parentTransform.transform);
            obj.transform.localPosition
                = localPosition;

            FloorLineObstacleObject floorObj
                = obj.GetComponent<FloorLineObstacleObject>();
            floorObj.SetPropeties(_Speed, _TriggerRange);
            return floorObj;
        }
    }
}