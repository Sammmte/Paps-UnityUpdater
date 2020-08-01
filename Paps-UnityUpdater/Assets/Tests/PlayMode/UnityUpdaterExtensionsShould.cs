using NUnit.Framework;
using Paps.UnityUpdater;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Collections;

namespace Tests
{
    public class UnityUpdaterExtensionsShould
    {
        private IUnityUpdater _unityUpdater;

        [SetUp]
        public void SetUp()
        {
            CleanUp();

            _unityUpdater = Substitute.For<IUnityUpdater>();
        }

        private void CleanUp()
        {
            var remainingObject = Object.FindObjectOfType<UnityUpdaterSceneInstance>();

            if (remainingObject != null)
                Object.Destroy(remainingObject);
        }

        [Test]
        public void Create_Unity_Updater_Scene_Instance()
        {
            //Given - When
            _unityUpdater.CreateUpdaterSceneInstance();

            //Then
            Assert.NotNull(Object.FindObjectOfType<UnityUpdaterSceneInstance>());
        }

        [UnityTest]
        public IEnumerator Destroy_Unity_Updater_Scene_Instance()
        {
            //Given
            _unityUpdater.CreateUpdaterSceneInstance();

            //When
            _unityUpdater.DestroyUpdaterSceneInstance();

            yield return null;

            //Then
            Assert.IsNull(Object.FindObjectOfType<UnityUpdaterSceneInstance>());
        }
    }
}
