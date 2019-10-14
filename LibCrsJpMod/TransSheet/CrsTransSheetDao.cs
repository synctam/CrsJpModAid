namespace LibCrsJpMod.TransSheet
{
    using System.IO;
    using System.Text;
    using CsvHelper;
    using CsvHelper.Configuration;
    using LibCrsJpMod.TransData;

    /// <summary>
    /// 翻訳シート入出力
    /// </summary>
    public class CrsTransSheetDao
    {
        /// <summary>
        /// CSV形式の翻訳シートファイルから翻訳シート情報を作成する。
        /// </summary>
        /// <param name="sheetInfo">翻訳シート情報</param>
        /// <param name="path">CSV形式の翻訳シートファイルのパス</param>
        /// <param name="enc">文字コード</param>
        public static void LoadFromFile(
            CrsTransSheetInfo sheetInfo, string path, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.UTF8;
            }

            using (var reader = new StreamReader(path, enc))
            {
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.Delimiter = ",";
                    csv.Configuration.HasHeaderRecord = true;
                    csv.Configuration.RegisterClassMap<CsvMapper>();

                    //// データを読み出し
                    var records = csv.GetRecords<CrsTransSheetEntry>();

                    foreach (var record in records)
                    {
                        sheetInfo.AddEntry(record.AssetName, record);
                    }
                }
            }
        }

        /// <summary>
        /// 言語データ情報からCSVファイルを作成する。
        /// </summary>
        /// <param name="dataInfo">言語データ情報</param>
        /// <param name="path">CSV形式の翻訳シート</param>
        /// <param name="enc">文字コード</param>
        public static void SaveToFile(
            CrsTransDataInfo dataInfo, string path, Encoding enc = null)
        {
            using (var writer = new CsvWriter(
                new StreamWriter(path, false, Encoding.UTF8)))
            {
                writer.Configuration.RegisterClassMap<CsvMapper>();
                writer.WriteHeader<CrsTransSheetEntry>();
                writer.NextRecord();

                foreach (var dataFile in dataInfo.Items.Values)
                {
                    int no = 0;
                    foreach (var dataEntry in dataFile.Items.Values)
                    {
                        var sheetEntry = new CrsTransSheetEntry(
                            dataFile.AssetName,
                            no,
                            dataEntry.TranslationId,
                            dataEntry.TranslationText,
                            string.Empty,
                            string.Empty,
                            dataEntry.HumanlyReadableDate);

                        writer.WriteRecord(sheetEntry);
                        writer.NextRecord();

                        no++;
                    }
                }
            }
        }

        /// <summary>
        /// 格納ルール ：マッピングルール(一行目を列名とした場合は列名で定義することができる。)
        /// </summary>
        public class CsvMapper : ClassMap<CrsTransSheetEntry>
        {
            public CsvMapper()
            {
                // 出力時の列の順番は指定した順となる。
                this.Map(x => x.No).Name("[[No]]");
                this.Map(x => x.English).Name("[[English]]");
                this.Map(x => x.Japanese).Name("[[Japanese]]");
                this.Map(x => x.MTrans).Name("[[MTrans]]");
                this.Map(x => x.EntryID).Name("[[ID]]");
                this.Map(x => x.AssetName).Name("[[FileID]]");
            }
        }
    }
}
