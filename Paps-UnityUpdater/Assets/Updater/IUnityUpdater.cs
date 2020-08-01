namespace Paps.UnityUpdater
{
    public interface IUnityUpdater
    {
        bool IsEnabled { get; }

        void SubscribeToUpdate(IUpdateListener updatable);
        void UnsubscribeFromUpdate(IUpdateListener updateable);
        void SubscribeToLateUpdate(ILateUpdateListener updatable);
        void UnsubscribeFromLateUpdate(ILateUpdateListener updateable);
        void SubscribeToFixedUpdate(IFixedUpdateListener updatable);
        void UnsubscribeFromFixedUpdate(IFixedUpdateListener updateable);
        bool IsSubscribedToUpdate(IUpdateListener updatable);
        bool IsSubscribedToLateUpdate(ILateUpdateListener updatable);
        bool IsSubscribedToFixedUpdate(IFixedUpdateListener updatable);
        void ExecuteUpdates();
        void ExecuteLateUpdates();
        void ExecuteFixedUpdates();
        void Enable();
        void Disable();
    }
}