#nullable enable
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;


namespace Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(NetworkTransform))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float jumpPower = 20;
        private new Rigidbody2D rigidbody2D = null!;
        private IInput input = new PlugInput();
        [SerializeField] private NetworkVariable<float> inputDirection = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );
        [SerializeField] private NetworkVariable<bool> inputJump = new(
            false,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner
        );
        private readonly Quaternion lookAdditional = Quaternion.Euler(0, -90, 0);


        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>()!;
        }


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                input = new KeyboardInput();
            }
        }


        private void Update()
        {
            if (IsOwner)
            {
                inputDirection.Value = input.Direction();
                inputJump.Value = input.Jump();
            }

            if (IsServer)
            {
                var direction = Vector2.right * inputDirection.Value;
                if (direction.sqrMagnitude > 0)
                {
                    rigidbody2D.AddForce(direction * (speed * Time.deltaTime));
                    transform.rotation = Quaternion.LookRotation(direction) * lookAdditional;
                }

                if (inputJump.Value && rigidbody2D.IsTouchingLayers())
                {
                    inputJump.Value = false;
                    rigidbody2D.AddForce(Vector2.up * jumpPower);
                }
            }
        }
    }
}