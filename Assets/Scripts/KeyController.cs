using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class KeySetting
    {
        public KeyCode KeyCode;
        public int Index;
    }
    
    public class KeyController : MonoBehaviour
    {
        [SerializeField]
        private KeySetting[] _keySettings;

        private KeyCode _keyCode;
        private Material _material;

        private void Awake()
        {
            _material = GetComponentInChildren<MeshRenderer>().material;
        }

        public void SetIndex(int index)
        {
            var setting = _keySettings.First(item => item.Index == index);

            _keyCode = setting.KeyCode;
            _material.SetFloat("_Index", setting.Index);
        }

        public bool IsKeyDown()
        {
            return Input.GetKeyDown(_keyCode);
        }
    }
}