using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

#pragma warning disable 649

namespace TestJob
{
    public class GeometryObjectSpawner
    {
        [Inject] private IInstantiator _instantiator;

        [Serializable]
        private struct GeometryObjectsJson
        {
            public string[] names;

            public string RandomName => names[Random.Range(0, names.Length)];

            public GeometryObjectsJson(string[] names)
            {
                this.names = names;
            }
        }

        private GeometryObjectData _geometryObjectData;
        private readonly GeometryObjectsJson _geometryObjectsJson;
        private readonly Dictionary<string, GameObject> _geometryObjectPrefabs = new Dictionary<string, GameObject>();

        public GeometryObjectSpawner()
        {
            _geometryObjectData = Resources.Load<GeometryObjectData>("GeometryObjectData");

            var geometryObjectTextAsset = Resources.Load<TextAsset>("GeometryObjects");
            _geometryObjectsJson = JsonUtility.FromJson<GeometryObjectsJson>(geometryObjectTextAsset.text);

            foreach (var name in _geometryObjectsJson.names)
            {
                var primitiveAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath,
                    "AssetBundles/" + (Application.platform == RuntimePlatform.Android ? "Android" : "Standalone") +
                    "/" + name.ToLower()));

                _geometryObjectPrefabs.Add(name, primitiveAssetBundle.LoadAsset<GameObject>(name));
            }
        }

        public void Spawn(Vector3 position)
        {
            var geometryObjectName = _geometryObjectsJson.RandomName;

            var clickColorData = _geometryObjectData.FindClickColorData(geometryObjectName);

            var geometryObjectView = _instantiator.InstantiatePrefabForComponent<IGeometryObjectView>(_geometryObjectPrefabs[geometryObjectName]);
            var geometryObjectModel = new GeometryObjectModel(clickColorData);
            var geometryObjectController = new GeometryObjectController(geometryObjectModel, geometryObjectView);

            geometryObjectModel.Position = position;
        }
    }
}