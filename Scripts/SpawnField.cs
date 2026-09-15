using System.IO.Compression;
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

        public Vector3 GetRandomPosition()
        {
            Vector3 center = transform.position;

            float xSize = _spawnSize.x * 0.5f;
            float ySize = _spawnSize.y * 0.5f;
            float zSize = _spawnSize.z * 0.5f;

            return new Vector3(
                Random.Range(center.x - xSize, center.x + xSize),
                Random.Range(center.y - ySize, center.y + ySize),
                Random.Range(center.z - zSize, center.z + zSize));
        }
    }
}

