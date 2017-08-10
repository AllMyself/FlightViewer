using System.Runtime.InteropServices;

namespace BinHong.FlightViewerCore
{
    public enum WaitStatusA429
    {
        BHT_L1_OPT_WAIT_FOREVER = -1,
        BHT_L1_OPT_NOWAIT = 0
    };

    public enum AbleStatusA429
    {
        BHT_L1_OPT_DISABLE = 0,
        BHT_L1_OPT_ENABLE = 1
    };

    /// <summary>
    /// bht_L1_param_opt_e
    /// </summary>
    public enum ParamOptionA429
    {
        BHT_L1_PARAM_OPT_GET = 0,
        BHT_L1_PARAM_OPT_SET = 1
    };

    /// <summary>
    /// bht_L1_a429_slope_e
    /// </summary>
    public enum SlopeA429
    {
        BHT_L1_A429_SLOPE_10_US = 0,
        BHT_L1_A429_SLOPE_1_5_US = 1
    };

    /// <summary>
    /// A429ChannelWorkMode
    /// </summary>
    public enum ChannelWorkModeA429
    {
        A429ChannelWorkModeNABLE = 3,
        BHT_L1_A429_CHAN_WORK_MODE_STOP = 2,
        BHT_L1_A429_CHAN_WORK_MODE_STOP_AND_CLEAR = 1,
        BHT_L1_A429_CHAN_WORK_MODE_CLOSE_AND_CLEAR = 0,
    };

    /// <summary>
    /// A429Baud
    /// </summary>
    public enum BaudA429
    {
        BHT_L1_A429_BAUD_12_5K = 12500,
        BHT_L1_A429_BAUD_50K = 50000,
        BHT_L1_A429_BAUD_100K = 100000
    };

    /// <summary>
    /// A429Parity
    /// </summary>
    public enum ParityA429
    {
        BHT_L1_A429_PARITY_ODD = 0,
        BHT_L1_A429_PARITY_EVEN = 1,
        BHT_L1_A429_PARITY_NONE = 2,
    };

    /// <summary>
    /// A429WorkBit
    /// </summary>
    public enum WorkBitA429
    {
        BHT_L1_A429_WORD_BIT32 = 0,
        BHT_L1_A429_WORD_BIT31 = 1,
        BHT_L1_A429_WORD_BIT33 = 2,
    };

    /// <summary>
    /// A429Gap
    /// </summary>
    public enum GapA429
    {
        BHT_L1_A429_GAP_4BIT = 0,
        BHT_L1_A429_GAP_2BIT = 1,
    };

    /// <summary>
    /// A429RecvMode
    /// </summary>
    public enum RecvModeA429
    {
        BHT_L1_A429_RECV_MODE_LIST = 0,
        BHT_L1_A429_RECV_MODE_SAMPLE = 1,
    };

    public enum SendOptA429
    {
        BHT_L1_A429_OPT_RANDOM_SEND,
        BHT_L1_A429_OPT_PERIOD_SEND_UPDATE,
        BHT_L1_A429_OPT_PERIOD_SEND_START,
        BHT_L1_A429_OPT_PERIOD_SEND_STOP
    };

    /// <summary>
    /// bht_L1_a429_chan_param_t
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class ChannelParamA429
    {
        public ChannelWorkModeA429 work_mode;
        public BaudA429 baud;
        public ParityA429 par;
    };

    /// <summary>
    /// bht_L1_a429_rx_chan_gather_param_t
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class ChannelGatherParamA429Rx
    {
        public uint gather_enable; /* 采集模式使能 */
        public RecvModeA429 recv_mode;
        public ushort threshold_count; /* 0-16 */
        public ushort threshold_time; /* 0-16, 单位100us */
    };

    /// <summary>
    /// bht_L1_a429_tx_chan_inject_param_t
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class ChannelInjectParamA429Tx
    {
        public WorkBitA429 tb_bits;
        public GapA429 tb_gap;
        public uint tb_par_en;
    };

