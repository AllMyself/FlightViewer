//********************************************************
//建立日期:2015.12.17
//作者:litao
//內容说明:　ExtendMethods文件主要用于给.net库中某些类的提供方法的扩展，使之具有更为强大的功能。

//修改日期：
//作者:
//內容说明:
//********************************************************

using System.Drawing;
using System.Windows.Forms;

namespace UiControls
{
    /// <summary>
    /// Form的方法扩展
    /// </summary>
    public static class FormExtend
    {
        /// <summary>
        /// 在父窗口parent的中心Show这个childForm界面。
        /// </summary>
        public static void ShowSingleAtCenterParent(this Form childForm, Form parent)
        {
            //已经有一个，激活就是了，然后返回。
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType().Name == childForm.GetType().Name)
                {
                    form.Activate();
                    return;
                }
            }

            //显示一个窗口
            childForm.Owner = parent;
            childForm.HandleCreated += (o, e) =>
            {
                //设置这个窗口到父窗口的中心。必须在HandleCreated事件中设置它的中心位置。在Load,Shown中设置都会在默认
                //位置闪一下，然后被拉到parent的Center。
                int offset = parent.OwnedForms.Length * 10;
                childForm.Location = new Point(parent.Left + (parent.Width - childForm.Width) / 2 + offset,
                    parent.Top + (parent.Height - childForm.Height) / 2 + offset);
            };
            childForm.Show(parent);
        }
    }
}
