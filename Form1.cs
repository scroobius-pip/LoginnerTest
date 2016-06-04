using System;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace LoginnerTest2
{
    public partial class Form1 : Form
    {
        private Information info = new Information();

        private SaveXml xml = new SaveXml();

        private Thread t;

        private Thread s;

        //   public string urlChanged;

        public Form1()
        {
            InitializeComponent();
        }

        //   [STAThread]
        private void button2_Click(object sender, EventArgs e)
        {
            Configuration configManager = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); // Configuration Mangaer is for storing Recurring settings
            KeyValueConfigurationCollection confCollection = configManager.AppSettings.Settings; //Added a new value to configuration manager
            confCollection["LastUrlIndex"].Value = comboBox1.SelectedIndex.ToString(); //set the new value as combobox selected value
            configManager.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configManager.AppSettings.SectionInformation.Name);

            LoginBeta2 win = new LoginBeta2(); // Created a new LoginBeta2 class named win

            //Setting various LoginBeta2 Fields
            win.InvalidKey1 = "invalid username or password";
            win.InvalidKey2 = "Download limit reached!";
            win.PASSWORD_Validate1 = maskedTextBox1.Text.Trim();
            win.USERNAME_Validate1 = textBoxuser.Text.Trim();
            win.RequestMethods1 = "POST";

            string Url = comboBox1.Text.Trim();
            win.UserAgent1 = "Mozilla/5.0 (Windows NT 6.1; rv:38.0) Gecko/20100101 Firefox/38.0";
            win.OnInvalidUser += Win_OnInvalidUser;
            win.OnLoggedIn += Win_OnLoggedIn;
            Retry2 retry = new Retry2();

            t = new Thread(() =>
              win.ValidateCredentials(Url));
            // VerifyPassUser.VerificationAndChangingPasswords(textBoxuser.Text.Trim(), maskedTextBox1.Text.Trim()));
            t.Start();
            pictureBox2.Visible = true;
        }

        private void Win_OnLoggedIn()
        {
            MessageBox.Show("success");
        }

        private void Win_OnInvalidUser()
        {
            //put verifypassuser class here to verify the password is correct
            MessageBox.Show("failure do you want to verify your password?");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = Convert.ToInt32(ConfigurationManager.AppSettings["LastUrlIndex"]);
            /*  System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
              myTimer.Interval = 5000;
              myTimer.Start();
           */
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (t != null)
                t.Abort();
        }
    }
}