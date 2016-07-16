using UnityEngine;
using System.Collections.Generic;
using Runner.Core;
using Runner.Game.Segments;
namespace Runner.Game.Generators
{
    public class TraceGenerator : MonoBehaviour
    {
        private List<SegmentGameObject> _SegmentsPool;
        private int _LastSegmentPosition;
        public void Init()
        {
            _LastSegmentPosition = BalanceManager.Instance.StartSegmentLength;
            _SegmentsPool = new List<SegmentGameObject>();
            foreach (var segment in SegmentManager.Instance.Segments)
            {
                for (int i = 0; i < BalanceManager.Instance.MaxSameSegmentsInARow; i++)
                {
                    SegmentGameObject segmObj = SegmentGameObject.Create(segment);
                    _SegmentsPool.Add(segmObj);
                    //Отправляем в нарнию, чтобы не мешался
                    segmObj.transform.position = Vector3.down * 5000f;
                }
            }
        }
        void Start()
        {
            Init();
            for(int i = 0; i < 3; i++)
            {
                AddSegment();
            }
        }

        void Update()
        {

        }
        public void AddSegment()
        {
            SegmentGameObject freeSegment = GetRandomFreeSegment();
            freeSegment.transform.position = Vector3.right * _LastSegmentPosition;
            _LastSegmentPosition += freeSegment.SegmentLength;
            freeSegment.CurrentState = SegmentGameObject.State.InUse;

        }
        public SegmentGameObject GetRandomFreeSegment()
        {
            List<SegmentGameObject> freeSegments = _SegmentsPool.FindAll(x =>
                x.CurrentState == SegmentGameObject.State.Free);
            return freeSegments[Random.Range(0, freeSegments.Count)];
        }
    }
}