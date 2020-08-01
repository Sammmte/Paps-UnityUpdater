using System.Collections.Generic;

namespace Paps.UnityUpdater
{
    public class UnityUpdater : IUnityUpdater
    {
        private int updateListenersCurrentIndex = 0;
        private int lateUpdateListenersCurrentIndex = 0;
        private int fixedUpdateListenersCurrentIndex = 0;

        private List<IUpdateListener> updateListeners = new List<IUpdateListener>();
        private List<ILateUpdateListener> lateUpdateListeners = new List<ILateUpdateListener>();
        private List<IFixedUpdateListener> fixedUpdateListeners = new List<IFixedUpdateListener>();

        public bool IsEnabled { get; private set; } = true;

        public void Disable()
        {
            IsEnabled = false;
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        public bool IsSubscribedToUpdate(IUpdateListener listener)
        {
            return updateListeners.Contains(listener);
        }

        public bool IsSubscribedToLateUpdate(ILateUpdateListener listener)
        {
            return lateUpdateListeners.Contains(listener);
        }

        public bool IsSubscribedToFixedUpdate(IFixedUpdateListener listener)
        {
            return fixedUpdateListeners.Contains(listener);
        }

        public void SubscribeToUpdate(IUpdateListener listener)
        {
            if (IsSubscribedToUpdate(listener)) 
                return;

            updateListeners.Add(listener);
        }

        public void UnsubscribeFromUpdate(IUpdateListener listener)
        {
            int indexOfListener = updateListeners.IndexOf(listener);

            if (updateListeners.Remove(listener))
            {
                if (indexOfListener <= updateListenersCurrentIndex && updateListenersCurrentIndex > 0)
                    updateListenersCurrentIndex--;
            }
        }

        public void SubscribeToLateUpdate(ILateUpdateListener listener)
        {
            if (IsSubscribedToLateUpdate(listener))
                return;

            lateUpdateListeners.Add(listener);
        }

        public void UnsubscribeFromLateUpdate(ILateUpdateListener listener)
        {
            int indexOfListener = lateUpdateListeners.IndexOf(listener);

            if (lateUpdateListeners.Remove(listener))
            {
                if (indexOfListener <= lateUpdateListenersCurrentIndex && lateUpdateListenersCurrentIndex > 0)
                    lateUpdateListenersCurrentIndex--;
            }
        }

        public void SubscribeToFixedUpdate(IFixedUpdateListener listener)
        {
            if (IsSubscribedToFixedUpdate(listener))
                return;

            fixedUpdateListeners.Add(listener);
        }

        public void UnsubscribeFromFixedUpdate(IFixedUpdateListener listener)
        {
            int indexOfListener = fixedUpdateListeners.IndexOf(listener);

            if (fixedUpdateListeners.Remove(listener))
            {
                if (indexOfListener <= fixedUpdateListenersCurrentIndex && fixedUpdateListenersCurrentIndex > 0)
                    fixedUpdateListenersCurrentIndex--;
            }
        }

        public void ExecuteUpdates()
        {
            for (updateListenersCurrentIndex = 0; updateListenersCurrentIndex < updateListeners.Count && IsEnabled; updateListenersCurrentIndex++)
            {
                var listenerItem = updateListeners[updateListenersCurrentIndex];

                listenerItem.DoUpdate();
            }

            updateListenersCurrentIndex = 0;
        }

        public void ExecuteLateUpdates()
        {
            for (lateUpdateListenersCurrentIndex = 0; lateUpdateListenersCurrentIndex < lateUpdateListeners.Count && IsEnabled; lateUpdateListenersCurrentIndex++)
            {
                var listenerItem = lateUpdateListeners[lateUpdateListenersCurrentIndex];

                listenerItem.DoLateUpdate();
            }

            lateUpdateListenersCurrentIndex = 0;
        }

        public void ExecuteFixedUpdates()
        {
            for (fixedUpdateListenersCurrentIndex = 0; fixedUpdateListenersCurrentIndex < fixedUpdateListeners.Count && IsEnabled; fixedUpdateListenersCurrentIndex++)
            {
                var listenerItem = fixedUpdateListeners[fixedUpdateListenersCurrentIndex];

                listenerItem.DoFixedUpdate();
            }

            fixedUpdateListenersCurrentIndex = 0;
        }
    }

}