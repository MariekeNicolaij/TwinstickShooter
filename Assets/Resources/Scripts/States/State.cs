public abstract class State
{
    public Enemy owner;
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Stop() { }
}