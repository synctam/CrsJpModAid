namespace LibCrsJpMod.TransSheet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CrsTransSheetInfo
    {
        /// <summary>
        /// 翻訳シートファイルの辞書
        /// キーはアセット名。
        /// </summary>
        public Dictionary<string, CrsTransSheetFile> Items { get; } =
            new Dictionary<string, CrsTransSheetFile>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 翻訳シートエントリーを追加する。
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="sheetEntry">翻訳シートエントリー</param>
        public void AddEntry(string assetName, CrsTransSheetEntry sheetEntry)
        {
            if (this.Items.ContainsKey(assetName))
            {
                //// 既に登録済みに場合は翻訳シートファイルに追加する。
                var sheetFile = this.Items[assetName];
                sheetFile.AddEntry(sheetEntry);
            }
            else
            {
                //// 翻訳シートファイルが未登録の場合は新規に作成し、
                //// 翻訳シートエントリーを追加する。
                var sheetFile = new CrsTransSheetFile(assetName);
                sheetFile.AddEntry(sheetEntry);
                this.Items.Add(sheetFile.AssetName, sheetFile);
            }
        }

        /// <summary>
        /// 指定したアセット名の翻訳シートファイルを返す。
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <returns>翻訳シートファイル</returns>
        public CrsTransSheetFile GetFile(string assetName)
        {
            if (this.Items.ContainsKey(assetName))
            {
                return this.Items[assetName];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 指定したアセット名と翻訳IDの翻訳シートエントリーを返す。
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="entryID">エントリーID</param>
        /// <returns>翻訳シートエントリー</returns>
        public CrsTransSheetEntry GetEntry(string assetName, string entryID)
        {
            if (this.Items.ContainsKey(assetName))
            {
                var sheetFile = this.Items[assetName];
                return sheetFile.GetEntry(entryID);
            }
            else
            {
                foreach (var sheetFile in this.Items.Values)
                {
                    var entry = sheetFile.GetEntry(entryID);
                    if (entry != null)
                    {
                        return entry;
                    }
                }

                return null;
            }
        }
    }
}
