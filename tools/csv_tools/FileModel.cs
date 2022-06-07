namespace csv_tools{
    public class FileModel : IFileProcess {
        private string _fileLocation = "";
        private string _header = "";
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public void PromptUser()
        {
            Console.WriteLine("Please type the location of the file, including the full file name: ");
            Console.WriteLine("=================================================");
        }

        public void GetUserInput()
        {
            string? input = Console.ReadLine();

            input = input ?? "";

            _fileLocation = input!;

            CheckFile();
        }

        private void CheckFile()
        {
            if (_fileLocation == null || _fileLocation == String.Empty)
            {
                Console.WriteLine("Sorry, we need a file location to proceed. Press return to try again.");
                Console.ReadLine();
                GetUserInput();
            }

            if (!File.Exists(_fileLocation))
            {
                Console.WriteLine("Sorry, there is no file in that location. Press return to try again.");
                Console.ReadLine();
                GetUserInput();
            }
        }

        public void ProceedWithOption(ToolOptions option)
        {
            CSV csv = new CSV(_fileLocation);
            csv.ProceedWithOption(option);
        } 

        public void Finish()
        {
            Console.WriteLine("Press return to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }       
    }
}