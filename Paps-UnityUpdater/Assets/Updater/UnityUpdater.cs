using System.Collections.Generic;

namespace Paps.UnityUpdater
{
    public class UnityUpdater : IUnityUpdater
    {
        private int updateListenersCurrentIndex = 0;
        private int lateUpdateListenersCurrentIndex = 0;
        private int fixedUpdateListenersCurrentIndex = 0;

        private List<UpdateListener> updateListeners = new List<UpdateListener>();
        private List<LateUpdateListener> lateUpdateListeners = new List<LateUpdateListener>();
        private List<FixedUpdateListener> fixedUpdateListeners = new List<FixedUpdateListener>();

        public bool IsEnabled { get; private set; } = true;

        public void Disable()
        {
            IsEnabled = false;
        }

        public void Enable()
        {
            IsEnabled = true;
        }

        private UpdateListener Find(IUpdateListener listener)
        {
            return updateListeners.Find(element => element.Listener == listener);
        }

        private LateUpdateListener Find(ILateUpdateListener listener)
        {
            return lateUpdateListeners.Find(element => element.Listener == listener);
        }

        private FixedUpdateListener Find(IFixedUpdateListener listener)
        {
            return fixedUpdateListeners.Find(element => element.Listener == listener);
        }

        private bool ContainsListener(IUpdateListener listener)
        {
            return Find(listener) != null;
        }

        private bool ContainsListener(ILateUpdateListener listener)
        {
            return Find(listener) != null;
        }

        private bool ContainsListener(IFixedUpdateListener listener)
        {
            return Find(listener) != null;
        }

        public bool IsSubscribedToUpdate(IUpdateListener listener)
        {
            return ContainsListener(listener);
        }

        public bool IsSubscribedToLateUpdate(ILateUpdateListener listener)
        {
            return ContainsListener(listener);
        }

        public bool IsSubscribedToFixedUpdate(IFixedUpdateListener listener)
        {
            return ContainsListener(listener);
        }

        public void SubscribeToUpdate(IUpdateListener listener)
        {
            if (IsSubscribedToUpdate(listener)) 
                return;

            updateListeners.Add(new UpdateListener(listener, true));
        }

        public void UnsubscribeFromUpdate(IUpdateListener listener)
        {
            if(ContainsListener(listener))
            {
                var listenerElement = Find(listener);

                int indexOfListener = updateListeners.IndexOf(listenerElement);

                updateListeners.Remove(listenerElement);

                if (indexOfListener <= updateListenersCurrentIndex && updateListenersCurrentIndex > 0)
                    updateListenersCurrentIndex--;
            }

            
        }

        public void SubscribeToLateUpdate(ILateUpdateListener listener)
        {
            if (IsSubscribedToLateUpdate(listener))
                return;

            lateUpdateListeners.Add(new LateUpdateListener(listener, true));
        }

        public void UnsubscribeFromLateUpdate(ILateUpdateListener listener)
        {
            if(ContainsListener(listener))
            {
                var listenerElement = Find(listener);

                int indexOfListener = lateUpdateListeners.IndexOf(listenerElement);

                lateUpdateListeners.Remove(listenerElement);

                if (indexOfListener <= lateUpdateListenersCurrentIndex && lateUpdateListenersCurrentIndex > 0)
                    lateUpdateListenersCurrentIndex--;
            }
        }

        public void SubscribeToFixedUpdate(IFixedUpdateListener listener)
        {
            if (IsSubscribedToFixedUpdate(listener))
                return;

            fixedUpdateListeners.Add(new FixedUpdateListener(listener, true));
        }

        public void UnsubscribeFromFixedUpdate(IFixedUpdateListener listener)
        {
            if(ContainsListener(listener))
            {
                var listenerElement = Find(listener);

                int indexOfListener = fixedUpdateListeners.IndexOf(listenerElement);

                fixedUpdateListeners.Remove(listenerElement);

                if (indexOfListener <= fixedUpdateListenersCurrentIndex && fixedUpdateListenersCurrentIndex > 0)
                    fixedUpdateListenersCurrentIndex--;
            }
        }

