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
        [SerializeField] private Color _HittedColor;

        private Coroutine _coroutine;
        private Renderer _renderer;
        private Rigidbody _rigitbody;
        private bool _hasHit;

        public event Action<FallingCube> LifeEnded;

        public int LifeTime { get; set; }

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _rigitbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _renderer.material.color = _defaultColor;
        }

        private void OnEnable()
        {
            _hasHit = false;
            ResetParameters();
            _renderer.material.color = _defaultColor;
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

            _hasHit = true;
            _renderer.material.color = _HittedColor;
            _coroutine = StartCoroutine(CountDownLifeTime());
        }

        public void Init(Color defaultColor, Color HittedColor)
        {
            _defaultColor = defaultColor;
            _HittedColor = HittedColor;
        }

        private void ResetParameters()
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            _rigitbody.velocity = Vector3.zero;
            _rigitbody.angularVelocity = Vector3.zero;
        }

        public void CloneTo(FallingCube clone)
        {
            clone.Init(_defaultColor, _HittedColor);
        }

        private IEnumerator CountDownLifeTime()
        {
            var time = new WaitForSecondsRealtime(LifeTime);
            yield return time;

            LifeEnded?.Invoke(this);
        }
    }
}

