using NUnit.Framework;

namespace Tests
{
    public class CreatureManagerTest
    {
        [Test]
        public void CreatureManagerTestSimplePasses()
        {
            CreatureManager creatureManager = new CreatureManager();
            creatureManager.life = 2;
            creatureManager.TakeDamage(1);

            Assert.AreEqual(1, creatureManager.life);
        }
    }
}
