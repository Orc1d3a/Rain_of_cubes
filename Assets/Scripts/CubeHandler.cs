using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private Pool _pool;

    private Coroutine _coroutineSpawner;

    private void Start()
    {
        StartSpawningCubes();
    }

    private void StartSpawningCubes()
    {
        _coroutineSpawner = StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSecondsRealtime wait;
        GameObject cubeObject;
        Cube cube;

        int period = 3;
        bool shoudSpawn = true;

        wait = new WaitForSecondsRealtime(period);

        while (shoudSpawn)
        {
            cubeObject = _pool.GetCube;
            cube = cubeObject.GetComponent<Cube>();
            cube.TouchedPlatform += DeleteCubeWithDelay;

            yield return wait;
        }
    }

    private void DeleteCubeWithDelay(GameObject cubeObject)
    {
        Coroutine coroutineDeleter = StartCoroutine(DeleteCube(cubeObject));

    }

    private IEnumerator DeleteCube(GameObject cubeObject)
    {
        WaitForSecondsRealtime wait;
        Cube cube = cubeObject.GetComponent<Cube>();

        int minDelay = 2;
        int maxDelay = 5;
        int delay = Random.Range(minDelay, maxDelay);

        wait = new WaitForSecondsRealtime(delay);
        cube.TouchedPlatform -= DeleteCubeWithDelay;

        yield return wait;

        _pool.ReturnCube(cubeObject);
    }
}
