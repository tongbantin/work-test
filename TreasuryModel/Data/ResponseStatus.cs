using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryModel.Data
{
    public class ResponseStatus
    {
        public static string PROCESSED_SUCCESSFUL = "CWL-I-1000";
        public static string UNDER_PROCESSING = "CWL-I-1001";
        public static string DATA_NOT_FOUND = "CWL-I-1003";
        //Not Change Status
        public static string CONNECTION_TIMEOUT = "CWL-E-1904";
        public static string SOCKET_TIMEOUT = "CWL-E-1905";
        public static string MAXIMUM_QUEUE = "CWL-E-1906";
        public static string DUPLICATE_REQUEST_PROGRESS_STATUS = "CWL-E-1907";
        public static string DUPLICATE_REQUEST_UNKNOW_STATUS = "CWL-E-1908";
        public static string CANNOT_PROCESS_YOUR_REQUEST = "CWL-E-1999";
    }
}
