namespace csv_tools{
    public interface IFileProcess{
        void PromptUser();
        void GetUserInput();
        void ProceedWithOption(ToolOptions option);
        void Finish();
    }
}