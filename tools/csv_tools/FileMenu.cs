namespace csv_tools{
    public class FileMenu{
        private ToolOptions _option = ToolOptions.NoneSelected;
        public ToolOptions Option {
            get { return _option; }
            set { _option = value; }
        }

        public FileMenu(ToolOptions option)
        {
            Option = option;
        }

        public void Display()
        {
            Console.WriteLine("Will you be converting a single file or a folder of files? ");
            Console.WriteLine("=================================================");
            Console.WriteLine("[1] Single File");
            Console.WriteLine("[2] Folder of Files");        
            Console.WriteLine("[3] Exit");

            GetUserInput();
        }

        private void GetUserInput()
        {
            string? input = Console.ReadLine();

            input = input ?? "";

            ProcessInput(input!);
        }

        private void ProcessInput(string input)
        {
            string trimmed = input.Trim();
            switch(trimmed)
            {
                case "1":
                    Process("FileModel");
                    break;
                case "2":
                    Process("FolderModel");
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Input not recognized. Please try again or press 3 to exit.");
                    GetUserInput();
                    break;
            }
        }

        private void Process(string name)
        {
            FileProcessFactory factory = new FileProcessFactory();
            IFileProcess process = factory.CreateInstance(name);

            process.PromptUser();
            process.GetUserInput();
            process.ProceedWithOption(Option);
            process.Finish();
        }
    }
}