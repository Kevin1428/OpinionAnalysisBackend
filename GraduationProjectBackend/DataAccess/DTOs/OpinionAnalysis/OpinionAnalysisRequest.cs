namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis
{
    public class OpinionAnalysisRequest
    {
        public class Route
        {
            /// <summary>
            /// 搜尋主題
            /// </summary>
            /// <example>戰爭 </example>
            public string Topic { get; set; }

            /// <summary>
            /// 搜尋開始時間
            /// </summary>
            /// <example>2022-01-01</example>
            public DateOnly StartDate { get; set; }

            /// <summary>
            /// 搜尋結束時間
            /// </summary>
            /// <example>2022-06-30</example>
            public DateOnly EndDate { get; set; }
        }
        public class Query
        {
            /// <summary>
            /// 分析日期的區間大小
            /// </summary>
            /// <example>30</example>
            public int dateRange { get; set; }
            /// <summary>
            /// 是不是要完全匹配到的資料才搜尋 預設false
            /// </summary>
            public bool? isExactMatch { get; set; }
        }
    }
}
