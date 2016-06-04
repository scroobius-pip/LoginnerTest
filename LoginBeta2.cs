using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace LoginnerTest2
{
    internal class LoginBeta2
    {
        private const int maxRetries = 5;
        private int attempts = 0;
        private CookieContainer cookies = new CookieContainer();
        private bool done = false;
        private   string invalidKey; //Make Invalid key a constant retreiving values from config
        private  string invalidKey2;
        private string PASSWORD_Validate;
        private string PostData;
        //Storing cookies
        private HttpWebRequest request = null;

        private string RequestMethods;
        private HttpWebResponse response = null;
        private string ResponseUri;
        //   private string URL_Validate;
        private int retry = 0;

        private string returnData = string.Empty;
        private string stringretry;
        private string Url;
        private string UserAgent;
        private string USERNAME_Validate;
        public delegate void InvalidUser();

        public delegate void LoggedIn(); 

        public event InvalidUser OnInvalidUser;

        public event LoggedIn OnLoggedIn; //Event for Succesfull login
        //       public delegate void

        public CookieContainer Cookies
        {
            get
            {
                return cookies;
            }

            set
            {
                cookies = value;
            }
        }

        public string InvalidKey1
        {
            get
            {
                return invalidKey;
            }

            set
            {
                invalidKey = value;
            }
        }

        public string InvalidKey2
        {
            get
            {
                return invalidKey2;
            }

            set
            {
                invalidKey2 = value;
            }
        }

        public string PASSWORD_Validate1
        {
            get
            {
                return PASSWORD_Validate;
            }

            set
            {
                PASSWORD_Validate = value;
            }
        }

        public string PostData1
        {
            get
            {
                return PostData;
            }

            set
            {
                PostData = value;
            }
        }

        public string RequestMethods1
        {
            get
            {
                return RequestMethods;
            }

            set
            {
                RequestMethods = value;
            }
        }

        public string UserAgent1
        {
            get
            {
                return UserAgent;
            }

            set
            {
                UserAgent = value;
            }
        }
        public string USERNAME_Validate1
        {
            get
            {
                return USERNAME_Validate;
            }

            set
            {
                USERNAME_Validate = value;
            }
        }
        
        public void ValidateCredentials(string URL_Validate)
        {
            while (!done)
            {
                attempts++; 
                try
                {
                    //Set up the request
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; }; //Bypass Certificate Errors
                    Url = URL_Validate;
                    request = (HttpWebRequest)WebRequest.Create(new Uri(Url));
                    request.Method = RequestMethods1;
                    request.UserAgent = UserAgent1;
                    request.AllowAutoRedirect = true;
                    request.KeepAlive = true;
                    request.CookieContainer = Cookies;

                    //Format Post Data using Custom String Builder
                    CustomStringBuilder CustomStringBuilder = new CustomStringBuilder();
                    CustomStringBuilder.StringToAppend1 = new[] { "dst", "popup=true", "username", USERNAME_Validate, "password", PASSWORD_Validate };
                    string postData = CustomStringBuilder.AppendAll();

                    //Write Post Data To Server

                    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                        writer.Write(postData);

                    response = (HttpWebResponse)request.GetResponse();
                    ResponseUri = response.ResponseUri.ToString();

                    //Read the Web Page retreived after sending request

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        returnData = reader.ReadToEnd();

                    if (returnData.Contains(invalidKey)) //Invalid Username or password

                    {
                        retry++;
                        stringretry = "Retrying" + " " + retry.ToString();
                        MessageBox.Show("Invalid username or password " + stringretry);
                        done = true;

                        if (retry >= 5)
                        {
                            if (OnInvalidUser != null)
                            {
                                OnInvalidUser();
                            }
                        }


                    }
                    else if (returnData.Contains(invalidKey2)) //checks second invalid key
                    {
                        MessageBox.Show("Download limit reached!");
                        done = true;
                    }
                    else if (returnData.Contains("You are already logged in - access denied"))
                    {
                        MessageBox.Show("You are already logged in - access denied");
                        if (OnInvalidUser != null)
                        {
                            OnInvalidUser();
                        }
                        done = true;
                    }
                    else if (!returnData.Contains(InvalidKey1) && !returnData.Contains(InvalidKey2))
                    {
                        done = true;
                        MessageBox.Show(returnData);
                        //   MessageBox.Show("success")
                        if (OnLoggedIn != null)
                        {
                            OnLoggedIn();
                        }
                    }
                    else
                    {
                        done = true;
                        MessageBox.Show(returnData);
                    }
                }
                catch (WebException ex)
                {
                    if (attempts >= maxRetries)
                    {
                        MessageBox.Show("Retry Limit Exceeded");
                        return;
                    }

                    if (ex.Status == WebExceptionStatus.ConnectFailure)
                    
                    {
                    
                        //Switcher Here
                        MessageBox.Show("Switching");
                        ValidateCredentials(SiteSwitcher.Switcher(Url));
                    }

                    MessageBox.Show(ex.ToString());
                    MessageBox.Show(SleepTime(attempts).ToString());
                    Thread.Sleep(SleepTime(attempts));
                }
            }
        }

        private static int SleepTime(int retryCount)
        {
            switch (retryCount)
            {
                case 0: return 0;
                case 1: return 1000;
                case 2: return 5000;
                case 3: return 10000;
                default: return 30000;
            }
        }
    }
}