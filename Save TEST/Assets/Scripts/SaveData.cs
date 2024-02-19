using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public float[] Position { get; private set; }

    public SaveData(Player player)
    {
        Health = player.Health;
        Level = player.Level;
        Position = new float [3];
        Position[0] = player.transform.position.x;
        Position[1] = player.transform.position.y;
        Position[2] = player.transform.position.z;
    }
    
}
