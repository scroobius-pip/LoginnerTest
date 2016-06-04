using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginnerTest2
{

    
    public partial class Guest_Acces : Form
    {
        public delegate void EventHandler();
        public event EventHandler onFormClosed;

        private string Url;
        public  Guest_Acces()
        {
            InitializeComponent();
        }

        public string Url1
        {
            get
            {
                return Url;
            }

            set
            {
                Url = value;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           
        }

        private void Guest_Acces_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (onFormClosed != null)
            {
                onFormClosed();
            }
        }
    }
}
