using System.Collections.Generic;
using UnityEngine;

public class Cub : MonoBehaviour
{
    [SerializeField] private int _minCountCreatedCubes = 2;
    [SerializeField] private int _maxCountCreatedCubes = 6;
    [SerializeField] private int _decreaseScale = 2;
    [SerializeField] private int _decreaseSeparationChance = 2;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _separationChance = 1f;
    [SerializeField] private Cub _prefab;

    private System.Random _random;

    private void Start()
    {
        _random = new System.Random();
    }

    private void OnMouseUpAsButton()
    {
        CreateCubs();
    }

    private void CreateCubs()
    {
        if (_separationChance > (float)_random.NextDouble())
        {
            int count = _random.Next(_minCountCreatedCubes, _maxCountCreatedCubes + 1);
            List<Rigidbody> explosionObjects = new List<Rigidbody>();

            for (int i = 0; i < count; i++)
                explosionObjects.Add(InstantiateCub().GetComponent<Rigidbody>());

            foreach (Rigidbody explosionObject in explosionObjects)
                explosionObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Destroy(gameObject);
    }

    private Cub InstantiateCub()
    {
        Cub cub = Instantiate(_prefab);
        cub.transform.localScale /= _decreaseScale;
        cub.GetComponent<Renderer>().material.color = GetRandomColor();
        cub._separationChance /= _decreaseSeparationChance;

        return cub;
    }

    private Color GetRandomColor()
    {
        float randomChannelOne = (float)_random.NextDouble();
        float randomChannelTwo = (float)_random.NextDouble();
        float randomChannelThree = (float)_random.NextDouble();

        return new Color(randomChannelOne, randomChannelTwo, randomChannelThree);
    }
}