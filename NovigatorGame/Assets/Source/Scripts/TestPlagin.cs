using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlagin : MonoBehaviour
{
    [SerializeField] private float _money;

    private void Awake()
    {
        Load();
    }

    [Button]
    private void AddMoney()
    {
        _money++;
        PlayerPrefs.SetFloat("Mon", _money);
    }

    private void Load()
    {
        _money = PlayerPrefs.GetFloat("Mon");
    }
    
    [Button]
    private void NextScene()
    {
        SceneManager.LoadScene("Shop");
    }
}