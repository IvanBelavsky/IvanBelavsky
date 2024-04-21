using UnityEngine;

public class LoadingServicGame : MonoBehaviour
{
   [field: SerializeField] public bool IsLoad { get; private set; }

   public void Loaouding(bool isLoad)
   {
      IsLoad = isLoad;
   }
}