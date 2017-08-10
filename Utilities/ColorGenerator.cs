using System.Drawing;

namespace BinHong.Utilities
{
    /// <summary>
    /// 颜色生成器
    /// <remarks>
    ///  规则为：r,g,b分别从255按照一定数据减少，组合成不同颜色。总共能产生54中不同的颜色
    /// </remarks>
    /// </summary>
    public class ColorGenerator
    {
        /// <summary>
        /// 颜色与颜色之间相差的距离
        /// </summary>
        private const int ColorStep = 75;

        private int ColorGrade
        {
            get { return 255 / ColorStep; }
        }

        private int _curXGrade = 255 / ColorStep;
        private int _curYGrade = 255 / ColorStep;
        private int _curZGrade = 255 / ColorStep - 1;

        /// <summary>
        /// 当前面的索引。rg,rb,gb能够形成3面,_curFaceIndex%3的值分别表示不同的面：0表示rb面，1：表示rg面，2：表示gb面,
        /// </summary>
        private uint _curFaceIndex = 0;

        public int Count = 0;

        /// <summary>
        /// 获取一个颜色
        /// </summary>
        /// <returns>生成的颜色</returns>
        public Color GetOneColor()
        {
            int r = 0;
            int b = 0;
            int g = 0;

            int x = _curXGrade * ColorStep;
            int y = _curYGrade * ColorStep;
            int z = _curZGrade * ColorStep;

            //一套颜色生成的过程中会出现少量重复的颜色，但是它们之间距离差距很大，所以不再优化了。
            //会出现重复的颜色的情况之一：当z==0时，xy不为0；与z！=0，yx不等与0的，循环过程可能出现y==0的情况。
            _curXGrade--;
            if (_curXGrade == 0)
            {
                _curXGrade = ColorGrade;
                _curYGrade--;
                if (_curYGrade == 0)
                {
                    _curYGrade = ColorGrade;
                    _curFaceIndex++;
                    if (_curFaceIndex % 3 == 0)
                    {
                        _curZGrade--;
                        if (_curZGrade == 0)
                        {
                            _curZGrade = 255 / ColorStep - 1;
                            Count = 0;
                        }
                    }
                }
            }

            uint faceIndex = _curFaceIndex % 3;
            switch (faceIndex)
            {
                case 0:
                    r = x;
                    b = y;
                    g = z;
                    break;
                case 1:
                    r = x;
                    g = y;
                    b = z;
                    break;
                case 2:
                    b = x;
                    g = y;
                    r = z;
                    break;
            }
            Count++;
            return Color.FromArgb(r, g, b);
        }
    }
}
