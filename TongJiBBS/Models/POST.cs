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
    public class POST
    {
        /// <summary>
        /// 
        /// </summary>
        public POST()
        {
        }

        private System.String _POST_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String POST_ID { get { return this._POST_ID; } set { this._POST_ID = value; } }

        private System.String _USER_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String USER_ID { get { return this._USER_ID; } set { this._USER_ID = value; } }

        private System.String _SECTION_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String SECTION_ID { get { return this._SECTION_ID; } set { this._SECTION_ID = value; } }

        private System.DateTime? _TIME_1;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? TIME_1 { get { return this._TIME_1; } set { this._TIME_1 = value; } }

        private System.String _TITLE;
        /// <summary>
        /// 
        /// </summary>
        public System.String TITLE { get { return this._TITLE; } set { this._TITLE = value; } }

        private System.Int16? _DELETE_FLAG;
        /// <summary>
        /// 
        /// </summary>
        public System.Int16? DELETE_FLAG { get { return this._DELETE_FLAG; } set { this._DELETE_FLAG = value; } }

        private System.String _CONTENT_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String CONTENT_1 { get { return this._CONTENT_1; } set { this._CONTENT_1 = value; } }

        private System.String _FORWARD_FROM_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String FORWARD_FROM_ID { get { return this._FORWARD_FROM_ID; } set { this._FORWARD_FROM_ID = value; } }
    }
}