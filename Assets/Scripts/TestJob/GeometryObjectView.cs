using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

#pragma warning disable 649

namespace TestJob
{
    public class GeometryObjectClickedEventArgs : EventArgs
    {
    }

    public interface IGeometryObjectView
    {
        event EventHandler<GeometryObjectClickedEventArgs> OnClicked;
        Color Color { set; }
        Vector3 Position { set; }
        void SetRandomColor();
    }

    public class GeometryObjectView : MonoBehaviour, IGeometryObjectView
    {
        [Inject] private Renderer _renderer;

        private static readonly int ColorID = Shader.PropertyToID("_Color");

        [Inject] private Camera _camera;

        public event EventHandler<GeometryObjectClickedEventArgs> OnClicked = (sender, e) => { };

        public Color Color
        {
            set => _renderer.material.SetColor(ColorID, value);
        }

        public Vector3 Position
        {
            set => transform.position = value;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit) || hit.transform != transform) return;
            var eventArgs = new GeometryObjectClickedEventArgs();
            OnClicked(this, eventArgs);
        }

        public void SetRandomColor()
        {
            Color = new Color(Random.value, Random.value, Random.value);
        }
    }
}