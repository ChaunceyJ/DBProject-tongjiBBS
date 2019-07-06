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
    public class USER_RELATION
    {
        /// <summary>
        /// 
        /// </summary>
        public USER_RELATION()
        {
        }

        private System.String _RELATION_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String RELATION_ID { get { return this._RELATION_ID; } set { this._RELATION_ID = value; } }

        private System.String _ACTORT_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String ACTORT_ID { get { return this._ACTORT_ID; } set { this._ACTORT_ID = value; } }

        private System.String _OBJECT_ID;
        /// <summary>
        /// 
        /// </summary>
        public System.String OBJECT_ID { get { return this._OBJECT_ID; } set { this._OBJECT_ID = value; } }

        private System.Int16? _RELATION_TYPE;
        /// <summary>
        /// 
        /// </summary>
        public System.Int16? RELATION_TYPE { get { return this._RELATION_TYPE; } set { this._RELATION_TYPE = value; } }
    }
}