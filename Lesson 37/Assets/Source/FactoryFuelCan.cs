using System;
using UnityEngine;

public class FactoryFuelCan : MonoBehaviour
{
   private FuelCan _fuel;

   private void Awake()
   {
      _fuel = Resources.Load<FuelCan>("FuelCan");
   }

   public void CreateeFuel(Vector3 position)
   {
      Instantiate(_fuel, position, Quaternion.identity);
   }
}
