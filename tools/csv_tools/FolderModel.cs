namespace csv_tools{
    public class FolderModel : IFileProcess {
        private string _folderLocation = "";
        private int _files = 0;
        private string[] files;

        public int FileCount
        {
            get { return _files; }
            set { _files = value; }
        }

        public void PromptUser()
        {
            Console.WriteLine("Please type the folder location of the file(s): ");
            Console.WriteLine("=================================================");
        }

        public void GetUserInput()
        {
            string? input = Console.ReadLine();

            input = input ?? "";

            _folderLocation = input!;
        }

        private void CheckFolder()
        {
            if (_folderLocation == null || _folderLocation == String.Empty)
            {
                Console.WriteLine("Sorry, we need a file location to proceed. Press return to try again.");
                Console.ReadLine();
                GetUserInput();
            }

            if (!Directory.Exists(_folderLocation))
            {
                Console.WriteLine("Sorry, there is no such location. Press return to try again.");
                Console.ReadLine();
                GetUserInput();
            }

            files = Directory.GetFiles(_folderLocation);            

            if(files.Count() == 0)
            {
                Console.WriteLine("Sorry, there are no files in that location. Press return to try again.");
                Console.ReadLine();
                GetUserInput();
            }
        }

        public void ProceedWithOption(ToolOptions option)
        {
            foreach(string filelocation in files)
            {
                CSV csv = new CSV(filelocation);
                csv.ProceedWithOption(option);
            }
        }

        public void Finish()
        {
            Console.WriteLine("Press return to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }        
    }
}