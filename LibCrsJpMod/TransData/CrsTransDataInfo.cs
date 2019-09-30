namespace LibCrsJpMod.TransData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 言語データ情報
    /// </summary>
    public class CrsTransDataInfo
    {
        /// <summary>
        /// 言語データヘッダー
        /// </summary>
        public CrsDataHeader DataHeader { get; set; } = new CrsDataHeader();

        /// <summary>
        /// ファイル辞書
        /// キーはアセット名
        /// </summary>
        public Dictionary<string, CrsTransDataFile> Items { get; } =
            new Dictionary<string, CrsTransDataFile>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Streamから言語データヘッダーを読み込む
        /// </summary>
        /// <param name="br">Stream</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="assetName">アセット名</param>
        public void Read(BinaryReader br, string fileName, string assetName)
        {
            this.DataHeader = new CrsDataHeader();
            this.DataHeader.Read(br);

            var dataFile = new CrsTransDataFile(fileName, assetName);
            dataFile.Read(br);
            this.AddFile(dataFile);
        }

        /// <summary>
        /// 言語データファイルを追加する。
        /// </summary>
        /// <param name="crsTransDataFile">言語データファイル</param>
        public void AddFile(CrsTransDataFile crsTransDataFile)
        {
            if (this.Items.ContainsKey(crsTransDataFile.AssetName))
            {
                //// 既に存在する場合は、エントリーを追加する。
                var dataFile = this.Items[crsTransDataFile.AssetName];
                foreach (var entry in crsTransDataFile.Items.Values)
                {
                    dataFile.AddEntry(entry);
                }
            }
            else
            {
                this.Items.Add(crsTransDataFile.AssetName, crsTransDataFile);
            }
        }

        /// <summary>
        /// 言語データエントリーを追加する。
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="assetName">アセット名</param>
        /// <param name="entry">言語データエントリー</param>
        public void AddEntry(string fileName, string assetName, CrsTransDataEntry entry)
        {
            if (this.Items.ContainsKey(assetName))
            {
                var dataFile = this.Items[assetName];
                dataFile.AddEntry(entry);
            }
            else
            {
                var dataFile = new CrsTransDataFile(fileName, assetName);
                dataFile.AddEntry(entry);

                this.Items.Add(dataFile.AssetName, dataFile);
            }
        }

        /// <summary>
        /// 言語データエントリーを返す。
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <returns>言語データエントリー</returns>
        public CrsTransDataFile GetFile(string assetName)
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
        /// Debug情報出力用
        /// </summary>
        /// <returns>デバッグ情報</returns>
        public override string ToString()
        {
            var buff = new StringBuilder();
            foreach (var dataFile in this.Items.Values)
            {
                buff.AppendLine(dataFile.ToString());
            }

            return buff.ToString();
        }
    }
}
