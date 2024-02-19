using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 position;
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Level { get; private set; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            Save();
        if(Input.GetKeyDown(KeyCode.E))
            Load();
    }

    private void Save()
    {
        SaveService.SavePlayer(this);
    }

    private void Load()
    {
        SaveData data = SaveService.LoadPlayer();

        Health = data.Health;
        Level = data.Level;

        position.x = data.Position[0];
        position.y = data.Position[1];
        position.z = data.Position[2];
        transform.position = position;
    }
}