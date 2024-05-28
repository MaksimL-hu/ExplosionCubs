using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _separationChance = 1f;

    public event UnityAction<Cube> Clicked;

    public float SeparationChance => _separationChance;

    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject);
    }

    public void SetSeparationChance(float newSeparationChance)
    {
        _separationChance = newSeparationChance;
    }
}