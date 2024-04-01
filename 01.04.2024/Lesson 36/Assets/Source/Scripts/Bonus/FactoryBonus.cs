using UnityEngine;

public class FactoryBonus : MonoBehaviour
{
    private Bonus _bonus;

    private void Awake()
    {
        _bonus = Resources.Load<Bonus>("Bonus/Bonus");
    }

    public void CreateBonus(Vector2 position)
    {
        Instantiate(_bonus, position, Quaternion.identity);
    }
}
