using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraOrienter : MonoBehaviour
    {
        private void Update()
        {
            var direction = Camera.main.transform.position - transform.position;
            direction.x = 0;
            direction.z = 0;
            
            transform.rotation = quaternion.LookRotation(direction, Vector3.up);
        }
    }
}