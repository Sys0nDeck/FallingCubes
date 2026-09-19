using UnityEngine;

namespace FallingCubes
{
    [RequireComponent(typeof(Rigidbody))]
    public class StateResetter : MonoBehaviour
    {
        private Rigidbody _rigitbody;

        private void Awake()
        {
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

        private void ResetParameters()
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            _rigitbody.velocity = Vector3.zero;
            _rigitbody.angularVelocity = Vector3.zero;
        }
    }
}


