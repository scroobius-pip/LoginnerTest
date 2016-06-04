using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginnerTest2
{
    internal static class VerifyPassUser
    {
        public static void VerificationAndChangingPasswords(string user, string pass)
        {
            try
            {
                MessageBox.Show("working...");
                string returndata;

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpWebRequest request;
                request = (HttpWebRequest)WebRequest.Create(new Uri("https://radsvr2.csis.int/user.php?cont=login"));
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:38.0) Gecko/20100101 Firefox/38.0";
                request.AllowAutoRedirect = true;
                request.KeepAlive = true;
                CookieContainer Cookies = new CookieContainer();
                request.CookieContainer = Cookies;

                CustomStringBuilder CustomStringBuild = new CustomStringBuilder();
                CustomStringBuild.StringToAppend1 = new[] { "username", user, "password", "", "lang", "English", "Submit=Login", "md5", StringToMd5.HMACMd5(user, pass), "url", "Y29udD1sb2dpbg%3D%3D" };
                string post = CustomStringBuild.AppendAll();
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                    writer.Write(post);
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    returndata = reader.ReadToEnd();

                System.IO.File.WriteAllText(@"\lOG.txt", returndata);

                MessageBox.Show(returndata);
                if (returndata.Contains("Wrong user name or password!"))
                {
                    MessageBox.Show("Wrong");
                }
                else if (!returndata.Contains("Wrong user name or password!"))
                {
                    MessageBox.Show("correct");
                }
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}