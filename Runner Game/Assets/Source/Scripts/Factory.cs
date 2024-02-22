using UnityEngine;

public class Factory : MonoBehaviour
{
    private Target _target;

    private void Awake()
    {
        _target = Resources.Load<Target>("Circle");
    }

    public Target CreateTarget(Vector2 position)
    {
         return Instantiate(_target, position, Quaternion.identity);
    }
}