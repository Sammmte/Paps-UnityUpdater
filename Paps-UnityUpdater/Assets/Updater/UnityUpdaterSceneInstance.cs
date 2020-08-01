using UnityEngine;

namespace Paps.UnityUpdater
{
    public class UnityUpdaterSceneInstance : MonoBehaviour
    {
        private IUnityUpdater unityUpdater;

        public void SetUnityUpdater(IUnityUpdater updateManager)
        {
            this.unityUpdater = updateManager;
        }

        public IUnityUpdater GetUnityUpdater()
        {
            return unityUpdater;
        }

        private void Update()
        {
            unityUpdater.ExecuteUpdates();
        }

        private void LateUpdate()
        {
            unityUpdater.ExecuteLateUpdates();
        }

        private void FixedUpdate()
        {
            unityUpdater.ExecuteFixedUpdates();
        }
    }
}