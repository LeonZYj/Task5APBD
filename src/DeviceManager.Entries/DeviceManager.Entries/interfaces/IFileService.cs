namespace DeviceManager.Entries.Interfaces
{


    public interface IFileService
    {
        List<string> ReadFile(string filePath);
        void writeLineToFile(string filePath, string line);
    }
}