using NSubstitute;
using NUnit.Framework;
using Paps.UnityUpdater;

namespace Tests
{
    public class UnityUpdaterShould
    {
        private UnityUpdater unityUpdater;

        [SetUp]
        public void SetUp()
        {
            unityUpdater = new UnityUpdater();
        }

        [Test]
        public void Add_Update_Listeners()
        {
            //Given
            var listener = Substitute.For<IUpdateListener>();

            //When
            unityUpdater.SubscribeToUpdate(listener);

            //Then
            Assert.That(unityUpdater.IsSubscribedToUpdate(listener), "Contains added listener");
        }

        [Test]
        public void Add_LateUpdate_Listeners()
        {
            //Given
            var listener = Substitute.For<ILateUpdateListener>();

            //When
            unityUpdater.SubscribeToLateUpdate(listener);

            //Then
            Assert.That(unityUpdater.IsSubscribedToLateUpdate(listener), "Contains added listener");
        }

        [Test]
        public void Add_FixedUpdate_Listeners()
        {
            //Given
            var listener = Substitute.For<IFixedUpdateListener>();

            //When
            unityUpdater.SubscribeToFixedUpdate(listener);

            //Then
            Assert.That(unityUpdater.IsSubscribedToFixedUpdate(listener), "Contains added listener");
        }
    }
}