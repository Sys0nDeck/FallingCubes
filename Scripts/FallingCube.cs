using System;
using System.Collections;
using UnityEngine;

namespace FallingCubes
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class FallingCube : MonoBehaviour
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _hittedColor;

        private Coroutine _coroutine;
        private Renderer _renderer;
        private Rigidbody _rigitbody;
        private bool _hasHit;
        private int _lifeTime;

        public event Action<FallingCube> LifeEnded;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _rigitbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            ResetParameters();
        }

        private void OnEnable()
        {
            ResetParameters();
        }

        private void OnDisable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);            
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hasHit == true)
                return;

            if (collision.gameObject.TryGetComponent<FallingCube>(out var component))
                return;

            _hasHit = true;
            _renderer.material.color = _hittedColor;
            _coroutine = StartCoroutine(CountDownLifeTime());
        }

        public void Init(int lifeTime)
        {
            _lifeTime = lifeTime;
        }

        private void ResetParameters()
        {
            _hasHit = false;
            _renderer.material.color = _defaultColor;
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            _rigitbody.velocity = Vector3.zero;
            _rigitbody.angularVelocity = Vector3.zero;
        }

        private IEnumerator CountDownLifeTime()
        {
            var time = new WaitForSecondsRealtime(_lifeTime);
            yield return time;

            LifeEnded?.Invoke(this);
        }
    }
}

