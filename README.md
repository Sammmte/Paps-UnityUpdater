# Paps Unity Updater

## What is it?

Paps Unity Updater is a central point for Update, LateUpdate and FixedUpdate method execution.

## For what purpose?

Performance, mostly. Also you can add Non-Monobehaviour objects to the updater.

## How to install it
1. The package is available on the [openupm registry](https://openupm.com). You can install it via [openupm-cli](https://github.com/openupm/openupm-cli).
```
openupm add paps.unity-updater
```
2. You can also install via git url by adding these entries in your **manifest.json**
```json
"paps.unity-updater": "https://github.com/Sammmte/Paps-UnityUpdater"
```

## How to use it

### Quick Setup

```csharp
void SomeMethod()
{
    var unityUpdater = new UnityUpdater();

    unityUpdater.SubscribeToUpdate(new MyUpdatableClass());
    unityUpdater.SubscribeToLateUpdate(new MyLateUpdatableClass());
    unityUpdater.SubscribeToFixedUpdate(new MyFixedUpdatableClass());

    //This method creates an object that executes all Update, LateUpdate and FixedUpdate Listeners
    //in their corresponding moment
    unityUpdater.CreateUpdaterSceneInstance();

    //unityUpdater.DestroyUpdaterSceneInstance(); //if you want to destroy that object
}

class MyUpdatableClass : IUpdateListener
{
    public void DoUpdate()
    {
        Debug.Log("Update");
    }
}

class MyLateUpdatableClass : ILateUpdateListener
{
    public void DoLateUpdate()
    {
        Debug.Log("Late Update");
    }
}

class MyFixedUpdatableClass : IFixedUpdateListener
{
    public void DoFixedUpdate()
    {
        Debug.Log("Fixed Update");
    }
}
```

### Manual Execution

Want to do weird stuff? Execute any type of update methods whenever you want.

```csharp
class MyCustomUpdaterMonoBehaviour : MonoBehaviour
{
    UnityUpdater unityUpdater;

    void Awake()
    {
        unityUpdater = new UnityUpdater();
    
        unityUpdater.SubscribeToUpdate(new MyUpdatableClass());
        unityUpdater.SubscribeToLateUpdate(new MyLateUpdatableClass());
        unityUpdater.SubscribeToFixedUpdate(new MyFixedUpdatableClass());
    }

    void Update()
    {
        //I execute my update twice. Because I can.
        unityUpdater.ExecuteUpdates();
        unityUpdater.ExecuteUpdates();
    }

    void LateUpdate()
    {
        unityUpdater.ExecuteLateUpdates();
    }

    void FixedUpdate()
    {
        unityUpdater.ExecuteFixedUpdates();
    }
}

class MyUpdatableClass : IUpdateListener
{
    public void DoUpdate()
    {
        Debug.Log("Update");
    }
}

class MyLateUpdatableClass : ILateUpdateListener
{
    public void DoLateUpdate()
    {
        Debug.Log("Late Update");
    }
}

class MyFixedUpdatableClass : IFixedUpdateListener
{
    public void DoFixedUpdate()
    {
        Debug.Log("Fixed Update");
    }
}
```