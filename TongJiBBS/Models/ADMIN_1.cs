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
    public class ADMIN_1
    {
        /// <summary>
        /// 
        /// </summary>
        public ADMIN_1()
        {
        }

        private System.String _ADMIN_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ADMIN_ID { get { return this._ADMIN_ID; } set { this._ADMIN_ID = value; } }

        private System.String _PASSWORD_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String PASSWORD_1 { get { return this._PASSWORD_1; } set { this._PASSWORD_1 = value; } }

        private System.String _IDENTITY_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String IDENTITY_1 { get { return this._IDENTITY_1; } set { this._IDENTITY_1 = value; } }

        private System.String _PROTRAIT;
        /// <summary>
        /// 
        /// </summary>
        public System.String PROTRAIT { get { return this._PROTRAIT; } set { this._PROTRAIT = value; } }

        private System.String _NAME_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String NAME_1 { get { return this._NAME_1; } set { this._NAME_1 = value; } }
    }
}