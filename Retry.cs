using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace LoginnerTest2
{
     public static class Retry
    {
        public static void Do(Action action,TimeSpan retryInterval,int retryCount = 5)        //retry count is for how many times to retry
        {                                                                                      //retryinterval is for the intervals between each retry
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }


        public static T Do<T>(Func<T> action,TimeSpan retryInterval,int retryCount = 5) //retry count is for how many times to retry
        {
            var exceptions = new List<Exception>();
            for (int retry = 0; retry < retryCount;retry++)
            {
                try
                {
                    if (retry > 0) Thread.Sleep(retryInterval);
                    return action();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    exceptions.Add(ex);
                }
            }
            
            throw new AggregateException(exceptions);

        }






    }
}
