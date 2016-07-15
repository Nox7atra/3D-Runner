using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game.Segments
{
    public class SegmentGameObject : MonoBehaviour
    {
        protected const int WALL_HEIGHT = 6;
        protected virtual void Start()
        {

        }

        void Update()
        {

        }
        protected static void CreateCell(Transform parentTransform, Vector3 localPosition)
        {
            GameObject Cell = ObjectsBuilder.
                        Instance.CreateObject(ObjectsBuilder.ObjectType.Cell);
            Cell.transform.SetParent(parentTransform.transform);
            Cell.transform.localPosition
                = localPosition;
        }
        protected static void CreateFloor(Transform parentTransform, int width, int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    CreateCell(parentTransform, 
                        Vector3.right * i + Vector3.forward * j);
                }
            }
        }
        protected static void CreateWalls(Transform parentTransform, int width, int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < WALL_HEIGHT; j++)
                {
                    //Create left wall cell
                    CreateCell(parentTransform, 
                        Vector3.right * i + Vector3.up * j - Vector3.forward);
                    //Create right wall cell
                    CreateCell(parentTransform,
                        Vector3.right * i + Vector3.up * j + Vector3.forward * width);
                }
            }
        }
        public static SegmentGameObject Create(Segment segment)
        {
            SegmentGameObject segmentObj = new SegmentGameObject();

            CreateFloor(segmentObj.transform, segment.Lines.Count, segment.Length);
            CreateWalls(segmentObj.transform, segment.Lines.Count, segment.Length);

            return segmentObj;
        }
    }
}