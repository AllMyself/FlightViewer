namespace BinHong.FlightViewerCore
{
    public interface ILabel429Info 
    {
        /// <summary>
        /// 一个32位的真实值
        /// </summary>
        int ActualValue { get; set; }

        /// <summary>
        /// 标号（1~8）
        /// </summary>
        int Label { get; set; }

        /// <summary>
        /// SDI(9~10)
        /// </summary>
        int SDI { get; set; }

        /// <summary>
        /// Data(11~29)
        /// </summary>
        int Data { get; set; }

        /// <summary>
        /// 符号位
        /// </summary>
        int SymbolState { get; set; }

        /// <summary>
        /// 奇偶校验位
        /// </summary>
        int Parity { get; set; }
    }
}
