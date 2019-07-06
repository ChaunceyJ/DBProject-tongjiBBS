using SqlSugar;
using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace TongJiBBS.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class REPORT
    {
        /// <summary>
        /// 
        /// </summary>
        public REPORT()
        {
        }

        private System.String _REPORT_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String REPORT_ID { get { return this._REPORT_ID; } set { this._REPORT_ID = value; } }

        private System.String _POST_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String POST_ID { get { return this._POST_ID; } set { this._POST_ID = value; } }

        private System.String _ACTOR_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ACTOR_ID { get { return this._ACTOR_ID; } set { this._ACTOR_ID = value; } }

        private System.String _REASON;
        /// <summary>
        /// 
        /// </summary>
        public System.String REASON { get { return this._REASON; } set { this._REASON = value; } }
    }
}