using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlLeavesDetail : DapperSql<LeavesDetail>
    {
        public z_sqlLeavesDetail()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "LeavesDetail.EmpNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "LeavesDetail.EmpNo";
            DropDownTextColumn = "LeavesDetail.EmpName";
            DropDownOrderColumn = "LeavesDetail.EmpNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT LeavesDetail.Id, LeavesDetail.ParentNo, LeavesDetail.EmpNo,
Employees.EmpName, LeavesDetail.TypeNo,vi_CodeLeave.CodeName AS TypeName,
LeavesDetail.StartTime, LeavesDetail.EndTime, LeavesDetail.Hours, 
LeavesDetail.ReasonText, LeavesDetail.SpecialDays, LeavesDetail.TotSpecialDays,
LeavesDetail.SumSpecialDays,LeavesDetail.Remark
FROM Employees
LEFT OUTER JOIN LeavesDetail ON Employees.EmpNo = LeavesDetail.EmpNo 
LEFT OUTER JOIN vi_CodeLeave ON LeavesDetail.TypeNo = vi_CodeLeave.CodeNo
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("vi_CodeLeave.CodeName");
            searchColumn.Add("Products.EmpName");
            return searchColumn;
        }

        public override List<LeavesDetail> GetDataList(string ParentNo)
        {
            string str_sql = GetSQLSelect();
            str_sql += "WHERE LeavesDetail.ParentNo = @ParentNo ";
            str_sql += GetSQLOrderBy();
            var parm = new DynamicParameters();
            parm.Add("@ParentNo", ParentNo);
            return dpr.ReadAll<LeavesDetail>(str_sql, parm);
        }

        public int DeleteMasterData(int id)
        {
            using var Leaves = new z_sqlLeaves();
            var LeavesModel = Leaves.GetData(id);
            string str_sql = "DELETE FROM LeavesDetail WHERE ParentNo = @ParentNo";
            var parm = new DynamicParameters();
            parm.Add("@ParentNo", LeavesModel.BaseNo);
            return dpr.Execute(str_sql, parm);
        }

    }
}
