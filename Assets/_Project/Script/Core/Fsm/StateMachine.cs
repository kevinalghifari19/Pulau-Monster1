// StateMachine.cs
public class StateMachine
{
    public IState Current { get; private set; }

    public void ChangeTo(IState next)
    {
        if (next == Current) return;
        Current?.Exit();        // state lama keluar dulu
        Current = next;
        Current.Enter();       // baru state baru masuk
    }

    public void Tick(float deltaTime) => Current?.Tick(deltaTime);
}