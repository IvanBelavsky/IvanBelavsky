using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public Typies Type { get; private set; }
    
    public void RandomType()
    {
        var type = System.Enum.GetValues(typeof(Typies));
        int randomEnum = UnityEngine.Random.Range(0, type.Length);
        Type = (Typies)randomEnum;
    }
}