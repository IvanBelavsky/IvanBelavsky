using UnityEngine;

public class TouchFingers : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            _player.MoveToPosition(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
        }
    }
}
