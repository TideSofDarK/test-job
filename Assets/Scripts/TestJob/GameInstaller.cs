using UnityEngine;
using Zenject;

namespace TestJob
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GeometryObjectSpawner>().AsSingle().NonLazy();

            Container.Bind<Renderer>().FromComponentSibling();
        }
    }
}