namespace Paps.UnityUpdater
{
    public interface IUnityUpdater
    {
        bool IsEnabled { get; }

        void SubscribeToUpdate(IUpdateListener listener);
        void UnsubscribeFromUpdate(IUpdateListener updateable);
        void SubscribeToLateUpdate(ILateUpdateListener listener);
        void UnsubscribeFromLateUpdate(ILateUpdateListener updateable);
        void SubscribeToFixedUpdate(IFixedUpdateListener listener);
        void UnsubscribeFromFixedUpdate(IFixedUpdateListener updateable);
        bool IsSubscribedToUpdate(IUpdateListener listener);
        bool IsSubscribedToLateUpdate(ILateUpdateListener listener);
        bool IsSubscribedToFixedUpdate(IFixedUpdateListener listener);
        void ExecuteUpdates();
        void ExecuteLateUpdates();
        void ExecuteFixedUpdates();
        void Enable();
        void Disable();
        void EnableUpdateListener(IUpdateListener listener);
        void DisableUpdateListener(IUpdateListener listener);
        void EnableLateUpdateListener(ILateUpdateListener listener);
        void DisableLateUpdateListener(ILateUpdateListener listener);
        void EnableFixedUpdateListener(IFixedUpdateListener listener);
        void DisableFixedUpdateListener(IFixedUpdateListener listener);
        bool IsEnabledForUpdate(IUpdateListener listener);
        bool IsEnabledForLateUpdate(ILateUpdateListener listener);
        bool IsEnabledForFixedUpdate(IFixedUpdateListener listener);
    }
}