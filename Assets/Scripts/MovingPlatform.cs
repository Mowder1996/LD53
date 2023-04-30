using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovingPlatform : BasePlatform
    {
        [SerializeField]
        private Vector3[] _pathPoints;
        [SerializeField]
        private float _duration;

        private Tween _animationTween;
        
        private void Start()
        {
            var path = _pathPoints.Select(item => Platform.position + item).ToArray();
            _animationTween = Platform.DOPath(path, _duration).SetLoops(-1, LoopType.Yoyo);
            _animationTween.Play();
        }

        private void OnDestroy()
        {
            _animationTween.Kill();
        }
    }
}