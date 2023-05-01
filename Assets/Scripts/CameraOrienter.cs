using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraOrienter : MonoBehaviour
    {
        private void Update()
        {
            var rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
            rotation.x = 0;
            rotation.z = 0;

            transform.rotation = rotation;
        }
    }
}