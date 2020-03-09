using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestJob
{
    public interface IGeometryObjectController
    {
    }

    public class GeometryObjectController : IGeometryObjectController
    {
        private readonly IGeometryObjectModel _model;
        private readonly IGeometryObjectView _view;

        private readonly IObservable<long> _observer;
        private IDisposable _subscription;

        public GeometryObjectController(IGeometryObjectModel model, IGeometryObjectView view)
        {
            _model = model;
            _view = view;

            _view.OnClicked += OnClicked;
            model.OnPositionChanged += HandlePositionChanged;

            SyncPosition();

            _observer = Observable.Interval(TimeSpan.FromSeconds(GameController.gameData.observableTime));

            ResetColorSubscription();
        }

        private void OnClicked(object sender, GeometryObjectClickedEventArgs e)
        {
            _model.ClickCount++;
            if (_model.IsClickCountInRange)
            {
                _view.Color = _model.ClickColorData.color;

                _subscription?.Dispose();
                _subscription = null;
            }
            else if (_subscription == null)
            {
                ResetColorSubscription();
            }
        }

        private void ResetColorSubscription()
        {
            _view.SetRandomColor();
            _subscription = _observer.Subscribe(_ => { _view.SetRandomColor(); });
        }

        private void HandlePositionChanged(object sender, GeometryObjectPositionChangedEventArgs e)
        {
            SyncPosition();
        }

        private void SyncPosition()
        {
            _view.Position = _model.Position;
        }
    }
}