namespace LibCrsJpMod.TransSheet
{
    /// <summary>
    /// 翻訳シートエントリー
    /// </summary>
    public class CrsTransSheetEntry
    {
        public CrsTransSheetEntry() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="no">項目番号</param>
        /// <param name="entryID">エントリーID</param>
        /// <param name="english">原文</param>
        /// <param name="japanese">翻訳文</param>
        /// <param name="mTrans">機械翻訳文</param>
        /// <param name="lastUpdate">最終更新日</param>
        public CrsTransSheetEntry(
            string assetName,
            int no,
            string entryID,
            string english,
            string japanese,
            string mTrans,
            string lastUpdate)
        {
            this.AssetName = assetName;
            this.No = no;
            this.EntryID = entryID;
            this.English = english;
            this.Japanese = japanese;
            this.MTrans = mTrans;
            this.LastUpdate = lastUpdate;
        }

        /// <summary>
        /// アセット名
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// 項目番号(ソート用)
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// キー：エントリーID
        /// </summary>
        public string EntryID { get; set; }

        /// <summary>
        /// 原文
        /// </summary>
        public string English { get; set; }

        /// <summary>
        /// 翻訳文
        /// </summary>
        public string Japanese { get; set; }

        /// <summary>
        /// 機械翻訳文
        /// </summary>
        public string MTrans { get; set; }

        /// <summary>
        /// 最終更新日
        /// </summary>
        public string LastUpdate { get; set; }
    }
}
