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
    public class PICTURE
    {
        /// <summary>
        /// 
        /// </summary>
        public PICTURE()
        {
        }

        private System.String _POST_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String POST_ID { get { return this._POST_ID; } set { this._POST_ID = value; } }

        private System.String _PICTURE_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String PICTURE_ID { get { return this._PICTURE_ID; } set { this._PICTURE_ID = value; } }
    }
}