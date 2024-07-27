using PhoneBookWithFile.Models;
using PhoneBookWithFile.Services;

namespace PhoneBookWithFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileServiceV2 fileService = new FileServiceV2();
            Contact contact = new Contact();
            contact.Name = "Sardor";
            contact.Phone = "7703456791";
            fileService.AddContact(contact);
        }
    }
}
