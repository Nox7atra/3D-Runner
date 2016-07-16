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
        protected static GameObject CreateCell(
            SegmentGameObject segmentObj,
            Vector3 localPosition)
        {
            GameObject cell = ObjectsBuilder.
                Instance.CreateObject(
                    ObjectsBuilder.ObjectType.Cell);
            cell.transform.SetParent(segmentObj.transform.transform);
            cell.transform.localPosition
                = localPosition;
            return cell;
        }
        protected static GameObject CreateCell(
            SegmentGameObject segmentObj,
            Vector3 localPosition, Vector3 scale)
        {
            GameObject cell = CreateCell(segmentObj, localPosition);
            cell.transform.localScale 
                = scale;
            return cell;
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
            CreateCell(segmentObj, 
                Vector3.right * length / 2
                + Vector3.down * 0.01f
                + Vector3.forward * width /2,
                Vector3.right * length
                + Vector3.up
                + Vector3.forward * (width + 1));
        }
        private static void CreateFloor(
            SegmentGameObject segmentObj, 
            List<Line> lines, 
            int length)
        {
            CreateFloor(segmentObj, lines.Count, length);
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
                }
            }
        }
        protected static void CreateWalls(
            SegmentGameObject segmentObj, 
            int width, 
            int length)
        {
            //Create left wall cell
            CreateCell(segmentObj,
                Vector3.right * length / 2
                + Vector3.up * WALL_HEIGHT/2
                - Vector3.forward, 
                Vector3.right * length
                + Vector3.up * WALL_HEIGHT
                + Vector3.forward);

            //Create right wall cell
            CreateCell(segmentObj,
            Vector3.right * length / 2
            + Vector3.up * WALL_HEIGHT / 2
            + Vector3.forward * width,
            Vector3.right * length
            + Vector3.up * WALL_HEIGHT
            + Vector3.forward);
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