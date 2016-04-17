public abstract class State
{
    public Enemy owner;
    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}