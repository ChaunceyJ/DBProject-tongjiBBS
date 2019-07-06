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
    public class POST_ATTITUDE
    {
        /// <summary>
        /// 
        /// </summary>
        public POST_ATTITUDE()
        {
        }

        private System.String _ATTITUDE_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ATTITUDE_ID { get { return this._ATTITUDE_ID; } set { this._ATTITUDE_ID = value; } }

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

        private System.Int16? _ATTITUDE_TYPE;
        /// <summary>
        /// 
        /// </summary>
        public System.Int16? ATTITUDE_TYPE { get { return this._ATTITUDE_TYPE; } set { this._ATTITUDE_TYPE = value; } }

        private System.DateTime? _TIME_1;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? TIME_1 { get { return this._TIME_1; } set { this._TIME_1 = value; } }
    }
}