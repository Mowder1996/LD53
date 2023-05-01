using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class AvatarController : MonoBehaviour
    {
        [SerializeField]
        private Transform _graphic;
        [SerializeField]
        private int _startIndex;
        [SerializeField]
        private int _jumpIndex;
        [SerializeField]
        private int _collectIndex;
        [SerializeField]
        private GameObject _baloonIcon;
        [SerializeField]
        private GameObject _oxygenIcon;   

        private Material _material;
        
        private bool _isJumping;
        private int _currentIndex;

        public bool IsJumping => _isJumping;

        private bool _hasSupply;
        private SupplyType _supplyType;

        private void Awake()
        {
            _material = _graphic.GetComponentInChildren<MeshRenderer>().material;
            SetIndex(_startIndex);
        }
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public Sequence ChangePlatform(IPlatform currentPlatform, IPlatform nextPlatform)
        {
            var heightDirection =  nextPlatform.GroundPoint.y - currentPlatform.GroundPoint.y;
            var commonDistance = 0.2f + 0.2f + Mathf.Abs(heightDirection);
            var getUpDistance = heightDirection > 0 ? heightDirection + 0.2f : 0.2f;
            var jumpDirection = nextPlatform.GroundPoint - currentPlatform.GroundPoint;
            jumpDirection.y = 0;

            var moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(
                    currentPlatform.GroundPoint + jumpDirection / 2f + Vector3.up * getUpDistance,
                    0.5f * getUpDistance / commonDistance).SetEase(Ease.OutSine))
                .Append(transform.DOMove(nextPlatform.GroundPoint, 0.5f * (commonDistance - getUpDistance) / commonDistance).SetEase(Ease.InSine));

            // var rotateSequence = DOTween.Sequence()
            //     .Append(_graphic.DORotate(Vector3.forward * 360, 0.5f, RotateMode.LocalAxisAdd))
            //     .AppendCallback(() =>
            //     {
            //         _graphic.localRotation = Quaternion.identity;
            //     });
            
            var jumpSequence = DOTween.Sequence();
            jumpSequence
                .PrependCallback(() =>
                {
                    _isJumping = true;
                    _material.SetFloat("_Index", _jumpIndex);
                })
                .Append(moveSequence)
                .AppendCallback(() =>
                {
                    var needChangeIndex = Random.Range(0, 2);

                    if (needChangeIndex > 0)
                    {
                        var randomIndex = Random.Range(0, 4);
                        
                        SetIndex(randomIndex);
                    }
                    else
                    {
                        SetIndex(_currentIndex);
                    }
                    
                    currentPlatform = nextPlatform;
                    _isJumping = false;
                });

            return jumpSequence;
        }

        public void GetSupply(ISupplier supplier)
        {
            _material.SetFloat("_Index", _collectIndex);
            _supplyType = supplier.Type;
            _hasSupply = true;

            if (supplier.Type.Equals(SupplyType.Baloon))
            {
                _baloonIcon.SetActive(true);
                _oxygenIcon.SetActive(false);
            }
            
            if (supplier.Type.Equals(SupplyType.Oxygen))
            {
                _baloonIcon.SetActive(false);
                _oxygenIcon.SetActive(true);
            }
        }

        public bool TrySpendSupply(IReceiver receiver)
        {
            var canSpend = _hasSupply && receiver.SupplyType.Equals(_supplyType);

            if (canSpend)
            {
                receiver.Destroy();
                
                _baloonIcon.SetActive(false);
                _oxygenIcon.SetActive(false);

                _hasSupply = false;
            }

            return canSpend;
        }

        private void Update()
        {
            var lookRotation = Quaternion.LookRotation(Camera.main.transform.position);
            lookRotation.x = 0;
            lookRotation.z = 0;

            transform.rotation = lookRotation;
        }

        private void SetIndex(int index)
        {
            _material.SetFloat("_Index", index);
            _currentIndex = index;
        }
    }
}