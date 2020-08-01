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

        [Test]
        public void Remove_Update_Listeners()
        {
            //Given
            var listener = Substitute.For<IUpdateListener>();
            unityUpdater.SubscribeToUpdate(listener);

            //When
            unityUpdater.UnsubscribeFromUpdate(listener);

            //Then
            Assert.That(unityUpdater.IsSubscribedToUpdate(listener) == false, "Does not contains removed listener");
        }

        [Test]
        public void Remove_LateUpdate_Listeners()
        {
            //Given
            var listener = Substitute.For<ILateUpdateListener>();
            unityUpdater.SubscribeToLateUpdate(listener);

            //When
            unityUpdater.UnsubscribeFromLateUpdate(listener);

            //Then
            Assert.That(unityUpdater.IsSubscribedToLateUpdate(listener) == false, "Does not contains removed listener");
        }

        [Test]
        public void Remove_FixedUpdate_Listeners()
        {
            //Given
            var listener = Substitute.For<IFixedUpdateListener>();
            unityUpdater.SubscribeToFixedUpdate(listener);

            //When
            unityUpdater.UnsubscribeFromFixedUpdate(listener);

            //Then
            Assert.That(unityUpdater.IsSubscribedToFixedUpdate(listener) == false, "Does not contains removed listener");
        }

        [Test]
        public void Execute_Update_Listeners_In_Same_Order_They_Were_Added()
        {
            //Given
            var listener1 = Substitute.For<IUpdateListener>();
            var listener2 = Substitute.For<IUpdateListener>();
            unityUpdater.SubscribeToUpdate(listener1);
            unityUpdater.SubscribeToUpdate(listener2);

            //When
            unityUpdater.ExecuteUpdates();

            //Then
            Received.InOrder(() =>
            {
                listener1.DoUpdate();
                listener2.DoUpdate();
            });
        }

        [Test]
        public void Execute_LateUpdate_Listeners_In_Same_Order_They_Were_Added()
        {
            //Given
            var listener1 = Substitute.For<ILateUpdateListener>();
            var listener2 = Substitute.For<ILateUpdateListener>();
            unityUpdater.SubscribeToLateUpdate(listener1);
            unityUpdater.SubscribeToLateUpdate(listener2);

            //When
            unityUpdater.ExecuteLateUpdates();

            //Then
            Received.InOrder(() =>
            {
                listener1.DoLateUpdate();
                listener2.DoLateUpdate();
            });
        }

        [Test]
        public void Execute_FixedUpdate_Listeners_In_Same_Order_They_Were_Added()
        {
            //Given
            var listener1 = Substitute.For<IFixedUpdateListener>();
            var listener2 = Substitute.For<IFixedUpdateListener>();
            unityUpdater.SubscribeToFixedUpdate(listener1);
            unityUpdater.SubscribeToFixedUpdate(listener2);

            //When
            unityUpdater.ExecuteFixedUpdates();

            //Then
            Received.InOrder(() =>
            {
                listener1.DoFixedUpdate();
                listener2.DoFixedUpdate();
            });
        }

        [Test]
        public void Do_Nothing_When_User_Tries_To_Add_An_Update_Listener_Twice()
        {
            //Given
            var listener = Substitute.For<IUpdateListener>();

            //When
            unityUpdater.SubscribeToUpdate(listener);
            unityUpdater.SubscribeToUpdate(listener);
            unityUpdater.ExecuteUpdates();

            //Then
            listener.Received(1).DoUpdate();
        }

        [Test]
        public void Do_Nothing_When_User_Tries_To_Add_An_LateUpdate_Listener_Twice()
        {
            //Given
            var listener = Substitute.For<ILateUpdateListener>();

            //When
            unityUpdater.SubscribeToLateUpdate(listener);
            unityUpdater.SubscribeToLateUpdate(listener);
            unityUpdater.ExecuteLateUpdates();

            //Then
            listener.Received(1).DoLateUpdate();
        }

        [Test]
        public void Do_Nothing_When_User_Tries_To_Add_An_FixedUpdate_Listener_Twice()
        {
            //Given
            var listener = Substitute.For<IFixedUpdateListener>();

            //When
            unityUpdater.SubscribeToFixedUpdate(listener);
            unityUpdater.SubscribeToFixedUpdate(listener);
            unityUpdater.ExecuteFixedUpdates();

            //Then
            listener.Received(1).DoFixedUpdate();
        }

        [Test]
        public void Do_Not_Execute_Any_Type_Of_Update_When_It_Is_Disabled()
        {
            //Given
            var updateListener = Substitute.For<IUpdateListener>();
            var lateUpdateListener = Substitute.For<ILateUpdateListener>();
            var fixedUpdateListener = Substitute.For<IFixedUpdateListener>();
            unityUpdater.SubscribeToUpdate(updateListener);
            unityUpdater.SubscribeToLateUpdate(lateUpdateListener);
            unityUpdater.SubscribeToFixedUpdate(fixedUpdateListener);
            unityUpdater.Disable();

            //When
            unityUpdater.ExecuteUpdates();
            unityUpdater.ExecuteLateUpdates();
            unityUpdater.ExecuteFixedUpdates();

            //Then
            updateListener.DidNotReceive().DoUpdate();
            lateUpdateListener.DidNotReceive().DoLateUpdate();
            fixedUpdateListener.DidNotReceive().DoFixedUpdate();
        }

        [Test]
        public void Stop_Executing_Updates_When_It_Is_Disabled()
        {
            //Given
            var listener1 = Substitute.For<IUpdateListener>();
            var listener2 = Substitute.For<IUpdateListener>();
            unityUpdater.SubscribeToUpdate(listener1);
            unityUpdater.SubscribeToUpdate(listener2);

            listener1.When(listener => listener.DoUpdate()).Do(_ => unityUpdater.Disable());

            //When
            unityUpdater.ExecuteUpdates();

            //Then
            listener1.Received(1).DoUpdate();
            listener2.DidNotReceive().DoUpdate();
        }

        [Test]
        public void Stop_Executing_LateUpdates_When_It_Is_Disabled()
        {
            //Given
            var listener1 = Substitute.For<ILateUpdateListener>();
            var listener2 = Substitute.For<ILateUpdateListener>();
            unityUpdater.SubscribeToLateUpdate(listener1);
            unityUpdater.SubscribeToLateUpdate(listener2);

            listener1.When(listener => listener.DoLateUpdate()).Do(_ => unityUpdater.Disable());

            //When
            unityUpdater.ExecuteLateUpdates();

            //Then
            listener1.Received(1).DoLateUpdate();
            listener2.DidNotReceive().DoLateUpdate();
        }

        [Test]
        public void Stop_Executing_FixedUpdates_When_It_Is_Disabled()
        {
            //Given
            var listener1 = Substitute.For<IFixedUpdateListener>();
            var listener2 = Substitute.For<IFixedUpdateListener>();
            unityUpdater.SubscribeToFixedUpdate(listener1);
            unityUpdater.SubscribeToFixedUpdate(listener2);

            listener1.When(listener => listener.DoFixedUpdate()).Do(_ => unityUpdater.Disable());

            //When
            unityUpdater.ExecuteFixedUpdates();

            //Then
            listener1.Received(1).DoFixedUpdate();
            listener2.DidNotReceive().DoFixedUpdate();
        }

        [Test]
        public void Let_Add_New_Update_Listener_During_Update_Execution()
        {
            //Given
            var listener1 = Substitute.For<IUpdateListener>();
            var listener2 = Substitute.For<IUpdateListener>();
            var listenerToAddDuringExecution = Substitute.For<IUpdateListener>();
            unityUpdater.SubscribeToUpdate(listener1);
            unityUpdater.SubscribeToUpdate(listener2);

            listener1.When(listener => listener.DoUpdate()).Do(_ => unityUpdater.SubscribeToUpdate(listenerToAddDuringExecution));

            //When
            unityUpdater.ExecuteUpdates();

            //Then
            listener1.Received(1).DoUpdate();
            listener2.Received(1).DoUpdate();
            listenerToAddDuringExecution.Received(1).DoUpdate();
        }

        [Test]
        public void Let_Add_New_LateUpdate_Listener_During_LateUpdate_Execution()
        {
            //Given
            var listener1 = Substitute.For<ILateUpdateListener>();
            var listener2 = Substitute.For<ILateUpdateListener>();
            var listenerToAddDuringExecution = Substitute.For<ILateUpdateListener>();
            unityUpdater.SubscribeToLateUpdate(listener1);
            unityUpdater.SubscribeToLateUpdate(listener2);

            listener1.When(listener => listener.DoLateUpdate()).Do(_ => unityUpdater.SubscribeToLateUpdate(listenerToAddDuringExecution));

            //When
            unityUpdater.ExecuteLateUpdates();

            //Then
            listener1.Received(1).DoLateUpdate();
            listener2.Received(1).DoLateUpdate();
            listenerToAddDuringExecution.Received(1).DoLateUpdate();
        }

        [Test]
        public void Let_Add_New_FixedUpdate_Listener_During_FixedUpdate_Execution()
        {
            //Given
            var listener1 = Substitute.For<IFixedUpdateListener>();
            var listener2 = Substitute.For<IFixedUpdateListener>();
            var listenerToAddDuringExecution = Substitute.For<IFixedUpdateListener>();
            unityUpdater.SubscribeToFixedUpdate(listener1);
            unityUpdater.SubscribeToFixedUpdate(listener2);

            listener1.When(listener => listener.DoFixedUpdate()).Do(_ => unityUpdater.SubscribeToFixedUpdate(listenerToAddDuringExecution));

            //When
            unityUpdater.ExecuteFixedUpdates();

            //Then
            listener1.Received(1).DoFixedUpdate();
            listener2.Received(1).DoFixedUpdate();
            listenerToAddDuringExecution.Received(1).DoFixedUpdate();
        }

        [Test]
        public void Let_Remove_Not_Yet_Executed_Update_Listener_During_Update_Execution()
        {
            //Given
            var listener1 = Substitute.For<IUpdateListener>();
            var listener2 = Substitute.For<IUpdateListener>();
            var listenerToRemoveDuringExecution = Substitute.For<IUpdateListener>();
            unityUpdater.SubscribeToUpdate(listener1);
            unityUpdater.SubscribeToUpdate(listener2);
            unityUpdater.SubscribeToUpdate(listenerToRemoveDuringExecution);

            listener1.When(listener => listener.DoUpdate()).Do(_ => unityUpdater.UnsubscribeFromUpdate(listenerToRemoveDuringExecution));

            //When
            unityUpdater.ExecuteUpdates();

            //Then
            listener1.Received(1).DoUpdate();
            listener2.Received(1).DoUpdate();
            listenerToRemoveDuringExecution.DidNotReceive().DoUpdate();
        }

        [Test]
        public void Let_Remove_Not_Yet_Executed_LateUpdate_Listener_During_LateUpdate_Execution()
        {
            //Given
            var listener1 = Substitute.For<ILateUpdateListener>();
            var listener2 = Substitute.For<ILateUpdateListener>();
            var listenerToRemoveDuringExecution = Substitute.For<ILateUpdateListener>();
            unityUpdater.SubscribeToLateUpdate(listener1);
            unityUpdater.SubscribeToLateUpdate(listener2);
            unityUpdater.SubscribeToLateUpdate(listenerToRemoveDuringExecution);

            listener1.When(listener => listener.DoLateUpdate()).Do(_ => unityUpdater.UnsubscribeFromLateUpdate(listenerToRemoveDuringExecution));

            //When
            unityUpdater.ExecuteLateUpdates();

            //Then
            listener1.Received(1).DoLateUpdate();
            listener2.Received(1).DoLateUpdate();
            listenerToRemoveDuringExecution.DidNotReceive().DoLateUpdate();
        }

        [Test]
        public void Let_Remove_Not_Yet_Executed_FixedUpdate_Listener_During_FixedUpdate_Execution()
        {
            //Given
            var listener1 = Substitute.For<IFixedUpdateListener>();
            var listener2 = Substitute.For<IFixedUpdateListener>();
            var listenerToRemoveDuringExecution = Substitute.For<IFixedUpdateListener>();
            unityUpdater.SubscribeToFixedUpdate(listener1);
            unityUpdater.SubscribeToFixedUpdate(listener2);
            unityUpdater.SubscribeToFixedUpdate(listenerToRemoveDuringExecution);

            listener1.When(listener => listener.DoFixedUpdate()).Do(_ => unityUpdater.UnsubscribeFromFixedUpdate(listenerToRemoveDuringExecution));

            //When
            unityUpdater.ExecuteFixedUpdates();

            //Then
            listener1.Received(1).DoFixedUpdate();
            listener2.Received(1).DoFixedUpdate();
            listenerToRemoveDuringExecution.DidNotReceive().DoFixedUpdate();
        }

        [Test]
        public void Let_Remove_Already_Executed_Update_Listener_During_Update_Execution()
        {
            //Given
            var listenerToRemoveDuringExecution = Substitute.For<IUpdateListener>();
            var listener2 = Substitute.For<IUpdateListener>();
            var listener3 = Substitute.For<IUpdateListener>();

            unityUpdater.SubscribeToUpdate(listenerToRemoveDuringExecution);
            unityUpdater.SubscribeToUpdate(listener2);
            unityUpdater.SubscribeToUpdate(listener3);

            listener2.When(listener => listener.DoUpdate()).Do(_ => unityUpdater.UnsubscribeFromUpdate(listenerToRemoveDuringExecution));

            //When
            unityUpdater.ExecuteUpdates();

            //Then
            listenerToRemoveDuringExecution.Received(1).DoUpdate();
            listener2.Received(1).DoUpdate();
            listener3.Received(1).DoUpdate();
        }

        [Test]
        public void Let_Remove_Already_Executed_LateUpdate_Listener_During_LateUpdate_Execution()
        {
            //Given
            var listenerToRemoveDuringExecution = Substitute.For<ILateUpdateListener>();
            var listener2 = Substitute.For<ILateUpdateListener>();
            var listener3 = Substitute.For<ILateUpdateListener>();

            unityUpdater.SubscribeToLateUpdate(listenerToRemoveDuringExecution);
            unityUpdater.SubscribeToLateUpdate(listener2);
            unityUpdater.SubscribeToLateUpdate(listener3);

            listener2.When(listener => listener.DoLateUpdate()).Do(_ => unityUpdater.UnsubscribeFromLateUpdate(listenerToRemoveDuringExecution));

            //When
            unityUpdater.ExecuteLateUpdates();

            //Then
            listenerToRemoveDuringExecution.Received(1).DoLateUpdate();
            listener2.Received(1).DoLateUpdate();
            listener3.Received(1).DoLateUpdate();
        }

        [Test]
        public void Let_Remove_Already_Executed_FixedUpdate_Listener_During_FixedUpdate_Execution()
        {
            //Given
            var listenerToRemoveDuringExecution = Substitute.For<IFixedUpdateListener>();
            var listener2 = Substitute.For<IFixedUpdateListener>();
            var listener3 = Substitute.For<IFixedUpdateListener>();

            unityUpdater.SubscribeToFixedUpdate(listenerToRemoveDuringExecution);
            unityUpdater.SubscribeToFixedUpdate(listener2);
            unityUpdater.SubscribeToFixedUpdate(listener3);

            listener2.When(listener => listener.DoFixedUpdate()).Do(_ => unityUpdater.UnsubscribeFromFixedUpdate(listenerToRemoveDuringExecution));

            //When
            unityUpdater.ExecuteFixedUpdates();

            //Then
            listenerToRemoveDuringExecution.Received(1).DoFixedUpdate();
            listener2.Received(1).DoFixedUpdate();
            listener3.Received(1).DoFixedUpdate();
        }
    }
}