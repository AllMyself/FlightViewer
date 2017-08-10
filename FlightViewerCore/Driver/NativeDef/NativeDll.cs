namespace BinHong.FlightViewerCore
{
    public static class NativeDll
    {
#if Test
        public const string Path = ".\\RefDll\\CTest.dll";
#else
        public const string Path = ".\\RefDll\\A429WinDrv.dll";
#endif

        public const string ErrorToString = "bht_L1_error_to_string";
        public const string DeviceProbe = "bht_L1_device_probe";
        public const string DeviceRemove = "bht_L1_device_remove";
        public const string DefaultInit = "bht_L1_a429_default_init";
       
        public const string ChannelParamTx = "bht_L1_a429_tx_chan_comm_param";
        public const string ChannelMibGetTx = "bht_L1_a429_tx_chan_mib_get";
        public const string ChannelMibClearTx = "bht_L1_a429_tx_chan_mib_clear";
        public const string ChannelInjectParamTx = "bht_L1_a429_tx_chan_inject_param";
        public const string ChannelLoopTx = "bht_L1_a429_tx_chan_loop";
        public const string ChannelSendTx = "bht_L1_a429_tx_chan_send";
        public const string ChannelSlopeCfgTx = "bht_L1_a429_tx_chan_slope_cfg";
        public const string ChannelPeriodParamTx = "bht_L1_a429_tx_chan_period_param";
        
        public const string ChannelParamRx = "bht_L1_a429_rx_chan_comm_param";
        public const string ChannelGatherParam = "bht_L1_a429_rx_chan_gather_param";
        public const string ChannelMibGetRx = "bht_L1_a429_rx_chan_mib_get";
        public const string ChannelMibClearRx = "bht_L1_a429_rx_chan_mib_clear";
        public const string ChannelFilterCfgRx = "bht_L1_a429_rx_chan_filter_cfg";
        public const string ChannelRecvRx = "bht_L1_a429_rx_chan_recv";
        public const string FpgaEepromRead = "bht_L1_bd_fpga_eeprom_read";
        public const string FpgaEepromWrite = "bht_L1_bd_fpga_eeprom_write";
    }
}
