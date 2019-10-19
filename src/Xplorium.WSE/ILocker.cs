namespace Xplorium.WSE
{
    public interface ILocker
    {
        public void Lock<T>(T TEntity);
    }
}
