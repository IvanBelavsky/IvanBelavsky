using NUnit.Framework;

public class TestHelthCharacter
{
    [Test]
    public void TestHelthCharacterSimplePasses()
    {
        Character characterFirst = new FirstCharacter();
        Character characterSecond = new SecondCharacter();
        int test = characterFirst.Health;
        int testSecond = characterSecond.Health;
        characterFirst.TakeDamage(10);
        characterSecond.TakeDamage(10);
        Assert.AreEqual(test - 10, characterFirst.Health);
        Assert.AreEqual(testSecond - 10, characterSecond.Health);
    }
}