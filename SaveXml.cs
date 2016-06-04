using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LoginnerTest2
{
    internal class SaveXml
    {
        private TextBox user;
        private MaskedTextBox pass;
        private ComboBox url;

        private string Filename;

        public TextBox User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public MaskedTextBox Pass
        {
            get
            {
                return pass;
            }

            set
            {
                pass = value;
            }
        }

        public ComboBox Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public void SaveData(Object obj, string filename)
        {
            Filename = filename;

            XmlSerializer sr = new XmlSerializer(obj.GetType());

            TextWriter writer = new StreamWriter(filename);

            sr.Serialize(writer, obj);

            writer.Close();

            MessageBox.Show("Save Data");
        }

        public void InputInfo()
        {
            try
            {
                Information info = new Information();
                info.Data1 = User.Text;
                info.Data2 = Pass.Text;
                info.Data3 = Url.Text;

                SaveData(info, "info.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Loaddata()
        {
            if (File.Exists("info.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("info.xml", FileMode.Open, FileAccess.ReadWrite);
                Information info = (Information)xs.Deserialize(read);
                User.Text = info.Data1;
                Pass.Text = info.Data2;
                //     Url.Text = info.Data3;
            }
        }
    }
}