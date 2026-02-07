using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    private Cube _cube;
    
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _cube = _cubePrefab.GetComponent<Cube>();

        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
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
