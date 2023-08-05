public struct SimpleDelayer
{
    public SimpleDelayer(ushort _delay)
    {
        delay = _delay;
        curDelay = 0;
    }
    internal readonly ushort delay;
    private ushort curDelay;

    internal void Move()
    {
        if (curDelay > 0)
        {
            curDelay--;
        }
    }
    internal bool OnElapsed()
    {
        if (curDelay <= 0)
        {
            curDelay = delay;
            return true;
        }
        return false;
    }
}