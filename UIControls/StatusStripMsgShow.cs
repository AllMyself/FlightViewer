using System;

namespace UiControls
{
    /// <summary>
    /// 辅助StatusStrip的消息显示
    /// </summary>
    public class StatusStripMsgShow
    {
        private event Action _clearAction;

        private event Action<string> _showErrorAction;

        private event Action<string> _showWarningAction;

        private event Action<string> _showInfoAction;

        public void Initialize(Action clearAction, Action<string> showErrorAction, Action<string> showWarningAction, Action<string> showInfoAction)
        {
            _clearAction = clearAction;
            _showErrorAction = showErrorAction;
            _showWarningAction = showWarningAction;
            _showInfoAction = showInfoAction;
        }

        public void Uninitialize()
        {
            Delegate[] delegates = _clearAction.GetInvocationList();
            foreach (var item in delegates)
            {
                Delegate.RemoveAll(_clearAction, item);
            }
            delegates = _showErrorAction.GetInvocationList();
            foreach (var item in delegates)
            {
                Delegate.RemoveAll(_showErrorAction, item);
            }
            delegates = _showWarningAction.GetInvocationList();
            foreach (var item in delegates)
            {
                Delegate.RemoveAll(_showWarningAction, item);
            }
            delegates = _showInfoAction.GetInvocationList();
            foreach (var item in delegates)
            {
                Delegate.RemoveAll(_showInfoAction, item);
            }
        }

       /// <summary>
       /// 显示错误
       /// </summary>
        public void ShowError(string msg)
       {
           Action<string> handle = _showErrorAction;
           if (handle != null)
           {
               handle(msg);
           }
       }

        /// <summary>
        /// 显示警告
        /// </summary>
        public void ShowWarning(string msg)
        {
            Action<string> handle =_showWarningAction;
            if (handle != null)
            {
                handle(msg);
            }
        }

        /// <summary>
        /// 显示普通的信息
        /// </summary>
        public void ShowInfo(string msg)
        {
            Action<string> handle = _showInfoAction;
            if (handle != null)
            {
                handle(msg);
            }
        }

        public void Clear()
        {
            Action handle = _clearAction;
            if (handle != null)
            {
                handle();
            }
        }
    }
}
