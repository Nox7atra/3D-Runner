using UnityEngine;
using System.Collections;
namespace Runner.Core
{
    public class ObjectsBuilder
    {
        #region singletone description
        private static ObjectsBuilder _Instance;
        public static ObjectsBuilder Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ObjectsBuilder();
                }
                return _Instance;
            }
        }
        private ObjectsBuilder()
        {
            Init();
        }
        #endregion
        public enum ObjectType
        {
            Cell
        }
        private GameObject _CellPrefab;
        private void Init()
        {
            _CellPrefab = Resources.Load(PathConstants.CELL_PREFAB_PATH) as GameObject;
        }

        public GameObject CreateObject(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Cell:
                    return GameObject.Instantiate(_CellPrefab);
            }
            return null;
        }
    }
}