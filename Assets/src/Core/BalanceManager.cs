using UnityEngine;
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
        private int _LinesNum;
        private int _MaxSameSegmentsInARow;
        private float _StartSpeed;
        private float _MaxSpeed;
        #endregion
        #region public properties
        public int LinesNum
        {
            get
            {
                return _LinesNum;
            }
        }
        public int MaxSameSegmentsInARow
        {
            get
            {
                return _MaxSameSegmentsInARow;
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
        #endregion
        public void Init()
        {
            ParseBalance();
        }
        private void ParseBalance()
        {
            TextAsset textAsset = (TextAsset)Resources.Load(PathConstants.BALANCE_PATH, typeof(TextAsset));
            StringReader reader = new StringReader(textAsset.text);
            var yaml = new YamlStream();
            yaml.Load(reader);
            YamlMappingNode doc = (YamlMappingNode)yaml.Documents[0].RootNode;
            YamlMappingNode root = (YamlMappingNode)doc.Children[new YamlScalarNode("Balance")];
        }
    }
}