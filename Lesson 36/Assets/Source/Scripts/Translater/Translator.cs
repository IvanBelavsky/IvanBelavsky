using System;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public Action<Language> OnLanguageChange;

    [field: SerializeField] public Language Language { get; private set; }

    private void Start()
    {
        ChangeLanguage(Language);
    }

    public void ChangeLanguage(Language language)
    {
        Language = language;
        OnLanguageChange?.Invoke(language);
    }
}