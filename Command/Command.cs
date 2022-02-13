namespace YWR.Tools
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();
    }
}