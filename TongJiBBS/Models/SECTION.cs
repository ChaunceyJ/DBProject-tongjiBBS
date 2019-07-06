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
    public class SECTION
    {
        /// <summary>
        /// 
        /// </summary>
        public SECTION()
        {
        }

        private System.String _SECTION_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String SECTION_ID { get { return this._SECTION_ID; } set { this._SECTION_ID = value; } }

        private System.String _ADMIN_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ADMIN_ID { get { return this._ADMIN_ID; } set { this._ADMIN_ID = value; } }

        private System.String _SECTION_NAME;
        /// <summary>
        /// 
        /// </summary>
        public System.String SECTION_NAME { get { return this._SECTION_NAME; } set { this._SECTION_NAME = value; } }
    }
}