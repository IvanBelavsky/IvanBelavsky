using System;
using UnityEngine;
using Zenject;

public class TranslateService : MonoBehaviour
{
    public Action<Language> OnLanguageChange;
    
    [field: SerializeField] public Language Language { get; private set; }

    private SaveService _saveService;
    
    [Inject]
    public void Constructor(SaveService saveService)
    {
        _saveService = saveService;
    }
    
    private void Start()
    {
        ChangeLanguage(_saveService.CurrentSaveData.Language);
    }

    public void ChangeLanguage(Language language)
    {
        Language = language;
        OnLanguageChange?.Invoke(language);
        _saveService.CurrentSaveData.UpdateLanguage(language);
        _saveService.Save();
    }
}

public enum Language
{
    ru = 0,
    eng = 1
}