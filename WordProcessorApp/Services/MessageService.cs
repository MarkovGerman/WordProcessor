namespace WordProcessorApp.Services;

public class MessageService: IMessageService
{
    public void CreationMessage()
    {
        MessageBox.Show("Словарь создан!");
    }
    public void UpdatingMessage()
    {
        MessageBox.Show("Словарь обновлён!");
    }
    public void CleaningMessage()
    {
        MessageBox.Show("Словарь очищен!");
    }
}
