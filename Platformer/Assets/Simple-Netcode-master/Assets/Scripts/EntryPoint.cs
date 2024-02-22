#nullable enable
using System;
using UI;
using Unity.Netcode;
using UnityEngine;


public class EntryPoint : MonoBehaviour
{
    [SerializeField] private NetworkButtons networkButtons = null!;
    [SerializeField] private NetworkManager network = null!;


    private void Start()
    {
        if (network == null)
        {
            throw new NullReferenceException(nameof(NetworkManager));
        }

        if (networkButtons == null)
        {
            throw new NullReferenceException(nameof(NetworkButtons));
        }

        networkButtons.request.AddListener(mode =>
        {
            switch (mode)
            {
                case NetworkButton.Mode.Host:
                    network.StartHost();
                    break;
                case NetworkButton.Mode.Client:
                    network.StartClient();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            networkButtons.Hide();
        });
    }
}