using UnityEngine;
using System.Collections.Generic;
using Runner.Core;
namespace Runner.Game.Segments
{
    public class SegmentGameObject : MonoBehaviour
    {
        protected const int WALL_HEIGHT = 6;
        //Состояния используемые для генератора
        public enum State
        {
            Free,
            InUse
        }
        private int _SegmentLength;
        public int SegmentLength
        {
            get
            {
                return _SegmentLength;
            }
        }
        [HideInInspector]
        public State CurrentState;
        public SegmentGameObject()
        {
            CurrentState = State.Free;
        }
        protected virtual void Start()
        {

        }

        void Update()
        {

        }
        protected static void CreateCell(
            Transform parentTransform, 
            Vector3 localPosition)
        {
            GameObject cell = ObjectsBuilder.
                Instance.CreateObject(
                    ObjectsBuilder.ObjectType.Cell);
            cell.transform.SetParent(parentTransform.transform);
            cell.transform.localPosition
                = localPosition;
        }
        protected static void CreateObstacle(
            Transform parentTransform, 
            Vector3 localPosition, 
            LineObstacle obstacle)
        {
             obstacle.Create(parentTransform, localPosition);      
        }
        protected static void CreateFloor(
            Transform parentTransform, 
            int width, 
            int length)
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
        private static void CreateFloor(
            Transform parentTransform, 
            List<Line> lines, 
            int length)
        {
            for(int j = 0; j < lines.Count; j++)
            {
                for(int i = 0; i < length; i++)
                {
                    List<LineObstacle> obstacles = lines[j].Obstacles;
                    LineObstacle obstacle =
                        obstacles.Find(x => x.PositionOnLine == i); 
                    if (obstacle != null)
                    {
                        CreateObstacle(
                            parentTransform, 
                            Vector3.right * i + Vector3.forward * j, 
                            obstacle);
                    }
                    else
                    {
                        CreateCell(
                            parentTransform,
                            Vector3.right * i + Vector3.forward * j);
                    }
                }
            }
        }
        protected static void CreateWalls(
            Transform parentTransform, 
            int width, 
            int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < WALL_HEIGHT; j++)
                {
                    //Create left wall cell
                    CreateCell(parentTransform, 
                        Vector3.right * i
                        + Vector3.up * j 
                        - Vector3.forward);
                    //Create right wall cell
                    CreateCell(parentTransform,
                        Vector3.right * i 
                        + Vector3.up * j 
                        + Vector3.forward * width);
                }
            }
        }
        public static SegmentGameObject Create(Segment segment)
        {
            GameObject gameobj = new GameObject();
            gameobj.name = "TraceSegment";
            SegmentGameObject segmentObj
                = gameobj.AddComponent<SegmentGameObject>();
            segmentObj._SegmentLength = segment.Length;
            CreateFloor(segmentObj.transform, segment.Lines, segment.Length);
            CreateWalls(segmentObj.transform, segment.Lines.Count, segment.Length);
            return segmentObj;
        }
    }
}