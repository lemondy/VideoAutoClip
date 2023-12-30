using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;



namespace VideoAutoClip
{
    public static class Log4Net
    {
        public static readonly ILog loginfo = LogManager.GetLogger("loginfo");
        public static readonly ILog logerror = LogManager.GetLogger("logerror");
        public static void WriteLog(string ndcInfo, string info)
        {
            NDC.Clear();
            NDC.Push(ndcInfo);
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void WriteError(string ndcInfo, string info, Exception se)
        {
            NDC.Clear();
            NDC.Push(ndcInfo);
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, se);
            }
        }
    }
}
