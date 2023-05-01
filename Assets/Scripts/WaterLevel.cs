using UnityEngine;

namespace DefaultNamespace
{
    public class WaterLevel : MonoBehaviour
    {
        [SerializeField]
        private Transform _water;

        public float WaterLevelValue => _water.position.y;
        
        
    }
}