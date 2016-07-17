using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Runner.Core;
using YamlDotNet.RepresentationModel;
using Runner.Game.Segments.Obstacles;
namespace Runner.Game.Segments
{
    public class SegmentManager
    {
        #region singletone description
        private static SegmentManager _Instance;
        public static SegmentManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SegmentManager();
                }
                return _Instance;
            }
        }
        private SegmentManager()
        {
            Init();
        }
        #endregion
        List<Segment> _Segments;

        public List<Segment> Segments
        {
            get
            {
                return _Segments;
            }
        }
        public void Init()
        {
            _Segments = new List<Segment>();
            ParseSegments();
        }
        private void ParseSegments()
        {
            TextAsset textAsset = (TextAsset)Resources.Load(
                PathConstants.SEGMENTS_DATA_PATH, typeof(TextAsset));
            StringReader reader = new StringReader(textAsset.text);
            var yaml = new YamlStream();
            yaml.Load(reader);
            YamlMappingNode doc = (YamlMappingNode)yaml.Documents[0].RootNode;
            YamlMappingNode root = (YamlMappingNode)doc.
                Children[new YamlScalarNode("Segments")];
            foreach(var segmentData in root.Children)
            {
                Segment segment = new Segment();
                foreach (var param in ((YamlMappingNode)segmentData.Value).Children)
                {
                    switch (param.Key.ToString())
                    {
                        case "Length":
                            segment.Length = System.Convert.ToInt32(
                                param.Value.ToString());
                            break;
                        case "Lines":
                            ParseLines((YamlMappingNode)param.Value, ref segment);
                            break;
                    }
                }
                _Segments.Add(segment);
            }
            reader.Close();
        }
        private void ParseLines(YamlMappingNode lines, ref Segment segment)
        {
            foreach(var lineData in lines.Children)
            {
                Line line = new Line();
                YamlMappingNode lineMapping = lineData.Value as YamlMappingNode;
                if (lineMapping != null)
                {

                    foreach (var param in lineMapping.Children)
                    {
                        switch (param.Key.ToString())
                        {
                            case "Obstacles":
                                ParseObstacles((YamlMappingNode)param.Value, ref line);
                                break;
                        }
                    }
                }
                segment.Lines.Add(line);
            }
        }
        private void ParseObstacles(YamlMappingNode obstacles, ref Line line)
        {
            foreach(var obstacleData in obstacles.Children)
            {
                LineObstacle obstacle = null;
                YamlMappingNode obstacleMapping = (YamlMappingNode)obstacleData.Value;
                foreach (var param in obstacleMapping.Children)
                {
                    switch (param.Value.ToString())
                    {
                        case "StaticObstacle":
                            obstacle = ParseStaticObstacle(obstacleMapping);
                            break;
                        case "MovingObstacle":
                            obstacle = ParseMovingObstacle(obstacleMapping);
                            break;
                        case "FloorObstacle":
                            obstacle = ParseFloorObstacle(obstacleMapping);
                            break;
                    }
                }
                
                if (obstacle != null)
                {
                    line.Obstacles.Add(obstacle);
                }
            }
        }
        private LineObstacle ParseStaticObstacle(YamlMappingNode obstacleData)
        {
            LineObstacle obstacle = new LineObstacle();
            foreach (var data in obstacleData.Children)
            {
                switch (data.Key.ToString())
                {
                    case "Position":
                        obstacle.PositionOnLine
                            = System.Convert.ToInt32(data.Value.ToString());
                        break;
                }
            }
            return obstacle;
        }
        private MovingLineObstacle ParseMovingObstacle(YamlMappingNode obstacleData)
        {
            MovingLineObstacle obstacle = new MovingLineObstacle();
            foreach(var data in obstacleData.Children)
            {
                switch (data.Key.ToString())
                {
                    case "Position":
                        obstacle.PositionOnLine 
                            = System.Convert.ToInt32(data.Value.ToString());
                        break;
                    case "TriggerRange":
                        obstacle.TriggerRange 
                            = System.Convert.ToInt32(data.Value.ToString());
                        break;
                    case "Speed":
                        obstacle.Speed =
                            float.Parse(data.Value.ToString());
                        break;
                    case "Direction":
                        obstacle.SetDirection(data.Value.ToString());
                        break;
                }
            }
            return obstacle;
        }
        private FloorLineObstacle ParseFloorObstacle(YamlMappingNode obstacleData)
        {
            FloorLineObstacle obstacle = new FloorLineObstacle();
            foreach (var data in obstacleData.Children)
            {
                switch (data.Key.ToString())
                {
                    case "Position":
                        obstacle.PositionOnLine
                            = System.Convert.ToInt32(data.Value.ToString());
                        break;
                    case "TriggerRange":
                        obstacle.TriggerRange
                            = System.Convert.ToInt32(data.Value.ToString());
                        break;
                    case "Speed":
                        obstacle.Speed =
                            float.Parse(data.Value.ToString());
                        break;
                }
            }
            return obstacle;
        }
    }
}