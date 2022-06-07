namespace csv_tools{

    public class MainMenu{
        public void Display()
        {
            Console.WriteLine("Hi! Please make a selection below and press return: ");
            Console.WriteLine("=================================================");
            Console.WriteLine("[1] Convert Dates from US to UK");
            Console.WriteLine("[2] Add the Trip Duration as a new column");
            Console.WriteLine("[3] Convert decimal numbers in quotes to integers");
            Console.WriteLine("[4] Exit");

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
            FileMenu fm = new FileMenu(ToolOptions.NoneSelected);
            switch(trimmed)
            {
                case "1":
                    fm = new FileMenu(ToolOptions.ConvertDates);
                    break;
                case "2":
                    fm = new FileMenu(ToolOptions.AddTripDuration);
                    break;
                case "3":
                    fm = new FileMenu(ToolOptions.ConvertDecimals);
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Input not recognized. Please try again or press 4 to exit.");
                    GetUserInput();
                    break;
            }

            if(fm.Option != ToolOptions.NoneSelected)
                fm.Display();
        }
    }
}