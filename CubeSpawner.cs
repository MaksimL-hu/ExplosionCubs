using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private int _minCountCreatedCubes = 2;
    [SerializeField] private int _maxCountCreatedCubes = 6;
    [SerializeField] private int _decreaseScale = 2;
    [SerializeField] private int _decreaseSeparationChance = 2;
    [SerializeField] private List<Cube> _cubes;

    public event Action<List<Cube>> CubesCreated;

    private void OnEnable()
    {
        foreach (Cube cub in _cubes)
            cub.Clicked += SpawnCubs;
    }

    private void SpawnCubs(Cube example)
    {
        List<Cube> createdCubes = new List<Cube>();

        if (example.SeparationChance > UnityEngine.Random.value)
        {
            int count = UnityEngine.Random.Range(_minCountCreatedCubes, _maxCountCreatedCubes + 1);

            for (int i = 0; i < count; i++)
            {
                createdCubes.Add(InstantiateCub(example));
            }

            CubesCreated?.Invoke(createdCubes);
        }

        example.Clicked -= SpawnCubs;

        _cubes.Remove(example);
        _cubes = _cubes.Union(createdCubes).ToList();
    }

    private Cube InstantiateCub(Cube example)
    {
        Cube newCube = Instantiate(example);
        newCube.transform.localScale /= _decreaseScale;
        newCube.SerColor(GetRandomColor());
        newCube.SetSeparationChance(example.SeparationChance / _decreaseSeparationChance);
        newCube.Clicked += SpawnCubs;

        return newCube;
    }

    private Color GetRandomColor()
    {
        return UnityEngine.Random.ColorHSV();
    }
}