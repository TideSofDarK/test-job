using UnityEngine;
using UnityEngine.Serialization;

namespace TestJob
{
    [CreateAssetMenu(fileName = "GameData", menuName = "GameDataScriptableObject", order = 1)]
    public class GameData : ScriptableObject
    {
        [SerializeField] public int observableTime;
    }
}
