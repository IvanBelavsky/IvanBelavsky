using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigHouse : MonoBehaviour
{
    public Action<float> OnCoinChange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            ReloadScene();
        }
    }

    public void AddCoin(int value)
    {
        OnCoinChange?.Invoke(value);
    }
    
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
