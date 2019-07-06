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
    public class PRIVATE_LETTER
    {
        /// <summary>
        /// 
        /// </summary>
        public PRIVATE_LETTER()
        {
        }

        private System.String _LETTER_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String LETTER_ID { get { return this._LETTER_ID; } set { this._LETTER_ID = value; } }

        private System.String _SENDER_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String SENDER_ID { get { return this._SENDER_ID; } set { this._SENDER_ID = value; } }

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

        private System.String _CONTENT_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String CONTENT_1 { get { return this._CONTENT_1; } set { this._CONTENT_1 = value; } }
    }
}