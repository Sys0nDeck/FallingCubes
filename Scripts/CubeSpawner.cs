using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace FallingCubes
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private SpawnField _spawnField;
        [SerializeField] private float _spawnDelay = 1f;
        [SerializeField] private int _poolCapacity = 5;
        [SerializeField] private int _poolMaxSize = 10;
        [SerializeField] private int _minCubeLifeTime = 2;
        [SerializeField] private int _maxCubeLifeTime = 5;

        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
        }

        private void Start()
        {
            StartCoroutine(nameof(Spawn));
        }

        private void ActionOnGet(GameObject obj)
        {
            obj.transform.position = GetRandomPositionOnField(_spawnField);
            obj.SetActive(true);
            var cube = obj.GetComponent<FallingCube>();
            cube.Init(Random.Range(_minCubeLifeTime, _maxCubeLifeTime + 1));
            cube.OnLifeEnded += ReleaseCube;
        }

        private void ReleaseCube(FallingCube cube)
        {
            cube.OnLifeEnded -= ReleaseCube;
            _pool.Release(cube.gameObject);
        }

        private IEnumerator Spawn()
        {
            var delay = new WaitForSecondsRealtime(_spawnDelay);

            while (true)
            {
                _pool.Get();
                yield return delay;
            }   
        }

        private Vector3 GetRandomPositionOnField(SpawnField field)
        {
            Vector3 spawnCenter = field.transform.position;
            Vector3 spawnScale = field.Scale;

            var spawnZoneX = GetRangeZone(spawnCenter.x, spawnScale.x);
            var spawnZoneY = GetRangeZone(spawnCenter.y, spawnScale.y);
            var spawnZoneZ = GetRangeZone(spawnCenter.z, spawnScale.z);

            float randomX = Random.Range(spawnZoneX.Item1, spawnZoneX.Item2);
            float randomY = Random.Range(spawnZoneY.Item1, spawnZoneY.Item2);
            float randomZ = Random.Range(spawnZoneZ.Item1, spawnZoneZ.Item2);

            return new Vector3(randomX, randomY, randomZ);
        }

        private (float,float) GetRangeZone(float center, float scale)
        {
            float start = center - (scale * 0.5f);
            float end = center + (scale * 0.5f);

            return (start, end);
        }
    }
}

