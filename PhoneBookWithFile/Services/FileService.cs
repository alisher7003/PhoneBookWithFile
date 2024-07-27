using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWithFile.Services
{
    internal class FileService : IFileService
    {
        private const string filePath = "../../../phoneBook.txt";
        private ILoggingService log;
        private string SelectedContact;

        public FileService()
        {
            this.log = new LoggingService();
            EnsureFileExists();
        }

        public void AddContact()
        {
            Console.Clear();
            log.LogInfo("Enter the name: ");
            string name = Console.ReadLine();
            log.LogInfo("Enter the phone number: ");
            string phoneNumber = Console.ReadLine();
            string contact = $"{name}, {phoneNumber}";
            File.AppendAllText(filePath, contact + Environment.NewLine);
        }

        public void RemoveContact()
        {
            Console.Clear();
            log.LogInfo("Enter the name: ");
            string name = Console.ReadLine();
            string[] allContacts = File.ReadAllLines(filePath);
            string tempAllContacts = string.Empty;
            foreach (var item in allContacts)
            {
                string splitName = item.Split(",")[0];
                if (!splitName.ToUpper().Equals(name.ToUpper()))
                {
                    tempAllContacts += item + "\n";
                }
            }
            DeleteAndCreateFile(tempAllContacts);
        }

        private void DeleteAndCreateFile(string tempAllContacts)
        {
            File.Delete(filePath);
            EnsureFileExists();
            File.AppendAllText(filePath, tempAllContacts);
        }

        public void ReadAllContacts()
        {
            Console.Clear();
            string txt = File.ReadAllText(filePath);
            log.LogInfoLine(txt);
        }

        public void SearchContact()
        {
            Console.Clear();
            SelectedContact = string.Empty;
            log.LogInfo("Search contact name: ");
            string searchName = Console.ReadLine();
            string[] allContacts = File.ReadAllLines(filePath);

            foreach (var contact in allContacts)
            {
                string splitName = contact.Split(",")[0];
                if (splitName.ToUpper().Equals(searchName.ToUpper()))
                {
                    log.LogInfoLine(contact);
                    SelectedContact = contact;
                }
            }
            if (SelectedContact == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                log.LogInfoLine("Contact is not found! try again.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void EnsureFileExists()
        {
            bool isFilePresent = File.Exists(filePath);
            if (!isFilePresent)
            {
                File.Create(filePath).Close();
            }
        }

        public void UpdateContact()
        {
            Console.Clear();
            SearchContact();
            if (!SelectedContact.Equals(""))
            {
                log.LogInfoLine("1. change name\n2. change number");
                int selection = int.Parse(Console.ReadLine());
                string contacts = File.ReadAllText(filePath);
                string[] nameAndNumber = SelectedContact.Split(",");
                string updatedContact = string.Empty;

                if (selection == 1)
                {
                    log.LogInfo("Enter the new name: ");
                    string newName = Console.ReadLine();
                    updatedContact = contacts.Replace(nameAndNumber[0], newName);
                }
                else if (selection == 2)
                {
                    log.LogInfo("Enter the new phone number: ");
                    string newPhoneNumber = Console.ReadLine();
                    updatedContact = contacts.Replace(nameAndNumber[1], newPhoneNumber);
                }
                DeleteAndCreateFile(updatedContact);

            }
        }

        public void ClearAllContacts()
        {
            Console.Clear();
            File.WriteAllText(filePath, "");
            Console.ForegroundColor = ConsoleColor.Red;
            log.LogInfoLine("All contacts are cleared successfully");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowMenu()
        {
            string selction;
            do
            {
                Console.Clear();
                log.LogInfoLine("Welcome to the Contact app");
                log.LogInfoLine("1. Read all contact");
                log.LogInfoLine("2. Search contact");
                log.LogInfoLine("3. Add contact");
                log.LogInfoLine("4. Remove contact");
                log.LogInfoLine("5. Edit contact");
                log.LogInfoLine("6. Clear all contacts\n");

                log.LogInfo("Enter your choice: ");
                try
                {
                    int yourChoice = int.Parse(Console.ReadLine());
                    switch (yourChoice)
                    {
                        case 1:
                            ReadAllContacts();
                            break;
                        case 2:
                            SearchContact();
                            break;
                        case 3:
                            AddContact();
                            break;
                        case 4:
                            RemoveContact();
                            break;
                        case 5:
                            UpdateContact();
                            break;
                        case 6:
                            ClearAllContacts();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            log.LogInfoLine("Enter the correct choice! on the menu");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine("Wrong!!! please enter only number(between 1 and 5)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine("Wrong!!! please enter 1, 2, 3, 4 or 5");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    log.LogInfoLine
                        ("If you make the right choice, " +
                        "and if the program is working incorrectly, " +
                        "please contact us");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                log.LogInfo("Do you want using again?\nplease press (yes) or (y): ");
                selction = Console.ReadLine().ToLower();
            } while (selction is "yes" or "y");
        }
    }
}
