using UnityEngine;

namespace TestJob
{
    public class GameController : MonoBehaviour
    {
        public static GameData gameData;

        private GeometryObjectSpawner _geometryObjectSpawner;
        private Camera _camera;

        private void Awake()
        {
            gameData = Resources.Load<GameData>("GameData");

            _camera = Camera.main;

            _geometryObjectSpawner = new GeometryObjectSpawner();
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition)))
                {
                    Vector3 position =
                        _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
                    position.y = 0;
                    _geometryObjectSpawner.Spawn(position);
                }
            }
        }
    }
}