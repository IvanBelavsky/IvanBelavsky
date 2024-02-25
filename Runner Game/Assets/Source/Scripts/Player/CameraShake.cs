using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraShake : MonoBehaviour, ISetupPlayer
{
    [SerializeField] private Player _player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _player.OnShakeCameraChange += Shake;
        _player.OnShakeCameraDisable += DisableShake;
    }

    private void OnDisable()
    {
        _player.OnShakeCameraChange -= Shake;
        _player.OnShakeCameraDisable -= DisableShake;
    }

    public void SetUpPlaer(Player player)
    {
        _player = player;
    }
    
    private void DisableShake()
    {
        _animator.SetBool("IsShake", false);
    }
    
    private void Shake()
    {
        _animator.SetBool("IsShake", true);
    }
}