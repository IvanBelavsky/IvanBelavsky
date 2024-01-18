using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class TimeTickBonus : MonoBehaviour, IEntryPointSetupPlayer
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _timeBonus;

    private Coroutine _timeTick;

    private void BarField(float time)
    {
        StartCoroutine(TimeTick(time));
        Debug.Log("got");
    }

    public void Open() => _timeBonus.SetActive(true);

    public void Setup(PlayerMovement player)
    {
        player.GetComponent<PlayerHealth>().OnSetBonus += BarField;
        Debug.Log(player.gameObject.name);
    }

    private IEnumerator TimeTick(float time)
    {
        while (time >= 0)
        {
            _text.text = $"Time bonus: {time.ToString()}";
            time--;
            yield return new WaitForSeconds(1);
        }

        _text.text = " ";
    }
}