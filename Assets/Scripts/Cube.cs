using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField]private ColorChanger _colorChanger;

    public event Action<Cube> Died;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Coroutine _coroutineDie;
    
    private bool _hasTouchedPlatform;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        SetDefaultSettings();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false)
        {
            if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            {
                _colorChanger.ChangeColor(_renderer);
                _hasTouchedPlatform = true;

                _coroutineDie = StartCoroutine(Die());
            }
        }
    }

    private void OnDisable()
    {
        SetDefaultSettings();
    }

    private void SetDefaultSettings()
    {
        Color color = Color.white;

        _hasTouchedPlatform = false;

        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _renderer.material.color = color;
    }

    private IEnumerator Die()
    {
        WaitForSeconds wait;

        float minDelay = 2;
        float maxDelay = 5;
        float delay = UnityEngine.Random.Range(minDelay, maxDelay);

        wait = new WaitForSeconds(delay);

        yield return wait;

        Died?.Invoke(this);
    }
}