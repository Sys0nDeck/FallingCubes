using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace FallingCubes
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private FallingCube _prefab;
        [SerializeField] private SpawnField _spawnField;
        [SerializeField] private float _spawnDelay = 1f;
        [SerializeField] private int _poolCapacity = 5;
        [SerializeField] private int _poolMaxSize = 10;
        [SerializeField] private int _minCubeLifeTime = 2;
        [SerializeField] private int _maxCubeLifeTime = 5;

        private ObjectPool<FallingCube> _pool;
        private Coroutine _coroutine;

        private void Awake()
        {
            _pool = new ObjectPool<FallingCube>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: (cube) => ActionOnGet(cube),
                actionOnRelease: (cube) => ActionOnRelease(cube),
                actionOnDestroy: (cube) => ActionOnDestroy(cube),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
        }

        private void Start()
        {
            _coroutine = StartCoroutine(Spawn());
        }

        private void ActionOnGet(FallingCube cube)
        {
            var obj = cube.gameObject;
            obj.SetActive(true);
            obj.transform.position = _spawnField.GetRandomPosition();
            cube.Init(Random.Range(_minCubeLifeTime, _maxCubeLifeTime + 1));
            cube.LifeEnded += ReleaseCube;
        }

        private void ActionOnRelease(FallingCube cube)
        {
            cube.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(FallingCube cube)
        {
            Destroy(cube.gameObject);
        }

        private void ReleaseCube(FallingCube cube)
        {
            cube.LifeEnded -= ReleaseCube;
            _pool.Release(cube);
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
    }
}

