using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace LoginnerTest2
{
  public  class Retry2
    {
        public delegate void NameResolutionerror();
        public event NameResolutionerror OnResolutionError;

        public  void ExecuteWithRetry (Action action)
        {
            const int maxRetries = 5;
            bool done = false;
            int attempts = 0;

            while (!done)
            {

                attempts++;
                try
                {
                    action();
                    done = true;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                    {
                        if (OnResolutionError != null)
                        {
                            OnResolutionError();
                        }
                    }

                    if (!isRetryable (ex))  //When webexception should not be retried
                    {
                        MessageBox.Show(ex.ToString());
                        MessageBox.Show("Not retryable");
                       // throw;
                    }
                    if (attempts >= maxRetries) //when maxretries = attempts
                    {
                        MessageBox.Show("exceeded");
                      //  throw;
                    }
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show(SleepTime(attempts).ToString());
                    Thread.Sleep(SleepTime(attempts));

                }
            }



        }




        private static int SleepTime(int retryCount)
        {

            switch(retryCount)
            {
                case 0: return 0;
                case 1: return 1000;
                case 2: return 5000;
                case 3: return 10000;
                default: return 30000;     
            }
        }

        private static bool isRetryable (WebException ex)
        {
            return
                ex.Status == WebExceptionStatus.ReceiveFailure || ex.Status == WebExceptionStatus.ConnectFailure || ex.Status == WebExceptionStatus.KeepAliveFailure;

        }
    }

   
     
   
}
