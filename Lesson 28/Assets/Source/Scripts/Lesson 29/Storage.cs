using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [field: SerializeField] private List<Resours> _resources;

    private void Awake() => _resources.Clear();

    public void SetResources(List<Resours> resources) => _resources.AddRange(resources.Select(resours => resours));
}