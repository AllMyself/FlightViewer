using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BinHong.FlightViewerCore;
using BinHong.FlightViewerUI.Properties;
using BinHong.FlightViewerVM;
using BinHong.Utilities;
using C1.Win.C1FlexGrid;
using UiControls;

namespace BinHong.FlightViewerUI
{
    public class RunningHistoryForm : EscForm
    {
        public RunningHistoryForm()
        {
            //设置图标
            this.Icon = Resources.PXILOGO;

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = true;
            this.Size = new Size(900, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "运行历史";
            this.SizeChanged += OnSizeChanged;
            
            _flgView = new C1FlexGrid();
            //设置样式
            //设置样式为行的高度随着内容变化而变化。
            _flgView.Styles[CellStyleEnum.Normal].WordWrap = true;
            _flgView.AllowResizing = AllowResizingEnum.Rows;
            //设置空白的样
            _flgView.AllowEditing = false;
            _flgView.BackColor = Color.White;
            _flgView.Styles.Normal.TextAlign = TextAlignEnum.CenterCenter;
            _flgView.Styles.Editor.TextAlign = TextAlignEnum.CenterCenter;
            _flgView.Styles.EmptyArea.BackColor = Color.White;
            _flgView.Styles.EmptyArea.Border.Width = 0;
            _flgView.ExtendLastCol = true;
            _flgView.SelectionMode = SelectionModeEnum.ListBox;
            this.Controls.Add(_flgView);

            Hashtable typeHashTable = new Hashtable();
            typeHashTable.Add("Information", Resources.Info);
            typeHashTable.Add("Warning", Resources.Warning);
            typeHashTable.Add("Error", Resources.Error);

            _logDataTable.Columns.Add("时间");
            _logDataTable.Columns.Add("类型");
            _logDataTable.Columns.Add("级别");
            _logDataTable.Columns.Add("内容");

            if (File.Exists(RunningLog.LogFile.FilePath))
            {
                string[] lines = File.ReadAllLines(RunningLog.LogFile.FilePath);
                foreach (var line in lines)
                {
                    LogItem item = LogItem.Parse(line);
                    if (item != null)
                    {
                        DataRow dataRow = _logDataTable.NewRow();
                        dataRow["时间"] = item.Time;
                        dataRow["类型"] = item.Type;
                        dataRow["级别"] = item.Level;
                        dataRow["内容"] = item.Text;
                        _logDataTable.Rows.Add(dataRow);
                    }
                    else if (line != "" && item == null)
                    {
                        _logDataTable.Rows[_logDataTable.Rows.Count - 1]["内容"] += "\n" + line;
                    }
                }

                //装载完内容后设置一下行的高度
                _flgView.AutoSizeRows();
            }
             
            _flgView.DataSource = _logDataTable;
            _flgView.Cols["级别"].ImageMap = typeHashTable;

            _flgView.Cols[0].AllowDragging = false;
            _flgView.Cols[0].AllowResizing = false;
            _flgView.Cols[0].Width = 0;
            _flgView.Cols[1].Width = 200;

            
            _previousCheckBox = new CheckBox();
            _previousCheckBox.AutoCheck = false;
            _previousCheckBox.Text = "显示过往日志";
            _previousCheckBox.Size = new Size(100, 23);
            _previousCheckBox.TextAlign = ContentAlignment.MiddleCenter;
            _previousCheckBox.Click += OnPreviousBtnClick;
            Controls.Add(_previousCheckBox);

            OnSizeChanged(null,null);
        }

        private readonly DataTable _logDataTable = new DataTable();
        private readonly C1FlexGrid _flgView;
        private readonly CheckBox _previousCheckBox;

        /// <summary>
        /// Size变化的时候
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnSizeChanged(object o,EventArgs e)
        {
            Size size=new Size(this.Size.Width-15,this.Size.Height-80);
            _flgView.Size = size;
            _previousCheckBox.Location = new Point(this.Size.Width - 180, this.Size.Height - 70);

            //Size变化后也要设置一下行的高度
            _flgView.AutoSizeRows();
        }

        /// <summary>
        /// 点击PreviousBtn时
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnPreviousBtnClick(object o, EventArgs e)
        {
            _previousCheckBox.Checked = !_previousCheckBox.Checked;
            try
            {
                _logDataTable.Rows.Clear();
                if (_previousCheckBox.Checked)
                {
                    string dir = Directory.GetParent(RunningLog.LogFile.FilePath).FullName;
                    string[] filePaths = Directory.GetFiles(dir, "*.his");

                    foreach (var filePath in filePaths)
                    {
                        string[] lines = File.ReadAllLines(filePath);
                        for (int index = lines.Length-1; index >-1; index--)
                        {
                            var line = lines[index];
                            LogItem item = LogItem.Parse(line);
                            if (item != null)
                            {
                                DataRow dataRow = _logDataTable.NewRow();
                                dataRow["时间"] = item.Time;
                                dataRow["类型"] = item.Type;
                                dataRow["级别"] = item.Level;
                                dataRow["内容"] = item.Text;
                                _logDataTable.Rows.Add(dataRow);
                            }
                            else if (line != "" && item == null)
                            {
                                _logDataTable.Rows[_logDataTable.Rows.Count - 1]["内容"] += "\n" + line;
                            }
                        }
                    }
                    //装载完内容后设置一下行的高度
                    _flgView.AutoSizeRows();
                }
                else
                {
                    if (!File.Exists(RunningLog.LogFile.FilePath))
                    {
                        return;
                    }
                    string[] lines = File.ReadAllLines(RunningLog.LogFile.FilePath);
                    foreach (var line in lines)
                    {
                        LogItem item = LogItem.Parse(line);
                        if (item != null)
                        {
                            DataRow dataRow = _logDataTable.NewRow();
                            dataRow["时间"] = item.Time;
                            dataRow["类型"] = item.Type;
                            dataRow["级别"] = item.Level;
                            dataRow["内容"] = item.Text;
                            _logDataTable.Rows.Add(dataRow);
                        }
                        else if (line != "" && item == null)
                        {
                            _logDataTable.Rows[_logDataTable.Rows.Count - 1]["内容"] += "\n" + line;
                        }
                    }
                    //装载完内容后设置一下行的高度
                    _flgView.AutoSizeRows();
                }
            }
            catch (Exception exception)
            {
                RunningLog.Record(LogLevel.Error, "读取日志错误，" + exception.Message);
            }
        }
    }

