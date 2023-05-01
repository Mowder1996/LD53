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
        [SerializeField]
        private Material _highlightMaterial;

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
                    if (pair.Value.TryGetSupplier(out var supplier))
                    {
                        DrawKeys(_cityManager.GetNextPlatforms(_currentPlatform, _limits));
                        
                        _avatar.GetSupply(supplier);
                        
                        return;
                    }
                    
                    if (pair.Value.TryGetReceiver(out var receiver))
                    {
                        _avatar.TrySpendSupply(receiver);

                        DrawKeys(_cityManager.GetNextPlatforms(_currentPlatform, _limits));

                        return;
                    }
                    
                    _currentPlatform = pair.Value;
                    DrawKeys(_cityManager.GetNextPlatforms(_currentPlatform, _limits));

                    var sequence = _avatar.ChangePlatform(_currentPlatform, pair.Value);
                    sequence.Play();
                }
            }
        }

        private void DrawKeys(IPlatform[] platforms)
        {
            foreach (var platform in _keyBindings.Values)
            {
                platform.HideHighlight();
            }
            
            foreach (var key in _keyBindings.Keys)
            {
                DestroyImmediate(key.gameObject);
            }
            
            _keyBindings.Clear();
            
            var keys = new KeyController[Mathf.Min(platforms.Length, 6)];
            var indexes = new List<int>() {0, 1, 2, 3, 4, 5};
            var cameraSight = -Camera.main.transform.forward;

            for (var i = 0; i < keys.Length; i++)
            {
                platforms[i].Highlight(_highlightMaterial);

                Vector3 keyPosition;
                Quaternion keyRotation;

                if (platforms[i].TryGetSupplier(out var supplier))
                {
                    keyPosition = platforms[i].GroundPoint + Vector3.up * 0.5f;
                    keyRotation = Quaternion.LookRotation(Camera.main.transform.position - keyPosition);
                }
                else if (platforms[i].TryGetReceiver(out var receiver))
                {
                    keyPosition = platforms[i].GroundPoint + Vector3.up * 0.75f;
                    keyRotation = Quaternion.LookRotation(Camera.main.transform.position - keyPosition);
                }
                else
                {
                    keyPosition = platforms[i].GroundPoint + Vector3.up * 0.05f;
                    keyRotation = Quaternion.LookRotation(Vector3.up, -cameraSight);   
                }

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