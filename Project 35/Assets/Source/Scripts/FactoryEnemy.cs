using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    private Enemy _stone;
    private Enemy _cactus;

    private void Awake()
    {
        _stone = Resources.Load<Stone>("Stone");
        _cactus = Resources.Load<Cactus>("Cactus");
    }

    public Enemy CreateStone(Vector2 position)
    {
        return Instantiate(_stone, position, Quaternion.identity);
    }

    public Enemy CreateCactus(Vector2 position)
    {
        return Instantiate(_cactus, position, Quaternion.identity);
    }
}