    /// <summary>
    /// 运行历史ListBox
    /// </summary>
    public class RunningHistoryListBox : ListBox
    {
        /// <summary>
        /// 是否自动跟踪
        /// </summary>
        private bool _autoTrack = true;

        /// <summary>
        /// 添加新条目之后的事件
        /// </summary>
        public event Action<LogItem> AfterAddNewItem;

        /// <summary>
        ///字符串格式
        /// </summary>
        private readonly StringFormat _stringFormat = new StringFormat(StringFormatFlags.LineLimit | StringFormatFlags.NoWrap);

        public RunningHistoryListBox()
        {
            RunningLog.LogFile.ShowQueue.Changed += (o1, e1) =>
            {
                if (e1.ChangeType == RingBufferChangeType.Write)
                {
                    App.Instance.BeginInvoke(
                        () =>
                        {
                            Items.Add(e1.Item);
                            if (_autoTrack)
                            {
                                this.SelectedItem = e1.Item;
                            }
                            if (AfterAddNewItem!=null)
                            {
                                AfterAddNewItem(e1.Item);
                            }
                        });
                }
            };

            _stringFormat.Alignment = StringAlignment.Near;
            _stringFormat.Trimming = StringTrimming.EllipsisCharacter;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            
            if (e.Index < 0
                || Items.Count < 1)
            {
                return;
            }

            LogItem logItem = Items[e.Index] as LogItem;
            if (logItem != null)
            {
                const char splitChar = LogItem.SplitChar;
                string runMsg = string.Format("=> {0,-16}{1}{2,-12}:{3}   {4}", logItem.Time, splitChar, logItem.Level, splitChar, logItem.Text);
                if (logItem.Level == LogLevel.Error)
                {
                    e.Graphics.DrawString(runMsg, this.Font, VmBrushes.Error, e.Bounds, _stringFormat);
                }
                else if (logItem.Level == LogLevel.Warning)
                {
                    e.Graphics.DrawString(runMsg, this.Font, VmBrushes.Warining, e.Bounds, _stringFormat);
                }
                else
                {
                    e.Graphics.DrawString(runMsg, this.Font, VmBrushes.Info, e.Bounds, _stringFormat);
                }
                e.DrawFocusRectangle();
            }
            base.OnDrawItem(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.SelectedItem = null;
            _autoTrack = true;
            base.OnLostFocus(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            RunningHistoryForm frm = new RunningHistoryForm();
            frm.ShowSingleAtCenterParent(this.FindForm());
            base.OnDoubleClick(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            _autoTrack = false;
            base.OnMouseClick(e);
        }
    }
}
