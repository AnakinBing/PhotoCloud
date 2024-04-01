using System.Text;

namespace PhotoCloud.Utility
{
    public class Log
    {
        static object lockerException = new object();
        static object lockerData = new object();
        private static string epath = "\\Exception\\";
        private static string dpath = "\\Data\\";
        public static string StartupPath = AppContext.BaseDirectory;
        public static string LogName = "";
        private static string EPath { get { return StartupPath + epath + DateTime.Now.ToString("yyyy_MM_dd") + (LogName == "" ? "" : ("_" + LogName)) + ".log"; } }
        private static string DPath { get { return StartupPath + dpath + DateTime.Now.ToString("yyyy_MM_dd") + (LogName == "" ? "" : ("_" + LogName)) + ".log"; } }
        private static List<string> listData = new List<string>();
        private static Thread tData;
        private static object locker = new object();

        static Log()
        {
            tData = new Thread(t_Data);
            tData.IsBackground = true;
            tData.Start();
        }

        public static void WriteException(Exception e)
        {
            lock (lockerException)
            {
                try
                {
                    CreateEDirectory();
                    using (StreamWriter sw = new StreamWriter(EPath, true))
                    {
                        sw.AutoFlush = true;
                        string time = DateTime.Now.ToString("HH:mm:ss");
                        sw.WriteLine("[" + time + "] " + e.Message);
                        if (e.InnerException != null)
                            sw.WriteLine(e.InnerException.Message);
                        sw.WriteLine(e.InnerException == null ? e.StackTrace : e.InnerException.StackTrace);
                        sw.Close();
                    }
                }
                catch { }
            }
        }

        public static void Write(object v) { lock (listData) listData.Add(v + ""); }

        public static void WriteHex(byte[] data, int start, int count) { Write(BitConverter.ToString(data, start, count)); }

        public static void Write(byte[] data, int index, int count) { Write(Encoding.Default.GetString(data, index, count)); }

        static void t_Data()
        {
            while (true)
            {
                try
                {
                    lock (listData)
                    {
                        if (listData.Count > 0)
                        {
                            CreateDDirectory();
                            StreamWriter sw = new StreamWriter(DPath, true);
                            sw.AutoFlush = true;
                            for (int i = 0; i < listData.Count; i++)
                            {
                                string time = DateTime.Now.ToString("HH:mm:ss.fff") + " ";
                                sw.WriteLine(time + listData[i]);
                                Console.WriteLine(time + listData[i]);
                            }
                            sw.Close();
                            listData.Clear();
                        }
                    }
                }
                catch { }
                Thread.CurrentThread.Join(1);
            }
        }

        public static void WriteBinary(byte[] buffer, string fileName)
        {
            string FILE_NAME = fileName;
            lock (locker)
            {
                FileStream fs = new FileStream(FILE_NAME, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                Console.WriteLine(fs.Length);
                bw.Seek((int)fs.Length, SeekOrigin.Begin);
                bw.Write(buffer);
                bw.Close();
                fs.Close();
            }
        }

        public static void WriteBinary(short[] buffer, string fileName)
        {
            string FILE_NAME = fileName;

            var bytes = new byte[buffer.Length * 2];
            Buffer.BlockCopy(buffer, 0, bytes, 0, bytes.Length);
            lock (locker)
            {
                FileStream fs = new FileStream(FILE_NAME, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fs);
                Console.WriteLine(fs.Length);
                bw.Seek((int)fs.Length, SeekOrigin.Begin);
                bw.Write(bytes);
                bw.Close();
                fs.Close();
            }
        }

        static void CreateEDirectory() { if (!Directory.Exists(StartupPath + epath)) Directory.CreateDirectory(StartupPath + epath); }

        static void CreateDDirectory() { if (!Directory.Exists(StartupPath + dpath)) Directory.CreateDirectory(StartupPath + dpath); }
    }
}
