using UnityEngine;
using Zenject;

public class FactoryBonus : MonoBehaviour
{
    private Bonus _bonus;
    private DiContainer _diContainer;

    [Inject]
    public void Constructor(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    
    private void Awake()
    {
        _bonus = Resources.Load<Bonus>("Bonus/Bonus");
    }

    public Bonus CreateBonus(Vector2 position)
    {
       return _diContainer.InstantiatePrefab(_bonus, position, Quaternion.identity, null).GetComponent<Bonus>();
    }
}