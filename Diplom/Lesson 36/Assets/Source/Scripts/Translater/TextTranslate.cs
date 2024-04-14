using TMPro;
using UnityEngine;
using Zenject;

public class TextTranslate : MonoBehaviour
{
    [SerializeField] private Language _language = Language.eng;
    [Space(15), SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private string _id;
    private string _ids;
    private string _result;
    private TranslateService _translator;

    [Inject]
    public void Constructor(TranslateService translator)
    {
        _translator = translator;
    }


    public void Awake()
    {
        _language = _translator.Language;
        _translator.OnLanguageChange += language =>
        {
            _language = language;
            Translate();
        };
        if (!_textMesh)
            _textMesh = GetComponent<TextMeshProUGUI>();
        if (_id == "")
            return;
        if (_textMesh == null)
            return;
        Translate();
    }

    private void Translate()
    {
        if (_textMesh == null)
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
                    _result = _language == Language.ru ? row[1] + "   " : row[2] + " ";
                }
            }
        }

        if (_result == "")
            Debug.LogError($"Id or word not found: id: {_id}");
        _textMesh.text = _result;
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
                    _result = _language == Language.ru ? row[1] + " " : row[2] + '\t';
                }
            }
        }

        if (_result == "")
            Debug.LogError($"Id or word not found: id: {_id}");
        return _result;
    }

    public void UpdateTextMesh(TextMeshProUGUI text, string id = "")
    {
        _textMesh = text;
        if (id != "")
            _id = id;
    }
}