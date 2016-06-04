using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginnerTest2
{
    public class SiteSwitcher
    {
        //This is not working i have  to find a better alogrithm
        private static int No = 0;

        public static string Switcher(string check)
        {
            if (check == "https://internetlogin1.cu.edu.ng/login" && No <= 2)
            {
                No++;
                return "https://internetlogin2.cu.edu.ng/login";
            }
            else if (check == "https://internetlogin2.cu.edu.ng/login" && No <= 2)
            {
                No++;
                return "https://internetlogin1.cu.edu.ng/login";
            }

            MessageBox.Show("No More Switching");

            return check; //will add default url from config
        }
    }
}