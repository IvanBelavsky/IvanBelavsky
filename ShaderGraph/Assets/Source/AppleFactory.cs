using UnityEngine;

public class AppleFactory : MonoBehaviour
{
    [SerializeField] private Apple _apple;

    private void Awake()
    {
        _apple = Resources.Load<Apple>("Apple");
    }
    public Apple CreatedApple(Vector3 position)
    {
        return Instantiate(_apple, position, Quaternion.identity).GetComponent<Apple>();
    }
}
