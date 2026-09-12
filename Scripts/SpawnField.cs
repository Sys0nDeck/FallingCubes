using UnityEngine;

namespace FallingCubes
{
    public class SpawnField : MonoBehaviour
    {
        [SerializeField] private Vector3 _spawnSize;

        public Vector3 Scale
            => _spawnSize;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, _spawnSize);
        }
    }
}

