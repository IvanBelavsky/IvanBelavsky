using System;
using System.Collections;
using UnityEngine;

public class SpawnAmmo : MonoBehaviour
{
   [SerializeField] private float _delay;
   
   private Ammo _ammo;
   private Coroutine _spanTick;
   private bool _isCanSpawn;

   private void Awake()
   {
      _ammo = Resources.Load<Ammo>("Ammo");
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space)&& !_isCanSpawn)
      {
         Spawn();
      }
   }

   private void Spawn()
   {
      _spanTick = StartCoroutine(SpawnTick());
      _isCanSpawn = true;
   }

   private IEnumerator SpawnTick()
   {
      Instantiate(_ammo, transform.position, Quaternion.identity);
      yield return new WaitForSeconds(_delay);
      _isCanSpawn = false;
   }
}
