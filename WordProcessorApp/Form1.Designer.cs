namespace WordProcessorApp
{
    partial class Form1 : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip2 = new MenuStrip();
            словарьToolStripMenuItem = new ToolStripMenuItem();
            createDictionaryButton = new ToolStripMenuItem();
            updateDictionaryButton = new ToolStripMenuItem();
            cleanDictionaryButton = new ToolStripMenuItem();
            sqlDataAdapter1 = new Microsoft.Data.SqlClient.SqlDataAdapter();
            openFileDialog1 = new OpenFileDialog();
            autoCompleteTextBox2 = new AutoCompleteTextBox();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip2
            // 
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { словарьToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Padding = new Padding(6, 3, 0, 3);
            menuStrip2.Size = new Size(800, 30);
            menuStrip2.TabIndex = 2;
            menuStrip2.Text = "menuStrip2";
            // 
            // словарьToolStripMenuItem
            // 
            словарьToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createDictionaryButton, updateDictionaryButton, cleanDictionaryButton });
            словарьToolStripMenuItem.Name = "словарьToolStripMenuItem";
            словарьToolStripMenuItem.Size = new Size(82, 24);
            словарьToolStripMenuItem.Text = "Словарь";
            // 
            // createDictionaryButton
            // 
            createDictionaryButton.Name = "createDictionaryButton";
            createDictionaryButton.Size = new Size(241, 26);
            createDictionaryButton.Text = "Создание словаря";
            createDictionaryButton.Click += createDictionaryButton_Click;
            // 
            // updateDictionaryButton
            // 
            updateDictionaryButton.Name = "updateDictionaryButton";
            updateDictionaryButton.Size = new Size(241, 26);
            updateDictionaryButton.Text = "Обновление словаря";
            updateDictionaryButton.Click += updateDictionaryButton_Click;
            // 
            // cleanDictionaryButton
            // 
            cleanDictionaryButton.Name = "cleanDictionaryButton";
            cleanDictionaryButton.Size = new Size(241, 26);
            cleanDictionaryButton.Text = "Очистить словарь";
            cleanDictionaryButton.Click += cleanDictionaryButton_Click_1;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // autoCompleteTextBox2
            // 
            autoCompleteTextBox2.Dock = DockStyle.Top;
            autoCompleteTextBox2.Location = new Point(0, 30);
            autoCompleteTextBox2.Name = "autoCompleteTextBox2";
            autoCompleteTextBox2.Size = new Size(800, 27);
            autoCompleteTextBox2.TabIndex = 4;
            autoCompleteTextBox2.Values = null;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(autoCompleteTextBox2);
            Controls.Add(menuStrip2);
            Name = "Form1";
            Text = "Текстовый процессор";
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip2;
        private Microsoft.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private ToolStripMenuItem словарьToolStripMenuItem;
        private ToolStripMenuItem createDictionaryButton;
        private ToolStripMenuItem updateDictionaryButton;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem cleanDictionaryButton;
        private AutoCompleteTextBox autoCompleteTextBox2;
    }
}