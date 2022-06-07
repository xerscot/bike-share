namespace csv_tools
{
    public class NullProcess : IFileProcess
    {
        public void PromptUser()
        {
            Console.WriteLine("There was a problem with this application.");
        }

        public void GetUserInput()
        {
            
        } 

        public void ProceedWithOption(ToolOptions option)
        {
            
        }

        public void Finish()
        {
            Console.WriteLine("Press return to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}