    public enum FilterModeA429Rx
    {
        BHT_L1_A429_FILTER_MODE_BLACKLIST = 0,
        BHT_L1_A429_FILTER_MODE_WHITELIST = 1
    };

    /// <summary>
    /// bht_L1_a429_rx_chan_filter_param_t
    /// </summary>
    //[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
    //public class ChannelFilterParamA429Rx
    //{
    //    [FieldOffset(0)]
    //    public FilterModeA429Rx filterMode;/* black list mode or white list mode */
    //    [FieldOffset(32)]
    //    public ushort reg16;
    //    [FieldOffset(32)] //private byte label;
    //    public byte label;
    //    /// <summary>
    //    /// Sdi标识label的第1,2位。对应原结构中的位段 bht_L0_u8 sdi  : 2;             /* Source/Destination Identifier */ 
    //    /// </summary>
    //    public byte Sdi
    //    {
    //        get { return (byte)(label & 0xC0); }
    //        set { label = (byte)((label & 0x3F) | (value & 0xC0)); }
    //    }

    //    /// <summary>
    //    /// Sdi标识label的第3,4位。对应原结构中的位段bht_L0_u8 ssm  : 2;             /* Sign/Status Matrix */
    //    /// </summary>
    //    public byte Ssm
    //    {
    //        get { return (byte)(label & 0x30); }
    //        set { label = (byte)((label & 0xCF) | (value & 0x30)); }
    //    }

    //    /// <summary>
    //    /// Sdi标识label的5,6,7,8位。对应原结构中的位段 bht_L0_u8 revs : 4;             /* reserve bits*/
    //    /// </summary>
    //    public byte Revs
    //    {
    //        get { return (byte)(label & 0x0F); }
    //        set { label = (byte)((label & 0xF0) | (value & 0x0F)); }
    //    }
    //};
    //by mayu 2017/7/21
    //[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
    //public class ChannelFilterParamA429Rx
    //{
    //    [FieldOffset(0)]
    //    public FilterModeA429Rx filterMode;/* black list mode or white list mode */
    //    [FieldOffset(32)]
    //    public ushort reg16;
    //    [FieldOffset(32)]
    //    private ushort filLabel;
    //    /// <summary>
    //    /// Sdi标识label的第1,2位。对应原结构中的位段 bht_L0_u8 sdi  : 2;             /* Source/Destination Identifier */ 
    //    /// </summary>
    //    public ushort Sdi
    //    {
    //        get { return (ushort)(filLabel & 0x0300); }
    //        set { filLabel = (ushort)((filLabel & 0x0CFF) | ((value << 8) & 0x0300)); }
    //    }

    //    /// <summary>
    //    /// Sdi标识label的第3,4位。对应原结构中的位段bht_L0_u8 ssm  : 2;             /* Sign/Status Matrix */
    //    /// </summary>
    //    public ushort Ssm
    //    {
    //        get { return (ushort)(filLabel & 0x0C00); }
    //        set { filLabel = (ushort)((filLabel & 0x03FF) | ((value << 10) & 0x0C00)); }
    //    }

    //    public ushort label
    //    {
    //        get { return (ushort)(filLabel & 0x00FF); }
    //        set { filLabel = (ushort)((filLabel & 0xFF00) | value & 0x00FF); }
    //    }
    //};
    [StructLayout(LayoutKind.Sequential)]
    public class ChannelFilterParamA429Rx
    {
        public uint filterMode;
        public uint label;
        public uint sdi;
        public uint ssm;
    }

    /// <summary>
    /// bht_L1_a429_rxp_t
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class RxpA429
    {
        public uint timestamp;
        public uint data;
    };

    [StructLayout(LayoutKind.Sequential)]
    public class MibDataA429
    {
        public uint cnt; /* mib - recv or send word count*/
        public uint err_cnt; /* mib - recv or send error word count*/
    }
}
