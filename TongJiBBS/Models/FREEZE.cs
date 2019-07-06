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
    public class FREEZE
    {
        /// <summary>
        /// 
        /// </summary>
        public FREEZE()
        {
        }

        private System.String _USER_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String USER_ID { get { return this._USER_ID; } set { this._USER_ID = value; } }

        private System.DateTime _FREEZE_START_TIME;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime FREEZE_START_TIME { get { return this._FREEZE_START_TIME; } set { this._FREEZE_START_TIME = value; } }

        private System.DateTime? _FREEZE_EN_TIME;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? FREEZE_EN_TIME { get { return this._FREEZE_EN_TIME; } set { this._FREEZE_EN_TIME = value; } }

        private System.String _FREEZE_TYPE;
        /// <summary>
        /// 
        /// </summary>
        public System.String FREEZE_TYPE { get { return this._FREEZE_TYPE; } set { this._FREEZE_TYPE = value; } }
    }
}