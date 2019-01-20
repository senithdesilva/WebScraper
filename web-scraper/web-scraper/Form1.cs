using System;
using System.Drawing;
using System.Windows.Forms;

namespace web_scraper
{
    public partial class Form1 : Form
    {
        private const string buttonNext = "Next",
            buttonGenerate = "Generate Context",
            buttonProcess = "Processing",
            mainTitleButton = "Web Scraper 1.0";
        private const string labelUrl = "Please enter the full Web URL Path",
            labelFilePath = "Please enter the file directory path to save the context",
            labelFileName = "Please enter the file name";

        private string uriAddress = string.Empty;
        private string filePath = string.Empty;
        private string fileName = string.Empty;

        private int numberOfClicks = 0;

        public Form1()
        {
            InitializeComponent();
            Text = "S. De Silva Enterprise";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ++numberOfClicks;
            switch(numberOfClicks)
            {
                case 1:
                    uriAddress = textBox.Text;
                    textBox.Text = string.Empty;
                    generateButton.Text = buttonNext;
                    labelDescription.Text = labelFilePath;
                    break;
                case 2:
                    filePath = textBox.Text;
                    textBox.Text = string.Empty;
                    labelDescription.Text = labelFileName;
                    generateButton.Text = buttonGenerate;
                
                    break;
                case 3:
                    fileName = textBox.Text;
                    generateButton.Text = buttonProcess;

                    if (!string.IsNullOrEmpty(uriAddress) && !string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(fileName))
                    {
                        WebScraper.Scraper(uriAddress, filePath, fileName);
                        Replay();
                    }
                    else
                    {
                        string message = "Empty or Null Input box. Do you want to try again?";
                        string caption = "Error Detected";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;

                        // Displays the MessageBox.
                        result = MessageBox.Show(message, caption, buttons);

                        if (result.Equals(DialogResult.Yes))
                        {
                            Hide();
                            ReopenApplication();
                        }
                        else
                        {
                            Close();
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetDefaultProperties();
        }

        private void SetDefaultProperties()
        {
            generateButton.Text = buttonNext;

            textBox.Text = string.Empty;

            mainLabel.Text = mainTitleButton;
            mainLabel.TextAlign = ContentAlignment.MiddleCenter;
            mainLabel.AutoSize = true;

            labelDescription.Text = labelUrl;
            labelDescription.TextAlign = ContentAlignment.MiddleCenter;
            labelDescription.AutoSize = true;
        }

        private void Replay()
        {
            string message = "Context Successfully Generated!\n" + "Do you want to Web Scrap again?";
            string caption = "Generation Log";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

            if (result.Equals(System.Windows.Forms.DialogResult.Yes))
            {
                Hide();
                ReopenApplication();
            }
            else
            {
                Close();
            }
        }

        private void ReopenApplication()
        {
            Form1 openForm = new Form1();
            openForm.ShowDialog();
        }
    }
}