        public void ExecuteUpdates()
        {
            for (updateListenersCurrentIndex = 0; updateListenersCurrentIndex < updateListeners.Count && IsEnabled; updateListenersCurrentIndex++)
            {
                var listenerItem = updateListeners[updateListenersCurrentIndex];

                if(listenerItem.Enabled)
                    listenerItem.Listener.DoUpdate();
            }

            updateListenersCurrentIndex = 0;
        }

        public void ExecuteLateUpdates()
        {
            for (lateUpdateListenersCurrentIndex = 0; lateUpdateListenersCurrentIndex < lateUpdateListeners.Count && IsEnabled; lateUpdateListenersCurrentIndex++)
            {
                var listenerItem = lateUpdateListeners[lateUpdateListenersCurrentIndex];

                if (listenerItem.Enabled)
                    listenerItem.Listener.DoLateUpdate();
            }

            lateUpdateListenersCurrentIndex = 0;
        }

        public void ExecuteFixedUpdates()
        {
            for (fixedUpdateListenersCurrentIndex = 0; fixedUpdateListenersCurrentIndex < fixedUpdateListeners.Count && IsEnabled; fixedUpdateListenersCurrentIndex++)
            {
                var listenerItem = fixedUpdateListeners[fixedUpdateListenersCurrentIndex];

                if (listenerItem.Enabled)
                    listenerItem.Listener.DoFixedUpdate();
            }

            fixedUpdateListenersCurrentIndex = 0;
        }

        public void EnableUpdateListener(IUpdateListener listener)
        {
            if (ContainsListener(listener))
                Find(listener).Enabled = true;
        }

        public void DisableUpdateListener(IUpdateListener listener)
        {
            if (ContainsListener(listener))
                Find(listener).Enabled = false;
        }

        public void EnableLateUpdateListener(ILateUpdateListener listener)
        {
            if (ContainsListener(listener))
                Find(listener).Enabled = true;
        }

        public void DisableLateUpdateListener(ILateUpdateListener listener)
        {
            if(ContainsListener(listener))
                Find(listener).Enabled = false;
        }

        public void EnableFixedUpdateListener(IFixedUpdateListener listener)
        {
            if (ContainsListener(listener))
                Find(listener).Enabled = true;
        }

        public void DisableFixedUpdateListener(IFixedUpdateListener listener)
        {
            if (ContainsListener(listener))
                Find(listener).Enabled = false;
        }

        public bool IsEnabledForUpdate(IUpdateListener listener)
        {
            if (ContainsListener(listener))
                return Find(listener).Enabled;
            else
                return false;
        }

        public bool IsEnabledForLateUpdate(ILateUpdateListener listener)
        {
            if (ContainsListener(listener))
                return Find(listener).Enabled;
            else
                return false;
        }

        public bool IsEnabledForFixedUpdate(IFixedUpdateListener listener)
        {
            if (ContainsListener(listener))
                return Find(listener).Enabled;
            else
                return false;
        }

        private class UpdateListener
        {
            public IUpdateListener Listener;
            public bool Enabled;

            public UpdateListener(IUpdateListener listener, bool enabled)
            {
                Listener = listener;
                Enabled = enabled;
            }
        }

        private class LateUpdateListener
        {
            public ILateUpdateListener Listener;
            public bool Enabled;

            public LateUpdateListener(ILateUpdateListener listener, bool enabled)
            {
                Listener = listener;
                Enabled = enabled;
            }
        }

        private class FixedUpdateListener
        {
            public IFixedUpdateListener Listener;
            public bool Enabled;

            public FixedUpdateListener(IFixedUpdateListener listener, bool enabled)
            {
                Listener = listener;
                Enabled = enabled;
            }
        }
    }

}