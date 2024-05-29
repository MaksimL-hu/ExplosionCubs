using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _explosionForce;

    private void OnEnable()
    {
        _cubeSpawner.CubesCreated += Detonate;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubesCreated -= Detonate;
    }

    private void Detonate(List<Cube> cubes)
    {
        Vector3 force;

        foreach (Cube cube in cubes)
        {
            force = new Vector3(Random.value, Random.value, Random.value) * _explosionForce;
            cube.AddForse(force);
        }
    }
}