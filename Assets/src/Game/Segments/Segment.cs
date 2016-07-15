using UnityEngine;
using System.Collections.Generic;
using Runner.Core;
namespace Runner.Game.Segments
{
    public class Segment
    {
        private List<Line> _Lines;

        public int Length;

        public Segment()
        {
            _Lines = new List<Line>(BalanceManager.Instance.LinesNum);
        }
        public Line GetLine(int lineNum)
        {
            if (lineNum < _Lines.Count)
            {
                return _Lines[lineNum];
            }
            else
            {
                Debug.Log("Can't Get this line:" + lineNum.ToString());
                return null;
            }
        }

    }
}