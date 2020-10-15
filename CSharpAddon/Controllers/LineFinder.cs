using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace VoiceControl
{
    public class LineFinder
    {
        public string GetClipboard()
        {
            string test = null;
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        test = Clipboard.GetText();
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return test;
        }
        public void SetClipboard(string value)
        {
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        Clipboard.SetText(value);
                    }

                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }
        public string GetLine()
        {
            SendKeys.SendWait("^{c}");
            var test = GetClipboard();
            Console.WriteLine(test);
            return test;
        }
        public string CutLine()
        {
            SendKeys.SendWait("^{x}");
            var test = GetClipboard();
            Console.WriteLine(test);
            return test;
        }
        public void PasteLine(string value)
        {
            SetClipboard(value);
            SendKeys.SendWait("^{v}");
        }
        public bool FindLine(Func<string, List<string>> action)
        {
            SendKeys.SendWait("{right}");
            List<string> test = null;
            for (int i = 0; i < 99; i++)
            {
                test = action(GetLine());
                if (test.Count != 0) return true;
                SendKeys.SendWait("{up}");
            }
            return false;

        }
        public bool FindWord(Func<string, List<string>> action)
        {
            string test = null;
            SendKeys.SendWait("{HOME}");

            for (int i = 0; i < 99; i++)
            {
                SendKeys.SendWait("^+{right}");
                test = GetLine();
                if (action(test).Count != 0) return true;
                if (test.Contains("\n")) return false;
                SendKeys.SendWait("{right}");
            }
            return false;

        }
    }
}
