using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestDieCharacter
{
    [UnityTest]
    public IEnumerator TestDieCharacterWithEnumeratorPasses()
    {
        GameObject gameObject = new GameObject();
        Fight fight = gameObject.AddComponent<Fight>();
        yield return new WaitForSeconds(20f); 
        Assert.IsTrue(fight._isDie);
    }
}
