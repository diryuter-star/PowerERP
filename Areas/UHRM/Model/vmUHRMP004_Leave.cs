using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powererp.Models
{
    public class vmUHRMP004_Leave : BaseClass
    {
        /// <summary>
        /// UHRMP004 - 請假單表頭檔
        /// </summary>
        public Leaves MasterModel { get; set; } = new Leaves();
        /// <summary>
        /// UHRMP004 - 請假單明細檔
        /// </summary>
        public List<LeavesDetail> DetailModel { get; set; } = new List<LeavesDetail>();

        public vmUHRMP004_Leave()
        {
            using var sqlLeaves = new z_sqlLeaves();
            using var sqlLeavesDetail = new z_sqlLeavesDetail();
            MasterModel = sqlLeaves.GetMasterData();
            if (MasterModel == null) MasterModel = new Leaves();
            string baseNo = (MasterModel == null) ? "" : MasterModel.BaseNo;
            DetailModel = sqlLeavesDetail.GetDataList(baseNo);
            if (DetailModel == null) DetailModel = new List<LeavesDetail>();
        }
    }
}