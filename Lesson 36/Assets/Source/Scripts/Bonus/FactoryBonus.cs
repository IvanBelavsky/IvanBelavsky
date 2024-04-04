using UnityEngine;

public class FactoryBonus : MonoBehaviour
{
    private Bonus _bonus;
    private ButtonsUI _buttonsUI;

    private void Awake()
    {
        _bonus = Resources.Load<Bonus>("Bonus/Bonus");
    }

    public void Setup(ButtonsUI buttonsUI)
    {
        _buttonsUI = buttonsUI;
    }

    public void CreateBonus(Vector2 position)
    {
        Bonus bonus = Instantiate(_bonus, position, Quaternion.identity);
        bonus.Setup(_buttonsUI);
    }
}