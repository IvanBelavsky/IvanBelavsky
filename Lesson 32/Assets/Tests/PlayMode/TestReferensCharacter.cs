using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestReferensCharacter
{
    [UnityTest]
    public IEnumerator TestScriptWithEnumeratorPasses()
    {
        GameObject gameObject = new GameObject();
        Factory factory = gameObject.AddComponent<Factory>();
        yield return new WaitForSeconds(1);
        Character character = factory.CreateCharacterFirst();
        Assert.IsNotNull(character);
    }
    [UnityTest]
    public IEnumerator TestReferensCharacterEnumeratorPasses()
    {
        GameObject gameObject = new GameObject();
        Factory factory = gameObject.AddComponent<Factory>();
        yield return new WaitForSeconds(1);
        Character character = factory.CreateCharacterSecond();
        Assert.IsNotNull(character);
    }
}
