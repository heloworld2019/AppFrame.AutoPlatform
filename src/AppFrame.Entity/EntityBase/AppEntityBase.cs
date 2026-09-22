using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.EntityBase
{
    /// <summary>
    /// 所有仓储实体的基类
    /// </summary>
    public class AppEntityBase
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [SugarColumn(SerializeDateTimeFormat ="yyyy-MM-dd HH:mm:ss")]
        public DateTime CreateTime { get; set; }
        [SugarColumn(SerializeDateTimeFormat = "yyyy-MM-dd HH:mm:ss")]
        public DateTime UpdateTime { get; set; }
    }
}
