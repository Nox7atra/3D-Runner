using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game.Segments
{
    public class StartSegmentGameObject : SegmentGameObject
    {
        protected override void Start()
        {
            CreateFloor(transform, 
                BalanceManager.Instance.StartSegmentWidth,
                BalanceManager.Instance.StartSegmentLength);
            CreateWalls(transform,
                BalanceManager.Instance.StartSegmentWidth,
                BalanceManager.Instance.StartSegmentLength);
        }

    }
}