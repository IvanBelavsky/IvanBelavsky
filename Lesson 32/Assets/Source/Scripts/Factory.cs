using UnityEngine;

public class Factory : MonoBehaviour
{
    private Character _characterFirst;
    private Character _characterSecond;

    private void Awake()
    {
        _characterFirst = Resources.Load<Character>("CharactrerFirst");
        _characterSecond = Resources.Load<Character>("CharacterSecond");
    }

    public Character CreateCharacterFirst()
    {
        Character character = Instantiate(_characterFirst, transform.position, Quaternion.identity);
        return character;
    } 
    
    public Character CreateCharacterSecond()
    {
        Character character = Instantiate(_characterSecond, transform.position, Quaternion.identity);
        return character;
    }
}