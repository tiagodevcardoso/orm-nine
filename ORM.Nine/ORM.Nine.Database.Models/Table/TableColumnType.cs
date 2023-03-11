using System.ComponentModel;

namespace ORM.Nine.Database.Models.Table
{
    public enum TableColumnType
    {
        [Description("varchar")]
        Tvarchar = 1,
        [Description("int")]
        Tint = 2,
        [Description("datetime")]
        Tdatetime = 3,
        [Description("tinyint")]
        Ttinyint = 4,
        [Description("bit")]
        Tntext = 5,
        [Description("sql_variant")]
        Tsql_variant = 6,
        [Description("bit")]
        Tbit = 7,
        [Description("xml")]
        Txml = 8,
        [Description("image")]
        Timage = 9,
        [Description("real")]
        Treal = 10,
        [Description("money")]
        Tmoney = 11,
        [Description("time")]
        Ttime = 12,
        [Description("geography")]
        Tgeography = 13,
        [Description("datetimeoffset")]
        Tdatetimeoffset = 14,
        [Description("date")]
        Tdate = 15,
        [Description("hierarchyid")]
        Thierarchyid = 16,
        [Description("varbinary")]
        Tvarbinary = 17,
        [Description("text")]
        Ttext = 18,
        [Description("smallmoney")]
        Tsmallmoney = 19,
        [Description("bigint")]
        Tbigint = 20,
        [Description("float")]
        Tfloat = 21,
        [Description("numeric")]
        Tnumeric = 22,
        [Description("smallint")]
        Tsmallint = 23,
        [Description("uniqueidentifier")]
        Tuniqueidentifier = 24,
        [Description("datetime2")]
        Tdatetime2 = 25,
        [Description("char")]
        Tchar = 26,
        [Description("decimal")]
        Tdecimal = 27,
        [Description("nchar")]
        Tnchar = 28,
        [Description("timestamp")]
        Ttimestamp = 29,
        [Description("geometry")]
        Tgeometry = 30,
        [Description("nvarchar")]
        Tnvarchar = 31,
        [Description("smalldatetime")]
        Tsmalldatetime = 32,
        [Description("sysname")]
        Tsysname = 33,
        [Description("binary")]
        Tbinary = 34
    }

    public static class MyEnumExtensions
    {
        public static string ToDescriptionString(this TableColumnType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}