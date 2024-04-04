using TMPro;
using UnityEngine;

public enum Language
{
    ru = 0,
    eng = 1
}

public class TextTranslate : MonoBehaviour
{
    [SerializeField] private Language _language;
    [SerializeField] private Translator _translator;
    [SerializeField] private string _id;

    [SerializeField] private TextMeshProUGUI _text;
    private string _result;

    private void Start()
    {
        _language = _translator.Language;
        _translator.OnLanguageChange += language =>
        {
            _language = language;
            Translate();
        };
        if (!_text)
            _text = GetComponent<TextMeshProUGUI>();
        if(_id == "")
            return;
        if(_text == null)
            return;
        Translate();
    }

    public void Setup(Translator translator)
    {
        _translator = translator;
    }
    
    public string GetTranslateById(string id)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("DataList");
        string[] data = textAsset.text.Split(new char[] { '\n' });
        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ';' });
            if (row[0] != "")
            {
                if (row[0] == _id)
                {
                    _result = row[(int)_language + 1];
                }
            }
        }

        if (_result == "")
            Debug.LogError($"Id or word not found: id: {_id}");
        return _result;
    }

    private void Translate()
    {
        if(_text ==null)
            return;
        TextAsset textAsset = Resources.Load<TextAsset>("DataList");
        string[] data = textAsset.text.Split(new char[] { '\n' });
        for (int i = 0; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ';' });
            if (row[0] != "")
            {
                if (row[0] == _id)
                {
                    _result = row[(int)_language + 1];
                }
            }
        }

        if (_result == "")
            Debug.LogError($"Id or word not found: id: {_id}");
        _text.text = _result;
    }
}
