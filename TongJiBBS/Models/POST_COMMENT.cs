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
    public class POST_COMMENT
    {
        /// <summary>
        /// 
        /// </summary>
        public POST_COMMENT()
        {
        }

        private System.String _COMMENT_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String COMMENT_ID { get { return this._COMMENT_ID; } set { this._COMMENT_ID = value; } }

        private System.String _CONTENT_1;
        /// <summary>
        /// 
        /// </summary>
        public System.String CONTENT_1 { get { return this._CONTENT_1; } set { this._CONTENT_1 = value; } }

        private System.String _ORIGINAL_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ORIGINAL_ID { get { return this._ORIGINAL_ID; } set { this._ORIGINAL_ID = value; } }

        private System.DateTime? _TIME_1;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? TIME_1 { get { return this._TIME_1; } set { this._TIME_1 = value; } }

        private System.String _ACTOR_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ACTOR_ID { get { return this._ACTOR_ID; } set { this._ACTOR_ID = value; } }

        private System.Int16? _DELETE_FLAG;
        /// <summary>
        /// 
        /// </summary>
        public System.Int16? DELETE_FLAG { get { return this._DELETE_FLAG; } set { this._DELETE_FLAG = value; } }

        private System.String _AT_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String AT_ID { get { return this._AT_ID; } set { this._AT_ID = value; } }
    }
}