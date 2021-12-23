using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KKB.Treasury.TreasuryCommon.Common
{
    public abstract class DBFactory
    {
        protected string ConnectionString { get; set; }
        protected IDbConnection Connection { get; set; }
        protected IDbCommand Command { get; set; }
        protected IDbTransaction Transaction { get; set; }
        protected IDbDataParameter OutputParameter { get; set; }

        public abstract bool openConnection();
        public abstract void createCommand(string Sql, CommandType Type);
        public abstract void beginTransaction();
        public abstract void setOutputParameter(string ParamName);
        public abstract void setInputParameter(string ParamName, object Value);
        public abstract int executeNonQuery();
        public abstract object executeScalar();
        public abstract DataSet executeReader(string ParamOut, string TableSpaceName);
        public abstract void commitTransaction();
        public abstract void rollbackTransaction();
        public abstract bool closeConnection();     
    }
}
