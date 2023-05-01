using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Camera _virtualCamera;
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private Transform _avatar;
        [SerializeField]
        private Vector3 _offset;

        private void LateUpdate()
        {
            var direction = _avatar.transform.position - _target.transform.position;
            direction.y = 0;

            var lookRotation = Quaternion.LookRotation(_avatar.position - _virtualCamera.transform.position);
            var cameraPosition = _avatar.position + direction.normalized * _offset.z + Vector3.up * _offset.y;

            _virtualCamera.transform.position = cameraPosition;
            _virtualCamera.transform.rotation = lookRotation;
        }
    }
}