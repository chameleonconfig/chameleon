namespace ChameleonConfig
{
    public interface ISectionStore
    {
        void Add(ISection section);
    }

    public interface ISection
    {
        string Name { get; }
    }
}