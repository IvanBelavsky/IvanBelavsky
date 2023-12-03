using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryCircle : MonoBehaviour
{
    [SerializeField] private Circle _circle;

    public Circle CreatedCircle(Vector3 position)
    {
        return Instantiate(_circle, position, Quaternion.Euler(new Vector3(90, 0, 0))).SetColor(Color.white);
    }

    public Circle CreatedMoveCircle(Vector3 position)
    {
        return Instantiate(_circle, position, Quaternion.Euler(new Vector3(90, 0, 0))).SetMoveTape().SetColor(Color.green);
    }

    public Circle CreatedBadCircle(Vector3 position)
    {
        return Instantiate(_circle, position, Quaternion.Euler(new Vector3(90, 0, 0))).SetColor(Color.red).SetLifeTime(15);
    }

    public Circle CreatedGoldCircle(Vector3 position)
    {
        return Instantiate(_circle, position, Quaternion.Euler(new Vector3(90, 0, 0))).SetColor(Color.yellow).SetLifeTime(1).SetScore(5);
    }

    public Circle CreatedRemoveCircle(Vector3 position)
    {
        return Instantiate(_circle, position, Quaternion.Euler(new Vector3(90, 0, 0))).SetColor(Color.blue).SetLifeTime(3).SetScore(-5);
    }

    public Circle CreatedColorCircle(Vector3 position)
    {
        return Instantiate(_circle, position, Quaternion.Euler(new Vector3(90, 0, 0))).SetRandomColor().SetLifeTime(4).SetScore(3);
    }
}
