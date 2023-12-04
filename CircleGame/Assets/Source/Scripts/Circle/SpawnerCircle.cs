using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FactoryCircle))]
public class SpawnerCircle : MonoBehaviour
{
    [SerializeField] private CounterUI _countUI;
    [SerializeField] private int _currentCount, _goalCount;
    [SerializeField] private float _delay;
    [SerializeField] private TextMeshProUGUI _text;
    [Space(10)]
    [Header("Gizmos")]
    [SerializeField] private float _radius;
    private Coroutine _spawnCircle;
    private Coroutine _pauseScene;
    private FactoryCircle _factory;

    private void Awake()
    {
        _factory = GetComponent<FactoryCircle>();
        _countUI.OnCountChange += CounterHandler;
    }

    private void Start()
    {
        _spawnCircle = StartCoroutine(SpawnCircle());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _radius);
    }

    private void RemoveCircle()
    {
        _currentCount--;
    }

    private void OnClickBadCircle()
    {
        _text.text = "GAME OVER";
        _pauseScene = StartCoroutine(PauseScene());
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CounterHandler(int count)
    {
        if ((count % 10) == 1)
            _delay -= 0.1f;
    }

    private IEnumerator PauseScene()
    {
        yield return new WaitForSeconds(2);
        RestartScene();
    }

    private IEnumerator SpawnCircle()
    {
        while (true)
        {
            yield return null;
            for (int i = _currentCount; i < _goalCount; i++)
            {
                yield return new WaitForSeconds(_delay);
                Circle circleCreated = null;
                _currentCount++;
                Vector3 position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
                float chance = Random.Range(0, 100);
                if (chance >= 30 && chance < 70)
                {
                    circleCreated = _factory.CreatedMoveCircle(position).SetCount(_countUI);
                    circleCreated.OnDestroy += RemoveCircle;
                }
                if (chance < 30)
                {
                    circleCreated = _factory.CreatedCircle(position).SetCount(_countUI);
                    circleCreated.OnDestroy += RemoveCircle;
                }
                if (chance >= 70 && chance < 85)
                {
                    circleCreated = _factory.CreatedBadCircle(position).SetCount(_countUI);
                    circleCreated.OnClick += OnClickBadCircle;
                    _currentCount--;
                }
                if (chance >= 85 && chance < 95)
                {
                    circleCreated = _factory.CreatedRemoveCircle(position).SetCount(_countUI);
                }
                if (chance > 95)
                {
                    circleCreated = _factory.CreatedGoldCircle(position).SetCount(_countUI);
                }
                if (chance >= 85 && chance < 95)
                {
                    circleCreated = _factory.CreatedColorCircle(position).SetCount(_countUI);
                }
                if (_countUI.Cpounter >= 50)
                {
                    _text.text = "YOU VINNER";
                    _pauseScene = StartCoroutine(PauseScene());
                }
            }
        }
    }
}
