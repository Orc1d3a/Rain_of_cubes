using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    
    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => cube.SetActive(true),
            actionOnRelease: (cube) => cube.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube)            
            );  
    }

    public GameObject GetCube => _pool.Get();

    public void ReturnCube(GameObject cube)
    {
        _pool.Release(cube);
    }
}
