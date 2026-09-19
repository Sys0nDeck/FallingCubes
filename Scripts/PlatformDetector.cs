using System;
using UnityEngine;

namespace FallingCubes
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlatformDetector : MonoBehaviour
    {
        private bool _hasHit = false;

        public event Action Hitted;

        public void OnEnable()
        {
            _hasHit = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hasHit)
                return;

            if (collision.gameObject.TryGetComponent<Platform>(out var component))
            {
                _hasHit = true;
                Hitted?.Invoke();
            } 
        }
    }
}

