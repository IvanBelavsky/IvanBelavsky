using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private int _lifeTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
