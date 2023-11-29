using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TargetPig : MonoBehaviour, ITargetable
{
    [SerializeField] private float _maxVelossity;
    [SerializeField] private Target _target;

    public void TargetChange()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bird>() && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= _maxVelossity)
        {
            TargetChange();
        }
        if (collision.gameObject.GetComponent<Target>())
        {
            TargetChange();
        }
    }

}
