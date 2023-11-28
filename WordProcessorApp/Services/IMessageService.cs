namespace WordProcessorApp.Services;

public interface IMessageService
{
    void CreationMessage();
    void UpdatingMessage();
    void CleaningMessage();
    void ShowError(string message, string error);
}
