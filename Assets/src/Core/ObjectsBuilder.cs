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
            Cell,
            StaticObstacle,
            MovingObstacle,
            FloorObstacle
        }
        private GameObject _CellPrefab;
        private GameObject _StaticObstaclePrefab;
        private GameObject _MovingObstaclePrefab;
        private GameObject _FloorObstaclePrefab;
        private void Init()
        {
            _CellPrefab = Resources.Load(
                PathConstants.CELL_PREFAB_PATH) as GameObject;
            _StaticObstaclePrefab = Resources.Load(
                PathConstants.STATIC_OBSTACLE_PRAFB_PATH) as GameObject;
            _FloorObstaclePrefab = Resources.Load(
                PathConstants.FLOOR_OBSTACLE_PREFAB_PATH) as GameObject;
            _MovingObstaclePrefab = Resources.Load(
                PathConstants.MOVING_OBSTACLE_PREFAB_PATH) as GameObject;
        }

        public GameObject CreateObject(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Cell:
                    return GameObject.Instantiate(_CellPrefab);
                case ObjectType.StaticObstacle:
                    return GameObject.Instantiate(_StaticObstaclePrefab);
                case ObjectType.FloorObstacle:
                    return GameObject.Instantiate(_FloorObstaclePrefab);
                case ObjectType.MovingObstacle:
                    return GameObject.Instantiate(_MovingObstaclePrefab);
            }
            return null;
        }
    }
}