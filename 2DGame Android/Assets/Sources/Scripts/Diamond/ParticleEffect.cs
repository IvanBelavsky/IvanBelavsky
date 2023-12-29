using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private Diamond _diamond;

    private void Awake()
    {
        _particleSystem.Stop();
        _diamond.OnEffects += PlayParticles;
    }
    private void PlayParticles()
    {
        Vector3 scorePosition = _textMeshPro.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(scorePosition);
        _particleSystem.Play();
    }
}
