using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => cube.gameObject.SetActive(true),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject)            
            );
    }

    public Cube GetCube()
    {
        return _pool.Get();
    }

    public void ReturnCube(Cube cube)
    {
        _pool.Release(cube);
    }
}
