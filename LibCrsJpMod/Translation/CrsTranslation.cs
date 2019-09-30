namespace LibCrsJpMod.Translation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibCrsJpMod.TransData;
    using LibCrsJpMod.TransSheet;

    /// <summary>
    /// 翻訳
    /// </summary>
    public class CrsTranslation
    {
        /// <summary>
        /// 言語データ情報と翻訳シート情報を元に翻訳済み言語データ情報を返す。
        /// </summary>
        /// <param name="dataInfo">言語データ情報</param>
        /// <param name="sheetInfo">翻訳シート情報</param>
        /// <param name="useMT">機械翻訳を採用する</param>
        /// <returns>翻訳済み言語データ情報</returns>
        public static CrsTransDataInfo Translate(
            CrsTransDataInfo dataInfo, CrsTransSheetInfo sheetInfo, bool useMT = false)
        {
            var dataFileJp = new CrsTransDataInfo();
            dataFileJp.DataHeader = dataInfo.DataHeader.Clone();

            foreach (var dataFile in dataInfo.Items.Values)
            {
                foreach (var entry in dataFile.Items.Values)
                {
                    var sheetEntry = sheetInfo.GetEntry(dataFile.AssetName, entry.TranslationId);
                    if (sheetEntry != null)
                    {
                        var entryJp = entry.GetTranslatedEntry(
                            entry.TranslationText,
                            sheetEntry.Japanese,
                            sheetEntry.MTrans,
                            useMT);

                        dataFileJp.AddEntry(dataFile.FileName ,dataFile.AssetName, entryJp);
                    }
                }
            }

            return dataFileJp;
        }
    }
}
