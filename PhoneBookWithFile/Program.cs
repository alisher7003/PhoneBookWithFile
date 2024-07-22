using PhoneBookWithFile.Services;

namespace PhoneBookWithFile
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            FileService fileService = new FileService();
            fileService.ShowMenu();
        }
    }
}
