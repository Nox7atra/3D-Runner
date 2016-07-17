using UnityEngine;
using System.Collections.Generic;
using Runner.Core;
using Runner.Game.Segments;
namespace Runner.Game.Generators
{
    public class TraceGenerator : MonoBehaviour
    {
        private const int MIN_TRACE_LENGTH = 200;
        [SerializeField]
        private GameObject _Player;

        private List<SegmentGameObject> _SegmentsPool;
        private List<SegmentGameObject> _SegmentsInUse;
        private int _LastSegmentPosition;
        public void Init()
        {
            _LastSegmentPosition = BalanceManager.Instance.StartSegmentLength;
            _SegmentsPool = new List<SegmentGameObject>();
            _SegmentsInUse = new List<SegmentGameObject>();
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
            CheckTraceLength();
        }

        void FixedUpdate()
        {
            FreedomToSegment();
            CheckTraceLength();
        }
        private int AddSegment()
        {
            SegmentGameObject freeSegment = GetRandomFreeSegment();
            freeSegment.transform.position = Vector3.right * _LastSegmentPosition;
            _LastSegmentPosition += freeSegment.SegmentLength;
            freeSegment.SetInUse();
            _SegmentsInUse.Add(freeSegment);
            return freeSegment.SegmentLength;
        }
        private void CheckTraceLength()
        {
            int traceLength = 0;
            foreach(var segment in _SegmentsInUse)
            {
                traceLength += segment.SegmentLength;
            }
            while(traceLength < MIN_TRACE_LENGTH)
            {
                traceLength += AddSegment();
            }
        }
        private void FreedomToSegment()
        {
            foreach(var segment in _SegmentsInUse)
            {
                if(_Player.transform.position.x > 
                    segment.transform.position.x + segment.SegmentLength 
                    + 5f)
                {
                    segment.SetFree();
                }
            }
            _SegmentsInUse.RemoveAll(x => x.CurrentState == SegmentGameObject.State.Free);
        }
        private SegmentGameObject GetRandomFreeSegment()
        {
            List<SegmentGameObject> freeSegments = _SegmentsPool.FindAll(x =>
                x.CurrentState == SegmentGameObject.State.Free);
            return freeSegments[Random.Range(0, freeSegments.Count)];
        }
    }
}