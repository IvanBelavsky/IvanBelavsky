using UnityEngine;

public class BonusFactory : MonoBehaviour
{
    private Bonus _bonus;

    private void Awake()
    {
        _bonus = Resources.Load<Bonus>("Bonus");
    }

    public void CreateBonus(Vector2 position)
    {
        Instantiate(_bonus, position, Quaternion.identity);
    }
}