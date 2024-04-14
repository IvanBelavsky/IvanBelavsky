using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MenuTranslater : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private TranslateService _translator;
    private SaveService _saveService;
    
    [Inject]
    public void Constructor(TranslateService translator, SaveService saveService)
    {
        _translator = translator;
        _saveService = saveService;
    }

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropDownChange);

        switch (_saveService.CurrentSaveData.Language)
        {
            case Language.ru:
                _dropdown.value = 0;
                break;
            case Language.eng:
                _dropdown.value = 1;
                break;
        }
    }

    public void OnDropDownChange(int id)
    {
        _translator.ChangeLanguage((Language)Enum.Parse(typeof(Language),id.ToString()));
    }
}
