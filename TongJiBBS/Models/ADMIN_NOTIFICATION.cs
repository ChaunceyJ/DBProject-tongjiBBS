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
    public class ADMIN_NOTIFICATION
    {
        /// <summary>
        /// 
        /// </summary>
        public ADMIN_NOTIFICATION()
        {
        }

        private System.String _NOTIFICATION_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String NOTIFICATION_ID { get { return this._NOTIFICATION_ID; } set { this._NOTIFICATION_ID = value; } }

        private System.String _RECEIVER_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String RECEIVER_ID { get { return this._RECEIVER_ID; } set { this._RECEIVER_ID = value; } }

        private System.DateTime? _TIME_1;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? TIME_1 { get { return this._TIME_1; } set { this._TIME_1 = value; } }

        private System.Int16? _NOTIFICATION_TYPE;
        /// <summary>
        /// 
        /// </summary>
        public System.Int16? NOTIFICATION_TYPE { get { return this._NOTIFICATION_TYPE; } set { this._NOTIFICATION_TYPE = value; } }

        private System.String _POST_USER_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String POST_USER_ID { get { return this._POST_USER_ID; } set { this._POST_USER_ID = value; } }
    }
}