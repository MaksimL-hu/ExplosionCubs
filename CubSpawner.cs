using System.Collections.Generic;
using UnityEngine;

public class CubSpawner : MonoBehaviour
{
    [SerializeField] private int _minCountCreatedCubes = 2;
    [SerializeField] private int _maxCountCreatedCubes = 6;
    [SerializeField] private int _decreaseScale = 2;
    [SerializeField] private int _decreaseSeparationChance = 2;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private List<Cub> _cubs;

    private System.Random _random;

    private void Start()
    {
        _random = new System.Random();
    }

    private void OnEnable()
    {
        foreach (Cub cub in _cubs)
            cub.Clicked += SpawnCubs;
    }

    private void SpawnCubs(Cub cub)
    {
        if (cub.SeparationChance > (float)_random.NextDouble())
        {
            int count = _random.Next(_minCountCreatedCubes, _maxCountCreatedCubes + 1);
            List<Rigidbody> explosionObjects = new List<Rigidbody>();

            for (int i = 0; i < count; i++)
                explosionObjects.Add(InstantiateCub(cub).GetComponent<Rigidbody>());

            foreach (Rigidbody explosionObject in explosionObjects)
                explosionObject.AddExplosionForce(_explosionForce, cub.transform.position, _explosionRadius);
        }

        cub.Clicked -= SpawnCubs;
        _cubs.Remove(cub);
    }

    private Cub InstantiateCub(Cub cub)
    {
        Cub newCub = Instantiate(cub);
        newCub.transform.localScale /= _decreaseScale;
        newCub.GetComponent<Renderer>().material.color = GetRandomColor();
        newCub.SetSeparationChance(cub.SeparationChance / _decreaseSeparationChance);
        newCub.Clicked += SpawnCubs;
        _cubs.Add(newCub);

        return newCub;
    }

    private Color GetRandomColor()
    {
        float randomChannelOne = (float)_random.NextDouble();
        float randomChannelTwo = (float)_random.NextDouble();
        float randomChannelThree = (float)_random.NextDouble();

        return new Color(randomChannelOne, randomChannelTwo, randomChannelThree);
    }
}