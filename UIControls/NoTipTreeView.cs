//********************************************************
//建立日期:2016.02.29
//作者:litao
//內容说明:　实现没有tip的TreeView。winform自带的TreeView在某一行数据显示不下的时候，会自动开启Tip，即是是被设置了
//          ShowNodeToolTips = false,ToolTipText="",所以只能重新它的样式。

//修改日期：
//作者:
//內容说明:
//********************************************************


using System.Windows.Forms;

namespace UiControls
{
    /// <summary>
    /// 实现没有tip的TreeView。
    /// </summary>
    public class NoTipTreeView:TreeView
    {
        private const int TVS_NOTOOLTIPS = 0x80;

        /// <summary>
        /// Disables the tooltip activity for the treenodes.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                p.Style = p.Style | TVS_NOTOOLTIPS;
                return p;
            }
        }
    }
}
