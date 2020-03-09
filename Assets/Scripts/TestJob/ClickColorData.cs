using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TestJob
{
    [Serializable]
    public struct ClickColorData
    {
        public string objectType;
        public int minClicksCount;
        public int maxClicksCount;
        public Color color;
    }
}