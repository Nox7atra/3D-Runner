using UnityEngine;
using System.Collections.Generic;
namespace Runner.Game.Segments
{
    public class Line
    {
        private List<LineObstacle> _Obstacles;
        public List<LineObstacle> Obstacles
        {
            get
            {
                return _Obstacles;
            }
        }
        public Line()
        {
            _Obstacles = new List<LineObstacle>();
        }
    }
}