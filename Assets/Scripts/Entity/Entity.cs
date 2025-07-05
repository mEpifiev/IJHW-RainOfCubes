using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public event Action<Entity> Released;

    public virtual void Initialize() { }

    public virtual void Initialize(Entity entity) { }

    public void SubscribeToReleased(Action<Entity> callback)
    {
        Released += callback;
    }

    public void UnsubscribeFromReleased(Action<Entity> callback)
    {
        Released -= callback;
    }

    protected void InvokeReleased()
    {
        Released?.Invoke(this);
    }
}
