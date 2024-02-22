#nullable enable
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class NetworkButton : MonoBehaviour
{
    public UnityEvent<Mode> request = new();
    [SerializeField] private Mode mode;
    private Button button = null!;


    private void Awake()
    {
        button = GetComponent<Button>()!;
        button.onClick!.AddListener(() => request?.Invoke(mode));
    }


    public enum Mode
    {
        Host,
        Client
    }
}