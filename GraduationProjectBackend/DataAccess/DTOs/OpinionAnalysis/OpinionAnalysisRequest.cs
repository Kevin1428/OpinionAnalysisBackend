using GraduationProjectBackend.Enums;

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
            public int DateRange { get; set; } = 30;

            /// <summary>
            /// 是不是要完全 Match 到的資料才分析
            /// </summary>
            public bool IsExactMatch { get; set; } = false;

            /// <summary>
            /// 對(標題,內文)搜索 或是 (標題,內文)(留言) 分別搜尋
            /// </summary>
            public SearchModeEnum SearchMode { get; set; } = SearchModeEnum.Normal;
        }


    }

}
