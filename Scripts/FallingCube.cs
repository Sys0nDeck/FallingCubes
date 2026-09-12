using System;
using System.Collections;
using UnityEngine;

namespace FallingCubes
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class FallingCube : MonoBehaviour
    {
        [SerializeField] private int _lifeTime;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _HittedColor;

        private Coroutine _coroutine;

        public event Action<FallingCube> OnLifeEnded;

        public bool HasHit { get; private set; }

        private void OnEnable()
        {
            HasHit = false;
            gameObject.GetComponent<Renderer>().material.color = _defaultColor;
        }

        private void OnDisable()
        {
            StopCoroutine(_coroutine);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (HasHit == true)
                return;

            HasHit = true;
            gameObject.GetComponent<Renderer>().material.color = _HittedColor;
            _coroutine = StartCoroutine(nameof(CountingDownLifeTime));
        }

        public void Init(int lifeTime)
        {
            _lifeTime = lifeTime;
        }

        private IEnumerator CountingDownLifeTime()
        {
            var time = new WaitForSecondsRealtime(_lifeTime);
            yield return time;

            OnLifeEnded?.Invoke(this);
        }
    }
}

