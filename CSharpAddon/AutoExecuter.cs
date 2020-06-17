using System.Threading.Tasks;
using System;
namespace VoiceControl
{
    public class AutoExecuter
    {


        Task task;
        volatile Action repeatedAction;
        public volatile int Interval;

        public async Task Perform()
        {
            do
            {
                repeatedAction();
                await Task.Delay(Interval);
            } while (repeatedAction != null);
        }
        public void Start(Action action,int interval=300)
        {
            Interval = interval;
            repeatedAction = action;
            if (repeatedAction == null || task == null || task.IsCompleted)
            {
                task = Perform();
            }

        }
        public void Stop()
        {
            repeatedAction = null;
        }
        public AutoExecuter(Action action, int interval = 300)
        {
            Start(action, interval);
        }
        public AutoExecuter()
        {

        }
    }

}
