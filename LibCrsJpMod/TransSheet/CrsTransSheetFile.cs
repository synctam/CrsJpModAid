namespace LibCrsJpMod.TransSheet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CrsTransSheetFile
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="assetName">アセット名</param>
        public CrsTransSheetFile(string assetName)
        {
            this.AssetName = assetName;
        }

        /// <summary>
        /// アセット名
        /// </summary>
        public string AssetName { get; private set; } = string.Empty;

        /// <summary>
        /// 翻訳シートエントリーの辞書。
        /// キーはエントリーID。
        /// </summary>
        public Dictionary<string, CrsTransSheetEntry> Items { get; } =
            new Dictionary<string, CrsTransSheetEntry>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 翻訳シートエントリーを追加する。
        /// </summary>
        /// <param name="sheetEntry">翻訳シートエントリー</param>
        public void AddEntry(CrsTransSheetEntry sheetEntry)
        {
            if (this.Items.ContainsKey(sheetEntry.EntryID))
            {
                //// キーが重複している場合は例外を発生させる。
                throw new Exception($"Duplucate ID({sheetEntry.EntryID})");
            }
            else
            {
                this.Items.Add(sheetEntry.EntryID, sheetEntry);
            }
        }

        /// <summary>
        /// 指定したエントリーIDの翻訳シートエントリーを返す。
        /// </summary>
        /// <param name="entryID">エントリーID</param>
        /// <returns>翻訳シートエントリー</returns>
        public CrsTransSheetEntry GetEntry(string entryID)
        {
            if (this.Items.ContainsKey(entryID))
            {
                return this.Items[entryID];
            }
            else
            {
                return null;
            }
        }
    }
}
