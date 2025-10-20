using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace powererp.Models
{
    public class z_sqlEmployees : DapperSql<Employees>
    {
        public z_sqlEmployees()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Employees.EmpNo";
            DefaultOrderByDirection = "ASC";
            DropDownValueColumn = "Employees.EmpNo";
            DropDownTextColumn = "Employees.EmpName";
            DropDownOrderColumn = "Employees.EmpNo ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Employees.Id, Employees.IsValid, Employees.EmpNo, Employees.EmpName, Employees.GenderCode, 
vi_CodeGender.CodeName AS GenderName, Employees.DeptNo, Departments.DeptName, 
Employees.TitleNo, Titles.TitleName, Employees.Birthday, Employees.OnboardDate, Employees.LeaveDate, 
Employees.ContactEmail, Employees.ContactTel, Employees.CityName, Employees.CityArea, Employees.ContactAddress, 
ISNULL(Employees.CityName , '') + ISNULL(Employees.CityArea , '') + ISNULL(Employees.ContactAddress , '') AS FullAddress,
Employees.Remark
FROM Employees 
LEFT OUTER JOIN vi_CodeGender ON Employees.GenderCode = vi_CodeGender.CodeNo 
LEFT OUTER JOIN Departments ON Employees.DeptNo = Departments.DeptNo 
LEFT OUTER JOIN Titles ON Employees.TitleNo = Titles.TitleNo 
";
            return str_query;
        }

        public override Employees GetData(string dataNo)
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Employees.EmpNo = @DataNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("DataNo", dataNo);
            var model = dpr.ReadSingle<Employees>(sql_query, parm);
            return model;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("Titles.TitleName");
            searchColumn.Add("vi_CodeGender.CodeName");
            return searchColumn;
        }

        /// <summary>
        /// 員工照片
        /// </summary>
        /// <param name="empNo">員工編號</param>
        /// <value></value>
        public string EmployeeImage(string empNo)
        {
            string str_value = "~/images/employees/";
            //取得目前專案資料夾目錄路徑
            string FileNameOnServer = Directory.GetCurrentDirectory();
            //專案路徑加入要存入的資料夾路徑
            FileNameOnServer += "\\wwwroot\\images\\employees\\";
            //以使用者名稱.jpg 存入
            FileNameOnServer += $"{empNo}.jpg";
            //照片如果不存在則用預設照片
            if (File.Exists(FileNameOnServer))
                str_value += $"{empNo}.jpg";
            else
                str_value += "Employee.jpg";
            //除理瀏覽器會在緩存圖片問題
            str_value += $"?t={DateTime.Now.ToString("yyyyMMddHHmmssff")}";
            return str_value;
        }


        /// <summary>
        /// 設定匯出欄位
        /// </summary>
        /// <returns></returns>
        public List<string> GetExportColumns()
        {
            List<string> exportColumn = new List<string>
            {
                "IsValid",
                "EmpNo",
                "EmpName",
                "GenderName",
                "DeptName",
                "TitleName",
                "Birthday",
                "OnboardDate",
                "LeaveDate",
                "ContactEmail",
                "ContactTel",
                "FullAddress",
                "Remark"
            };
            return exportColumn;
        }

        /// <summary>
        /// 匯出資料到 Excel
        /// </summary>
        /// <returns></returns>
        public XLWorkbook ExportToExcel()
        {
            //設定變數
            bool bln_value = false;
            DateTime dtm_value = DateTime.MinValue;
            string className = EntityObject.GetType().Name;
            string nameSpaceName = EntityObject.GetType().Namespace;
            string metaClassName = "z_meta" + className;
            string columnName = "";
            string columnText = "";
            //取得員工資料
            var dataList = this.GetDataList();
            //取得欄位名稱
            var columnList = GetExportColumns();
            // 移除欄位名稱中的表格名稱及小數點
            for (int i = 0; i < columnList.Count; i++)
            {
                var col = columnList[i];
                var dotIndex = col.IndexOf('.');
                if (dotIndex >= 0 && dotIndex < col.Length - 1)
                {
                    columnList[i] = col.Substring(dotIndex + 1);
                }
            }
            //建立 Excel 物件
            var workbook = new XLWorkbook();
            //建立 Excel 工作表
            IXLWorksheet worksheet = workbook.Worksheets.Add("員工資料");
            //設定標題列名稱與樣式
            using var dataModel = new DataModelService();
            for (int i = 1; i <= columnList.Count(); i++)
            {
                //取得欄位名稱
                columnName = columnList[i - 1];
                //取得欄位顯示名稱
                columnText = dataModel.GetPropertyTypeValue(className, columnName, enDataModelProperty.DisplayName, nameSpaceName, metaClassName);
                //設定標題列
                worksheet.Cell(1, i).Value = columnText;
                //worksheet.Cell(1, i).Style.Fill.SetBackgroundColor(XLColor.Red);
                worksheet.Cell(1, i).Style.Font.SetFontSize(12);
                worksheet.Cell(1, i).Style.Font.SetBold();
            }

            //設定資料列
            for (int j = 1; j <= dataList.Count(); j++)
            {
                for (int i = 1; i <= columnList.Count(); i++)
                {
                    //取得欄位名稱與值
                    columnName = columnList[i - 1];
                    var value = dataList[j - 1].GetType().GetProperty(columnName)?.GetValue(dataList[j - 1], null);
                    string str_value = value?.ToString();
                    if (!string.IsNullOrEmpty(str_value))
                    {
                        if (columnName == "IsValid")
                        {
                            bln_value = false;
                            bool.TryParse(str_value, out bln_value);
                            str_value = bln_value ? "是" : "否";
                        }
                        if (columnName == "Birthday" || columnName == "OnboardDate" || columnName == "LeaveDate")
                        {
                            dtm_value = DateTime.MinValue;
                            DateTime.TryParse(str_value, out dtm_value);
                            str_value = dtm_value == DateTime.MinValue ? "" : dtm_value.ToString("yyyy/MM/dd");
                        }
                    }
                    //寫入資料到工作表
                    worksheet.Cell(j + 1, i).Value = str_value;
                }
            }
            //自動調整欄寬
            worksheet.Columns().AdjustToContents();
            //回傳 Excel 工作簿
            return workbook;
        }
    }
}