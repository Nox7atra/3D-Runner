using UnityEngine;
using System.Collections.Generic;
using Runner.Core;
using Runner.Game.Segments;
namespace Runner.Game.Generators
{
    public class TraceGenerator : MonoBehaviour
    {
        private List<SegmentGameObject> _SegmentsPool;
        public void Init()
        {
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
        void Start() {
            Init();
        }

        void Update() {

        }
    }
}