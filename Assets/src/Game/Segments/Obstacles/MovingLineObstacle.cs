using UnityEngine;
using System.Collections;
using System;
using Runner.Core;
namespace Runner.Game.Segments.Obstacles
{
    public class MovingLineObstacle : TriggerableLineObstacle
    {
        private Vector3 _Direction;
        public float Speed;
        public override GameObject Create(Transform parentTransform,
            Vector3 localPosition)
        {
            GameObject obj;
            obj = ObjectsBuilder.Instance
                .CreateObject(
                ObjectsBuilder.ObjectType.MovingObstacle);
            obj.transform.SetParent(parentTransform.transform);
            //Добавка на вектор ап, чтобы поднять над полом
            obj.transform.localPosition
                = localPosition + Vector3.up;
            return obj;
        }
        public void SetDirection(string direction)
        {
            switch (direction.ToLower())
            {
                case "forward":
                    _Direction = Vector3.right;
                    break;
                case "back":
                    _Direction = Vector3.left;
                    break;
                case "left":
                    _Direction = Vector3.forward;
                    break;
                case "right":
                    _Direction = Vector3.back;
                    break;
            }
        }
    }
}