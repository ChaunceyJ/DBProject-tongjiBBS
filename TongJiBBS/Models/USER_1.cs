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
    public class USER_1
    {
        /// <summary>
        /// 
        /// </summary>
        public USER_1()
        {
        }

        private System.String _USER_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String USER_ID { get { return this._USER_ID; } set { this._USER_ID = value; } }

        private System.String _USER_NAME;
        /// <summary>
        /// 
        /// </summary>
        public System.String USER_NAME { get { return this._USER_NAME; } set { this._USER_NAME = value; } }

        private System.Int32? _CREDIT;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CREDIT { get { return this._CREDIT; } set { this._CREDIT = value; } }

        private System.String _PASSWORD_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String PASSWORD_1 { get { return this._PASSWORD_1; } set { this._PASSWORD_1 = value; } }

        private System.String _POTRAIT;
        /// <summary>
        /// 
        /// </summary>
        public System.String POTRAIT { get { return this._POTRAIT; } set { this._POTRAIT = value; } }

        private System.String _SCHOOL;
        /// <summary>
        /// 
        /// </summary>
        public System.String SCHOOL { get { return this._SCHOOL; } set { this._SCHOOL = value; } }

        private System.String _IDENTITY_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String IDENTITY_1 { get { return this._IDENTITY_1; } set { this._IDENTITY_1 = value; } }
    }
}