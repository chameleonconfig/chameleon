namespace ChameleonConfig
{
    public interface IConfigService
    {
        void AddConfigProvider(IConfigProvider configProvider);

        T Get<T>();
    }
}