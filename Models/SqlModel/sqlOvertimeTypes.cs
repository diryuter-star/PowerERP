using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlOvertimeTypes : DapperSql<OvertimeTypes>
    {
        public z_sqlOvertimeTypes()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "OvertimeTypes.TypeNo,OvertimeTypes.StartHour";
            DefaultOrderByDirection = "ASC,ASC";
            DropDownValueColumn = "OvertimeTypes.TypeNo";
            DropDownTextColumn = "OvertimeTypes.TypeName,OvertimeTypes.StartHour";
            DropDownOrderColumn = "OvertimeTypes.TypeNo ASC , OvertimeTypes.StartHour ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}
