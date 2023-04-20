using TechConfEvents.Dto;

namespace TechConfEvents.Interface
{
    public interface ICSVService
    {
        void WriteCSV<T>(List<T> records);
    }
}
    