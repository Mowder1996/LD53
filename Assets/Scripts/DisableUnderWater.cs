using UnityEngine;

namespace DefaultNamespace
{
    public class DisableUnderWater : MonoBehaviour
    {
        private WaterLevel _waterLevel;

        private void Awake()
        {
            _waterLevel = FindObjectOfType<WaterLevel>();
        }

        private void Update()
        {
            if (_waterLevel == null)
            {
                return;
            }
            
            if (transform.position.y < _waterLevel.WaterLevelValue)
            {
                DestroyImmediate(gameObject);
            }
        }
    }
}