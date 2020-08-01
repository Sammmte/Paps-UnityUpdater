using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Paps.UnityUpdater
{
    public static class UnityUpdaterExtensions
    {
        public static void CreateUpdaterSceneInstance(this IUnityUpdater updater)
        {
            if (updater == null)
                throw new NullReferenceException();

            GameObject gameObject = new GameObject(nameof(UnityUpdaterSceneInstance));

            var updaterInstance = gameObject.AddComponent<UnityUpdaterSceneInstance>();

            Object.DontDestroyOnLoad(gameObject);

            updaterInstance.SetUnityUpdater(updater);
        }

        public static void DestroyUpdaterSceneInstance(this IUnityUpdater updater)
        {
            if (updater == null)
                throw new NullReferenceException();

            var updaterSceneInstances = Object.FindObjectsOfType<UnityUpdaterSceneInstance>();

            foreach (var instance in updaterSceneInstances)
            {
                if (instance.GetUnityUpdater() == updater)
                {
                    Object.Destroy(instance.gameObject);
                    return;
                }
            }
        }
    }
}