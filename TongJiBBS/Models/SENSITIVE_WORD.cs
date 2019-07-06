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
    public class SENSITIVE_WORD
    {
        /// <summary>
        /// 
        /// </summary>
        public SENSITIVE_WORD()
        {
        }

        private System.String _TEXT;
        /// <summary>
        /// 
        /// </summary>
        public System.String TEXT { get { return this._TEXT; } set { this._TEXT = value; } }

        private System.String _TYPE_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String TYPE_1 { get { return this._TYPE_1; } set { this._TYPE_1 = value; } }
    }
}