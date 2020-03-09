using System;
using UnityEngine;

namespace TestJob
{
    public class GeometryObjectPositionChangedEventArgs : EventArgs
    {
    }

    public interface IGeometryObjectModel
    {
        event EventHandler<GeometryObjectPositionChangedEventArgs> OnPositionChanged;
        Vector3 Position { get; set; }
        int ClickCount { get; set; }
        ClickColorData ClickColorData { get; }
        bool IsClickCountInRange { get; }
    }

    public class GeometryObjectModel : IGeometryObjectModel
    {
        public event EventHandler<GeometryObjectPositionChangedEventArgs> OnPositionChanged = (sender, e) => { };

        private Vector3 _position;

        public Vector3 Position
        {
            get => _position;
            set
            {
                if (_position == value) return;
                _position = value;
                var eventArgs = new GeometryObjectPositionChangedEventArgs();
                OnPositionChanged(this, eventArgs);
            }
        }

        public int ClickCount { get; set; }

        public ClickColorData ClickColorData { get; }

        public bool IsClickCountInRange => ClickCount >= ClickColorData.minClicksCount && ClickCount <= ClickColorData.maxClicksCount;

        public GeometryObjectModel(ClickColorData clickColorData)
        {
            ClickColorData = clickColorData;
        }
    }
}