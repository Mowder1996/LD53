using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class MovingController : MonoBehaviour
    {   
        [SerializeField]
        private CityManager _cityManager;
        [SerializeField]
        private AvatarController _avatar;
        [SerializeField]
        private KeyController _keyPrefab;
        [SerializeField]
        private Vector3 _startPoint;
        [SerializeField]
        private Vector3 _limits;

        private Dictionary<KeyController, IPlatform> _keyBindings;
        private IPlatform _currentPlatform;

        private void Awake()
        {
            _keyBindings = new Dictionary<KeyController, IPlatform>();
        }

        private void Start()
        {
            var platform = _cityManager.GetNearestPlatform(_startPoint);
            _currentPlatform = platform;
            _avatar.SetPosition(platform.GroundPoint);
            
            DrawKeys(_cityManager.GetNextPlatforms(_currentPlatform, _limits));
        }

        private void Update()
        {
            if (_keyBindings == null || !_keyBindings.Any())
            {
                return;
            }

            if (_avatar.IsJumping)
            {
                return;
            }
            
            foreach (var pair in _keyBindings)
            {
                if (pair.Key.IsKeyDown())
                {
                    _currentPlatform = pair.Value;
                    DrawKeys(_cityManager.GetNextPlatforms(_currentPlatform, _limits));
                    
                    var sequence = _avatar.ChangePlatform(_currentPlatform, pair.Value);
                    sequence.Play();
                }
            }
        }

        private void DrawKeys(IPlatform[] platforms)
        {
            foreach (var key in _keyBindings.Keys)
            {
                DestroyImmediate(key.gameObject);
            }
            
            _keyBindings.Clear();
            
            var keys = new KeyController[Mathf.Min(platforms.Length, 6)];
            var indexes = new List<int>() {0, 1, 2, 3, 4, 5};
            var cameraSight = -Camera.main.transform.forward;
            // cameraSight.x = 0;
            // cameraSight.z = 0;
            
            for (var i = 0; i < keys.Length; i++)
            {
                var keyPosition = platforms[i].GroundPoint + Vector3.up * 0.05f;
                var keyRotation = Quaternion.LookRotation(Vector3.up, -cameraSight);

                var keyController = Instantiate(_keyPrefab, keyPosition, keyRotation);
                var index = indexes[Random.Range(0, indexes.Count)];
                indexes.Remove(index);
                
                keyController.SetIndex(index);
                
                _keyBindings.Add(keyController, platforms[i]);
            }
        }
        
        // public void OnDrawGizmos()
        // {
        //     if (!Application.isPlaying)
        //     {
        //         return;
        //     }
        //     
        //     var nextPlatforms = _cityManager.GetNextPlatforms(_currentPlatform, _limits);
        //
        //     Gizmos.color = Color.red;
        //
        //     for (var i = 0; i < nextPlatforms.Length; i++)
        //     {
        //         Gizmos.DrawSphere(nextPlatforms[i].GroundPoint, 0.1f);
        //     }
        // }
    }
}