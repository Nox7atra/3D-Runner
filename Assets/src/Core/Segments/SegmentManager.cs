using UnityEngine;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.RepresentationModel;
namespace Runner.Core.Segments
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
        public void Init()
        {
            _Segments = new List<Segment>();
            ParseSegments();
        }
        private void ParseSegments()
        {
            TextAsset textAsset = (TextAsset)Resources.Load(PathConstants.SEGMENTS_DATA_PATH, typeof(TextAsset));
            StringReader reader = new StringReader(textAsset.text);
            var yaml = new YamlStream();
            yaml.Load(reader);
            YamlMappingNode doc = (YamlMappingNode)yaml.Documents[0].RootNode;
            YamlMappingNode root = (YamlMappingNode)doc.Children[new YamlScalarNode("Segments")];
        }
    }
}