//********************************************************
//建立日期:2015.11.26
//作者:litao
//內容说明:　保存文件线程，这个线程处理
//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BinHong.Utilities
{
    /// <summary>
    /// 将要保存的文件
    /// </summary>
    public class SavingFile
    {
        /// <summary>
        /// 次数记录
        /// </summary>
        private int _count = 0;

        /// <summary>
        /// 文件保存的时候的异步锁
        /// 异步操作SaveQueue的锁
        /// 因为写数据的过程可能有多个线程同时写，所以需要这个锁。同时写和读也是异步的，更需要这个锁
        /// </summary>
        public readonly object SyncLocker = new object();

        /// <summary>
        /// 要保存的数据 
        /// 缓存队列。
        /// <remarks>数据每隔50ms保存到文件一次的，但是在50ms内可能已经写了多条数据，SaveQueue就是用来保存50ms期间
        /// 产生的数据，大小设置成100条。如果50ms内产生的数据数超过了100条，会造成先入的数据丢失。这里不考虑，因为系统不会有这么快的数据
        /// 生成速度。另外如果系统发生异常，应该不会丢失这50ms的数据，具体可能还要论证一下</remarks>
        /// </summary>
        public readonly RingBuffer<object> SaveQueue = new RingBuffer<object>(100);

        /// <summary>
        /// 文件保存的时间间隔（ms）
        /// </summary>
        public int TimeInterval = 100;

        /// <summary>
        /// 文件要保存的路径
        /// </summary>
        public string FilePath;

        /// <summary>
        /// 检测是否到（超过）保存间隔，是否可以保存。在文件线程中调用这方法
        /// </summary>
        public bool CanSave()
        {
            _count++;
            if (_count*50 >= TimeInterval)
            {
                _count = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 保存某一条数据后的行为
        /// </summary>
        public virtual void AfterSave(object item)
        {

        }
    }

    /// <summary>
    /// 保存文件的线程
    /// </summary>
    public static class SaveFileThread
    {
        /// <summary>
        /// 文件处理的线程
        /// </summary>
        private static Thread _fileThread;

        /// <summary>
        /// 文件处理的线程的AutoResetEvent事件
        /// </summary>
        private static AutoResetEvent _fileThreadAutoReset;

        /// <summary>
        /// 文件线程状态（0：未初始化，1：已经启动，-1：已经关闭）
        /// </summary>
        private static int _fileThreadState;

        /// <summary>
        /// 文件列表
        /// </summary>
        private static readonly List<SavingFile> Files = new List<SavingFile>();

        /// <summary>
        /// 线程执行函数
        /// </summary>
        private static void LogThreadProc()
        {
            try
            {
                do
                {
                    foreach (SavingFile fileSave in Files)
                    {
                        //没有数据，或者还没有到保存时间就continue
                        if (fileSave.SaveQueue.IsEmpty
                            || !fileSave.CanSave())
                        {
                            continue;
                        }

                        //保存数据
                        using (StreamWriter writer = new StreamWriter(fileSave.FilePath, true))
                        {
                            lock (fileSave.SyncLocker)
                            {
                                while (!fileSave.SaveQueue.IsEmpty)
                                {
                                    Object item = null;
                                    fileSave.SaveQueue.Read(out item);
                                    if (item == null)
                                    {
                                        continue;
                                    }
                                    fileSave.AfterSave(item);

                                    writer.WriteLine(item);
                                }
                            }
                            writer.Flush();
                        }
                    }

                    //文件线程的状态是停止，就停止文件线程
                    if (_fileThreadState == -1)
                    {
                        _fileThread.Abort();
                    }
                } while (!_fileThreadAutoReset.WaitOne(50));
            }
            catch (Exception exception)
            {
                Debug.WriteLine("线程异常，被强制结束：" + exception.Message + Environment.NewLine + exception.StackTrace);
            }
        }

        /// <summary>
        /// 添加一个文件到保存线程中
        /// </summary>
        public static void AddFileToSaveThread(SavingFile file)
        {
            Files.Add(file);
        }

        /// <summary>
        /// 线程启动
        /// </summary>
        public static void Start()
        {
            if (_fileThreadState == 1)
            {
                return;
            }

            //线程相关操作
            _fileThreadAutoReset = new AutoResetEvent(false);
            _fileThread = new Thread(new ThreadStart(LogThreadProc));
            _fileThread.IsBackground = true;
            _fileThread.Start();
            _fileThreadState = 1;
        }

        /// <summary>
        /// Close
        /// </summary>
        public static void Close()
        {
            if (_fileThread.IsAlive)
            {
                _fileThreadAutoReset.Set();
                _fileThreadState = -1;
                _fileThread.Join();
                _fileThreadAutoReset.Close();
            }
        }
    }
}
