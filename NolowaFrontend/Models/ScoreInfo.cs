using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Models
{
    /// <summary>
    ///  검색어 순위를 기록하는 데이터, 작은 데이터만 가지고 있어서 구조체로 선언함
    /// </summary>
    //public struct ScoreInfo
    //{
    //    public string Key { get; set; }
    //    public int Score { get; set; }
    //    public int Ranking { get; set; }

    //    public ScoreInfo(string key, int score, int ranking)
    //    {
    //        this.Key = key;
    //        this.Score = score;
    //        this.Ranking = ranking;
    //    }
    //}

    public class ScoreInfo
    {
        public string Key { get; set; }
        public int Score { get; set; }
        public int Ranking { get; set; }
    }
}
