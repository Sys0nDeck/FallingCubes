using System;
using System.Collections;
using UnityEngine;

namespace FallingCubes
{
    public class Timer : MonoBehaviour
    {
        private Coroutine _coroutine;

        public event Action Ended;

        private void OnDisable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void StartTime(float seconds)
        {
            _coroutine = StartCoroutine(DoCountdown(seconds));
        }

        private IEnumerator DoCountdown(float seconds)
        {
            var time = new WaitForSecondsRealtime(seconds);
            yield return time;

            Ended?.Invoke();
        }
    }
}

