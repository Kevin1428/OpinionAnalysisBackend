namespace GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs
{
    public class WordCloudRequest
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
}
