namespace LibCrsJpMod.TransData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 言語データファイル
    /// </summary>
    public class CrsTransDataFile
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="assetName">アセット名</param>
        public CrsTransDataFile(string fileName, string assetName)
        {
            this.FileName = fileName;
            this.AssetName = assetName;
        }

        /// <summary>
        /// Asset Studio で確認できる項目名
        /// </summary>
        public string AssetName { get; private set; } = string.Empty;

        /// <summary>
        /// UnityEX で確認できる項目名。物理ファイル名。
        /// </summary>
        public string FileName { get; private set; } = string.Empty;

        /// <summary>
        /// 言語ファイル辞書
        /// </summary>
        public Dictionary<string, CrsTransDataEntry> Items { get; } =
            new Dictionary<string, CrsTransDataEntry>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 言語ファイルを読み込み、言語ファイル辞書を作成する。
        /// </summary>
        /// <param name="br">BinaryReader</param>
        public void Read(BinaryReader br)
        {
            var entryCount = br.ReadInt32();
            for (var i = 0; i < entryCount; i++)
            {
                var entry = new CrsTransDataEntry();
                entry.Read(br);
                this.AddEntry(entry);
            }
        }

        /// <summary>
        /// エントリーの追加。
        /// </summary>
        /// <param name="entry">エントリー</param>
        public void AddEntry(CrsTransDataEntry entry)
        {
            if (this.Items.ContainsKey(entry.TranslationId))
            {
                throw new Exception($"Duplicate key({entry.TranslationId}).");
            }
            else
            {
                this.Items.Add(entry.TranslationId, entry);
            }
        }

        /// <summary>
        /// Debug情報出力用
        /// </summary>
        /// <returns>デバッグ情報</returns>
        public override string ToString()
        {
            var buff = new StringBuilder();
            buff.AppendLine($"AssetName({this.AssetName}) FileName({this.FileName})");
            foreach (var entry in this.Items.Values)
            {
                buff.AppendLine(entry.ToString());
            }

            return buff.ToString();
        }
    }
}
