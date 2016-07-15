using UnityEngine;
using System.Collections.Generic;
using Runner.Core;
namespace Runner.Game.Segments
{
    public class Segment
    {
        private List<Line> _Lines;

        public int Length;

        public List<Line> Lines
        {
            get
            {
                return _Lines;
            }
        }
        public Segment()
        {
            _Lines = new List<Line>();
        }

    }
}