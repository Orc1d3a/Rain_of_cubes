using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;

    private Coroutine _coroutineSpawn;

    private void Start()
    {
        StartSpawningCubes();
    }

    private void StartSpawningCubes()
    {
        _coroutineSpawn = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait;
        Cube cube;

        Vector3 position;

        float minPositionX = -5;
        float maxPositionX = 5;

        float minPositionZ = -5;
        float maxPositionZ = 5;

        float positionX;
        float positionY = 20;
        float positionZ;

        int period = 3;
        bool shoudSpawn = true;

        wait = new WaitForSeconds(period);

        while (shoudSpawn)
        {
            positionX = Random.Range(minPositionX, maxPositionX);
            positionZ = Random.Range(minPositionZ, maxPositionZ);

            cube = _pool.GetCube();

            position = new Vector3(positionX, positionY, positionZ);
            cube.transform.position = position;

            cube.Died += DestroyCube;

            yield return wait;
        }
    }

    private void DestroyCube(Cube cube)
    {
        cube.Died -= DestroyCube;

        _pool.ReturnCube(cube);
    }
}
