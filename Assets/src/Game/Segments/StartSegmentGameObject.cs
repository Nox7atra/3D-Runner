using UnityEngine;
using System.Collections;
using Runner.Core;
namespace Runner.Game.Segments
{
    public class StartSegmentGameObject : SegmentGameObject
    {
        protected override void Start()
        {
            CreateFloor(this, 
                BalanceManager.Instance.StartSegmentWidth,
                BalanceManager.Instance.StartSegmentLength);
            CreateWalls(this,
                BalanceManager.Instance.StartSegmentWidth,
                BalanceManager.Instance.StartSegmentLength);
        }

    }
}