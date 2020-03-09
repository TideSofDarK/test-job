using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace TestJob
{
    [CreateAssetMenu(fileName = "GeometryObjectData", menuName = "GeometryObjectDataScriptableObject", order = 0)]
    public class GeometryObjectData : ScriptableObject
    {
        public List<ClickColorData> clicksData;

        public ClickColorData FindClickColorData(string objectType)
        {
            return clicksData.FirstOrDefault(x => x.objectType == objectType);
        }
    }
}