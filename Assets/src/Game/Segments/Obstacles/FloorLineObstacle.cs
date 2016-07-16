using UnityEngine;
using Runner.Core;

namespace Runner.Game.Segments.Obstacles
{
    public class FloorLineObstacle : TriggerableLineObstacle
    {
        public float Speed;
        public override GameObject Create(Transform parentTransform,
            Vector3 localPosition)
        {
            GameObject obj;
            obj = ObjectsBuilder.Instance
                .CreateObject(
                ObjectsBuilder.ObjectType.FloorObstacle);
            obj.transform.SetParent(parentTransform.transform);
            obj.transform.localPosition
                = localPosition;
            return obj;
        }
    }
}