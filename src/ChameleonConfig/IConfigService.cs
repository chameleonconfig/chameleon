namespace ChameleonConfig
{
    public interface IConfigService
    {
        T Get<T>();
    }
}