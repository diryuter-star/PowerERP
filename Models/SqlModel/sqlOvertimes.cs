using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PowerERP.Models;


namespace PowerERP.Models;

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
Overtimes.EmpNo, Employees.EmpName, Overtimes.DeptNo, Overtimes.DeptName, Overtimes.ResonText, Overtimes.
TypeNo, vi_CodeOvertime.CodeName AS TypeName ,Overtimes.StartTime,Overtimes.EndTime,
Overtimes.Hours, Overtimes.Remark 
FROM Overtimes 
LEFT OUTER JOIN vi_CodeOvertime ON vi_CodeOvertime.CodeNo = Overtimes.TypeNo
LEFT OUTER JOIN Employees ON Overtimes.EmpNo = Employees.EmpNo
";
        return str_query;
    }

    public override List<string> GetSearchColumns()
    {
        List<string> searchColumn;
        searchColumn = dpr.GetStringColumnList(EntityObject);
        searchColumn.Add("vi_CodeOvertime.CodeName");
        return searchColumn;
    }

    public List<int> GetMonthHours(int year)
    {
        List<int> hoursList = new List<int>();
        string str_query = "SELECT SUM(Hours) AS Hours FROM Overtimes WHERE YEAR(SheetDate) = @year AND Month(SheetDate) = ";
        var parm = new DynamicParameters();
        parm.Add("@year", year);
        for (int i = 1; i <= 12; i++)
        {
            var month_query = str_query + i.ToString();
            var model = dpr.ReadSingle<vmUHRMP003_Hours>(month_query, parm);
            if (model != null)
                hoursList.Add(model.Hours);
            else
                hoursList.Add(0);
        }
        return hoursList;
    }
}

