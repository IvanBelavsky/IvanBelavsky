using UnityEngine;

public class ColorTarget : MonoBehaviour, IColorable
{
    [SerializeField] private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            SetColor();
            Destroy(collision.gameObject);
        }
    }

    public void SetColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
