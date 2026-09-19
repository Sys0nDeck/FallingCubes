using System;
using UnityEngine;

namespace FallingCubes
{
    [RequireComponent(typeof(PlatformDetector))]
    [RequireComponent(typeof(Timer))]
    public class FallingCube : MonoBehaviour
    {
        [SerializeField] private PlatformDetector _detector;
        [SerializeField] private Timer _timer;

        private float _lifeTime;

        public event Action<FallingCube> LifeEnded;

        private void Awake()
        {
            _detector = GetComponent<PlatformDetector>();
            _timer = GetComponent<Timer>();
        }

        private void OnEnable()
        {
            _detector.Hitted += CubeHitted;
            _timer.Ended += EndLife;
        }

        private void OnDisable()
        {
            _detector.Hitted -= CubeHitted;
            _timer.Ended -= EndLife;
        }

        public void Init(float second)
        {
            _lifeTime = second;
        }

        private void CubeHitted()
        {
            _timer.StartTime(_lifeTime);
        }

        private void EndLife()
        {
            LifeEnded?.Invoke(this);
        }
    }
}

