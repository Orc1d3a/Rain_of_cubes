using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public event Action<GameObject> TouchedPlatform;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private const string _platformTag = "Platform";
    private bool _hasTouchedPlatform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        SetDefaultSettings();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_platformTag))
        {
            if(_hasTouchedPlatform == false)
                _renderer.material.color = UnityEngine.Random.ColorHSV();
    
            _hasTouchedPlatform = true;

            TouchedPlatform?.Invoke(gameObject);
        }
    }

    private void OnDisable()
    {
        SetDefaultSettings();
    }

    private void SetDefaultSettings()
    {
        Vector3 position;

        float minPositionX = -5;
        float maxPositionX = 5;

        float minPositionZ = -5;
        float maxPositionZ = 5;

        float PositionX = UnityEngine.Random.Range(minPositionX, maxPositionX);
        float PositionY = 20;
        float PositionZ = UnityEngine.Random.Range(minPositionZ, maxPositionZ);

        Color color = Color.white;

        position = new Vector3(PositionX, PositionY, PositionZ);

        _hasTouchedPlatform = false;

        transform.position = position;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _renderer.material.color = color;
    }
}