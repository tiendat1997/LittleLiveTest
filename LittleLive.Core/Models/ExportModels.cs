using System;
using System.Collections.Generic;
using System.Text;

namespace LittleLive.Core.Models
{
    public class BaseExportHeader { }
    public class BaseExportColumnInfo
    {
        public BaseExportColumnInfo(int colIndex, string colName, int minWith)
        {
            ColumnIndex = colIndex;
            ColumnName = colName;
            MinWidth = minWith;
        }

        public BaseExportColumnInfo(int colIndex, string colName)
        {
            ColumnIndex = colIndex;
            ColumnName = colName;
        }

        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public int? MinWidth { get; set; }
    }

    #region Export Activity For School Owner
    public class ActivityExportHeader : BaseExportHeader
    {
        public static BaseExportColumnInfo ChildName = new BaseExportColumnInfo(1, "Child Name");
        public static BaseExportColumnInfo TimeCheckIn = new BaseExportColumnInfo(2, "Time Check In");
        public static BaseExportColumnInfo TimeCheckOut = new BaseExportColumnInfo(3, "Time Check Out");
        public static BaseExportColumnInfo ClassName = new BaseExportColumnInfo(4, "Class Name");
        public static BaseExportColumnInfo TeacherName = new BaseExportColumnInfo(5, "Teacher Name");
    }
    #endregion   
}
