using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Factory))]
public class Fight : MonoBehaviour
{
    private Factory _factory;
    private Coroutine _damageTick;
    public Character _firstCharacter { get; private set; }
    public Character _secondCharacter { get; private set; }
    public bool _isDie { get; private set; }

    private void Awake()
    {
        _factory = GetComponent<Factory>();
    }

    private void Start()
    {
        _firstCharacter = _factory.CreateCharacterFirst();
        _secondCharacter = _factory.CreateCharacterSecond();
        _damageTick = StartCoroutine(DamageTick());
    }

    private void Update()
    {
        if (_secondCharacter == null)
        {
            _secondCharacter = _factory.CreateCharacterSecond();
            _firstCharacter.Improve();
            _isDie = true;
        }

        if (_firstCharacter == null)
        {
            _firstCharacter = _factory.CreateCharacterFirst();
            _secondCharacter.Improve();
            _isDie = true;
        }
    }

    private IEnumerator DamageTick()
    {
        while (_firstCharacter && _secondCharacter)
        {
            yield return new WaitForSeconds(2);
            _firstCharacter.TakeDamage(_secondCharacter.Damage);
            yield return new WaitForSeconds(2);
            _secondCharacter.TakeDamage(_firstCharacter.Damage);
        }
    }
}