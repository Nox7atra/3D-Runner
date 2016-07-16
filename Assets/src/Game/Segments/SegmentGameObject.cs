using UnityEngine;
using System.Collections.Generic;
using Runner.Core;
using Runner.Game.Segments.Obstacles;
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
        private State _CurrentState;
        public State CurrentState {
            get
            {
                return _CurrentState;
            }
        }
        private List<ObstacleObject> _ObstaclesPool;
        public SegmentGameObject()
        {
            _CurrentState = State.Free;
            _ObstaclesPool = new List<ObstacleObject>();
        }
        protected virtual void Start()
        {

        }

        void Update()
        {

        }
        protected static void CreateCell(
            SegmentGameObject segmentObj, 
            Vector3 localPosition)
        {
            GameObject cell = ObjectsBuilder.
                Instance.CreateObject(
                    ObjectsBuilder.ObjectType.Cell);
            cell.transform.SetParent(segmentObj.transform.transform);
            cell.transform.localPosition
                = localPosition;
        }
        protected static void CreateObstacle(
            SegmentGameObject segmentObj, 
            Vector3 localPosition, 
            LineObstacle obstacle)
        {
            segmentObj._ObstaclesPool.Add(obstacle.Create(segmentObj.transform, localPosition));      
        }
        protected static void CreateFloor(
            SegmentGameObject segmentObj, 
            int width, 
            int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    CreateCell(segmentObj, 
                        Vector3.right * i + Vector3.forward * j);
                }
            }
        }
        private static void CreateFloor(
            SegmentGameObject segmentObj, 
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
                            segmentObj, 
                            Vector3.right * i + Vector3.forward * j, 
                            obstacle);
                    }
                    else
                    {
                        CreateCell(
                            segmentObj,
                            Vector3.right * i + Vector3.forward * j);
                    }
                }
            }
        }
        protected static void CreateWalls(
            SegmentGameObject segmentObj, 
            int width, 
            int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < WALL_HEIGHT; j++)
                {
                    //Рандом, чтобы стены не были идеально гладкими и отличались
                    if (Random.Range(0, 6) < 4)
                    {
                        //Create left wall cell
                        CreateCell(segmentObj,
                            Vector3.right * i
                            + Vector3.up * j
                            - Vector3.forward);
                    }
                    if (Random.Range(0, 6) < 4)
                    {
                        //Create right wall cell
                        CreateCell(segmentObj,
                        Vector3.right * i
                        + Vector3.up * j
                        + Vector3.forward * width);
                    }
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
            CreateFloor(segmentObj, segment.Lines, segment.Length);
            CreateWalls(segmentObj, segment.Lines.Count, segment.Length);
            return segmentObj;
        }
        public void SetInUse()
        {
            _CurrentState = State.InUse;
        }
        public void SetFree()
        {
            _CurrentState = State.Free;
            foreach(ObstacleObject obstacle in _ObstaclesPool)
            {
                obstacle.Reset();
            }
        }
    }
}