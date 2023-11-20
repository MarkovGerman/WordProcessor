using WordProcessorApp.Repositories;
using WordProcessorApp.Services;

namespace WordProcessorApp
{
    public partial class Form1 : Form
    {
        private IDictionaryService dictionaryService;
        private IMessageService messageService;

        public Form1()
        {
            dictionaryService = new DictionaryService(new Repository());
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            autoCompleteTextBox2.Values = new string[0];
            messageService = new MessageService();
        }

        private async void createDictionaryButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var path = openFileDialog1.FileName;
            await dictionaryService.CreateDictionary(path);
            messageService.CreationMessage();
        }

        private async void updateDictionaryButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var path = openFileDialog1.FileName;
            await dictionaryService.UpdateDictionary(path);
            messageService.UpdatingMessage();
        }

        private async void cleanDictionaryButton_Click_1(object sender, EventArgs e)
        {
            await dictionaryService.CleanDictionary();
            messageService.CleaningMessage();
        }
    }
}