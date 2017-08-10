using System;
using System.Drawing;
using System.Windows.Forms;
using UiControls.Properties;

namespace UiControls
{
    /// <summary>
    /// 状态条
    /// </summary>
    public class StatusStrip : Panel
    {
        private readonly Label _label;
        public StatusStrip()
        {
            if (DesignMode)
            {
                return;
            }
            var pictureBox = new PictureBox();
            _label = new Label();
            Controls.Add(pictureBox);
            Controls.Add(_label);

            Name = "lblMsg";
            pictureBox.Image = Resources.Information;
            pictureBox.Location = new Point(0, 2);
            pictureBox.Name = "pictureBox1";
            pictureBox.Size = new Size(18, 18);
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox.TabIndex = 2;
            pictureBox.TabStop = false;

            _label.AutoSize = true;
            _label.Location = new Point(18, 5);
            _label.Name = "label1";
            _label.Size = new Size(83, 12);
            _label.TabIndex = 1;
            _label.Text = "即时提示信息! ";

        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _label.Text = "即时提示信息! ";
            Height = 22;
            Width = 2000;//设置一个非常大的宽度
            Dock = DockStyle.Bottom;
            BackColor = SystemColors.GradientInactiveCaption;
        }

        public void ClearMsg()
        {
            _label.Text = "即时提示信息! ";
            _label.ForeColor = Color.Black;
        }

        public void ShowErrorInfo(string info)
        {
            this.Text = info;
            this.ForeColor = Color.Red;
        }

        public void ShowWarningInfo(string info)
        {
            this.Text = info;
            this.ForeColor = Color.Orange;
        }

        public void ShowInfo(string info)
        {
            this.Text = info;
            this.ForeColor = Color.Black;
        }
        public override string Text
        {
            get { return _text; }
            set
            {
                _label.Text = value;
                _text = value;
            }
        }

        private string _text;
    }
}
