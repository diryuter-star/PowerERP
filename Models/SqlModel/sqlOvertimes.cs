using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlOvertimes : DapperSql<Overtimes>
    {
        public z_sqlOvertimes()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Overtimes.SheetNo";
            DefaultOrderByDirection = "DESC";
            DropDownValueColumn = "Overtimes.SheetNo";
            DropDownTextColumn = "Overtimes.SheetNo";
            DropDownOrderColumn = "Overtimes.SheetNo DESC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Overtimes.id, Overtimes.BaseNo, Overtimes.SheetNo, Overtimes.SheetDate, 
Overtimes.EmpNo, Overtimes.DeptNo, Overtimes.DeptName, Overtimes.ResonText, Overtimes.
TypeNo, OvertimeTypes.TypeName,
Overtimes.StartTime,Overtimes.EndTime, Overtimes.Hours, Overtimes.Remark 
FROM Overtimes 
LEFT OUTER JOIN OvertimeTypes ON Overtimes.TypeNo = OvertimeTypes.TypeNo
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("OvertimeTypes.TypeName");
            return searchColumn;
        }
    }
}