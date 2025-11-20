using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class z_sqlLeaves : DapperSql<Leaves>
    {
        public z_sqlLeaves()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Leaves.SheetNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Leaves.SheetNo";
            DropDownTextColumn = "Leaves.SheetNo";
            DropDownOrderColumn = "Leaves.SheetNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Leaves.Id, Leaves.BaseNo, Leaves.IsConfirm, Leaves.IsCancel,
Leaves.SheetNo, Leaves.SheetDate, Leaves.HandleNo,
Users.UserName AS HandleName, Leaves.Remark
FROM Leaves
INNER JOIN Users ON Leaves.HandleNo = Users.UserNo
";
            return AddRowNoColumn(str_query);
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("Users.UserName");
            return searchColumn;
        }
        /// <summary>
        /// 設定主檔目前記錄位置
        /// </summary>
        public override void SetMasterPage()
        {
            var models = GetAllData();
            var model = GetMasterData();
            SessionService.MasterId = (model != null) ? model.Id : 0;
            SessionService.MasterNo = (model != null) ? model.SheetNo : "";
            SessionService.MasterBaseNo = (model != null) ? model.BaseNo : "";
            SessionService.PageMaster = (model != null) ? model.RowNo : 0;
            SessionService.PageCountMaster = (model != null) ? models.Count() : 0;
        }

        /// <summary>
        /// 以 BaseNo 設定目前的記錄值
        /// </summary>
        /// <param name="baseno"></param>
        public override void SetBaseNo(string baseno)
        {
            var models = GetAllData();
            var model = models.Where(x => x.BaseNo == baseno).FirstOrDefault();
            SessionService.PageMaster = model.RowNo;
            SetMasterPage();
        }

        /// <summary>
        /// 取得指定記錄 Id的單筆資料
        /// </summary>
        /// <param name="id">記錄 Id</param>
        /// <returns></returns>
        public Leaves GetMasterData(int id)
        {
            SessionService.PageMaster = id;
            var model = GetMasterData();
            SetMasterPage();
            return model;
        }

        /// <summary>
        /// 取得目前主檔資料
        /// </summary>
        /// <returns></returns>
        public Leaves GetMasterData()
        {
            var model = GetAllData().Where(x => x.RowNo == SessionService.PageMaster).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 確認單據
        /// </summary>
        /// <param name="id">單據 ID</param>
        public bool Confirm(int id)
        {
            ErrorMessage = "";
            var model = GetData(id);
            if (model == null) { ErrorMessage = "找不到指定的單據資料!!"; return false; }
            if (model.IsConfirm) { ErrorMessage = "單據已經過確認，無法重覆確認!!"; return false; }
            if (model.IsCancel) { ErrorMessage = "單據已經過作廢，無法確認!!"; return false; }

            using var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            string sql_query = $"UPDATE Leaves SET IsConfirm = @IsConfirm WHERE Leaves.Id = @Id";
            parm.Add("Id", id);
            parm.Add("IsConfirm", true);
            AffectedRows = dpr.Execute(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
            return (AffectedRows > 0);
        }

        /// <summary>
        /// 取消確認單據
        /// </summary>
        /// <param name="id">單據 ID</param>
        public bool Undo(int id)
        {
            ErrorMessage = "";
            var model = GetData(id);
            if (model == null) { ErrorMessage = "找不到指定的單據資料!!"; return false; }
            if (!model.IsConfirm) { ErrorMessage = "單據未確認，無法取消確認!!"; return false; }
            if (model.IsCancel) { ErrorMessage = "單據已經過作廢，無法確認!!"; return false; }

            using var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            string sql_query = $"UPDATE Leaves SET IsConfirm = @IsConfirm WHERE Leaves.Id = @Id";
            parm.Add("Id", id);
            parm.Add("IsConfirm", false);
            AffectedRows = dpr.Execute(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
            return (AffectedRows > 0);
        }

        /// <summary>
        /// 作廢單據
        /// </summary>
        /// <param name="id">單據 ID</param>
        public bool Cancel(int id)
        {
            ErrorMessage = "";
            var model = GetData(id);
            if (model == null) { ErrorMessage = "找不到指定的單據資料!!"; return false; }
            if (model.IsCancel) { ErrorMessage = "單據已經過作廢，無法再次作廢!!"; return false; }

            using var dpr = new DapperRepository();
            var parm = new DynamicParameters();
            string sql_query = $"UPDATE Leaves SET IsCancel = @IsCancel WHERE Leaves.Id = @Id";
            parm.Add("Id", id);
            parm.Add("IsCancel", true);
            AffectedRows = dpr.Execute(sql_query, parm);
            ErrorMessage = dpr.ErrorMessage;
            return (AffectedRows > 0);
        }
    }
}