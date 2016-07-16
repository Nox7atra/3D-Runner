using UnityEngine;
using System.Collections.Generic;
namespace Runner.Core
{
    public class Modules : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> _ModulesPrefabs;
        [SerializeField]
        Transform _UIRoot;

        List<GameObject> _Modules;
        void Awake()
        {
            ModulesContoller.Instance.Modules = this;
            _Modules = new List<GameObject>();
            foreach(var prefab in _ModulesPrefabs)
            {
                var module = Instantiate(prefab);
                module.transform.SetParent(_UIRoot, false);
                _Modules.Add(module);
            }
        }
        public T GetModuleByName<T>(string moduleName)
        {
            T mod = default(T);
            foreach (var module in _Modules)
            {
                if (moduleName == module.name)
                {
                    mod = module.GetComponent<T>();
                }
            }
            return mod;
        }
        public T GetModule<T>()
        {
            T mod = default(T);
            foreach (var module in _Modules)
            {
                mod = module.GetComponent<T>();
                if (mod != null)
                {
                    return mod;
                }
            }
            return mod;
        }
    }
}