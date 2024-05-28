using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private int _minCountCreatedCubes = 2;
    [SerializeField] private int _maxCountCreatedCubes = 6;
    [SerializeField] private int _decreaseScale = 2;
    [SerializeField] private int _decreaseSeparationChance = 2;
    [SerializeField] private List<Cube> _cubes;

    public Cube NewCube { get; private set; }

    public event UnityAction CubeCreated;

    private void OnEnable()
    {
        foreach (Cube cub in _cubes)
            cub.Clicked += SpawnCubs;
    }

    private void SpawnCubs(Cube example)
    {
        if (example.SeparationChance > Random.value)
        {
            int count = Random.Range(_minCountCreatedCubes, _maxCountCreatedCubes + 1);

            for (int i = 0; i < count; i++)
                InstantiateCub(example);
        }

        example.Clicked -= SpawnCubs;
        _cubes.Remove(example);
    }

    private void InstantiateCub(Cube example)
    {
        NewCube = Instantiate(example);
        NewCube.transform.localScale /= _decreaseScale;
        NewCube.GetComponent<Renderer>().material.color = GetRandomColor();
        NewCube.SetSeparationChance(example.SeparationChance / _decreaseSeparationChance);
        NewCube.Clicked += SpawnCubs;
        _cubes.Add(NewCube);
        CubeCreated?.Invoke();
    }

    private Color GetRandomColor()
    {
        float randomChannelOne = Random.value;
        float randomChannelTwo = Random.value;
        float randomChannelThree = Random.value;

        return new Color(randomChannelOne, randomChannelTwo, randomChannelThree);
    }
}