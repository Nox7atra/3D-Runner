using UnityEngine;
using System;
using System.IO;
using YamlDotNet.RepresentationModel;
namespace Runner.Core
{
    public class BalanceManager
    {
        #region singletone description
        private static BalanceManager _Instance;
        public static BalanceManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BalanceManager();
                }
                return _Instance;
            }
        }
        private BalanceManager()
        {
            Init();
        }
        #endregion
        #region private properties
        private int _MaxSameSegmentsInARow;
        private int _StartSegmentWidth;
        private int _StartSegmentLength;
        private float _StartSpeed;
        private float _MaxSpeed;
        private float _Acceleration;
        #endregion
        #region public properties
        public int MaxSameSegmentsInARow
        {
            get
            {
                return _MaxSameSegmentsInARow;
            }
        }
        public int StartSegmentWidth
        {
            get
            {
                return _StartSegmentWidth;
            }
        }
        public int StartSegmentLength
        {
            get
            {
                return _StartSegmentLength;
            }
        }
        public float StartSpeed
        {
            get
            {
                return _StartSpeed;
            }
        }
        public float MaxSpeed
        {
            get
            {
                return _MaxSpeed;
            }
        }
        public float Acceleration
        {
            get
            {
                return _Acceleration;
            }
        }
        #endregion
        public void Init()
        {
            ParseBalance();
        }
        private void ParseBalance()
        {
            TextAsset textAsset = (TextAsset)Resources.Load(
                PathConstants.BALANCE_PATH, typeof(TextAsset));
            StringReader reader = new StringReader(textAsset.text);
            var yaml = new YamlStream();
            yaml.Load(reader);
            YamlMappingNode doc = (YamlMappingNode)yaml.Documents[0].RootNode;
            YamlMappingNode root = (YamlMappingNode)doc.
                Children[new YamlScalarNode("Balance")];
            foreach(var param in root.Children)
            {
                switch (param.Key.ToString())
                {
                    case "StartSegmentWidth":
                        _StartSegmentWidth 
                            = Convert.ToInt32(param.Value.ToString());
                        break;
                    case "StartSegmentLength":
                        _StartSegmentLength
                            = Convert.ToInt32(param.Value.ToString());
                        break;
                    case "MaxSegmentsInARow":
                        _MaxSameSegmentsInARow 
                            = Convert.ToInt32(param.Value.ToString());
                        break;
                    case "StartSpeed":
                        _StartSpeed = float.Parse(param.Value.ToString());
                        break;
                    case "MaxSpeed":
                        _MaxSpeed = float.Parse(param.Value.ToString());
                        break;
                    case "Acceleration":
                        _Acceleration = float.Parse(param.Value.ToString());
                        break;
                }
            }
        }
    }
}