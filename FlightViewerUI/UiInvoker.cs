using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.Utilities;
using CommonInterface;

namespace BinHong.FlightViewerUI
{
    public class UiInvoker:IInvoke
    {
        public void Invoke()
        {
            //下面几行暂时没有用。我的想象中，程序的打开、关闭、运行的各种时机不应该由界面Form的情况
            //来获知，（因为界面也只是程序过程的一种时机）应该由程序自身BhRuntime.Instance来响应，
            //所以有了下面几行代码。
            Form mainForm =new MainWindow();

            mainForm.Load += (o, e) =>
            {
                App.Instance.RaiseStarted();

                App.Instance.BeingInvokeEvent += mainForm.BeginInvoke;
                App.Instance.InvokeEvent += mainForm.Invoke;
                App.Instance.IsMainFormExisted += () => mainForm.IsHandleCreated;
            };
            mainForm.Closing += App.Instance.RaiseWindowClosing;
            mainForm.Closed += App.Instance.RaiseWindowClosed;

            //主线程延迟调用行为的初始化
            MainThreadDelayAction.Initialize(act =>
            {
                if (mainForm.IsHandleCreated)
                {
                    if (mainForm.InvokeRequired)
                    {
                        mainForm.BeginInvoke(act);
                        return;
                    }
                }
                act();
            });
            //运行主窗口
            Application.Run(mainForm);
        }
    }
}
