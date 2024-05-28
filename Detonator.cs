using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _explosionForce;

    private void OnEnable()
    {
        _cubeSpawner.CubeCreated += Detonate;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeCreated -= Detonate;
    }

    private void Detonate()
    {
        Vector3 force = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value) * _explosionForce;
        _cubeSpawner.NewCube.GetComponent<Rigidbody>().AddForce(force);
    }
}