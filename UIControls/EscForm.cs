using System.Windows.Forms;

namespace UiControls
{
    /// <summary>
    /// 自定义的一个Form.主要用于完成Esc，关闭Form的逻辑
    /// </summary>
    public class EscForm : Form
    {
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
            }
            return false;
        }
    }
}
