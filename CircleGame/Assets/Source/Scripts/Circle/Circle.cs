using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Circle : MonoBehaviour
{
    public Action OnClick;
    public Action OnDestroy;

    [SerializeField] private float _lifeTime;
    private CounterUI _counterUI;
    private Vector3 _startPosition;
    private MeshRenderer _renderer;
    private Coroutine _lifeTimeTick;
    private int _score = 1;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _startPosition = transform.position;
    }

    private void Start()
    {
        _lifeTimeTick = StartCoroutine(LifeTimeTick());
    }

    public Circle SetCount(CounterUI count)
    {
        _counterUI = count;
        return this;
    }

    public Circle SetMoveTape()
    {
        transform.DOMove(_startPosition + new Vector3(1, 0, 0), 1).OnComplete(() =>
        {
            transform.DOMove(_startPosition + new Vector3(-1, 0, 0), 1);
        }).SetLoops(-1, LoopType.Yoyo);
        return this;
    }

    public Circle SetColor(Color color)
    {
        _renderer.material.color = color;
        return this;
    }
    public Circle SetRandomColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
        return this;
    }

    public Circle SetLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
        return this;
    }

    public Circle SetScore(int score)
    {
        _score = score;
        return this;
    }

    private void Kill()
    {
        if (_lifeTimeTick != null)
        {
            StopCoroutine(_lifeTimeTick);
        }
        OnDestroy?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator LifeTimeTick()
    {
        yield return new WaitForSeconds(_lifeTime);
        Kill();
    }

    private void OnMouseDown()
    {
        _counterUI.AddCount(_score);
        OnClick?.Invoke();
        Kill();
    }
}
