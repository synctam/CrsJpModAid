namespace LibCrsJpMod.TransData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibCrsJpMod.FileUtils;

    /// <summary>
    /// 言語データ入出力
    /// </summary>
    public class CrsTransDataDao
    {
        /// <summary>
        /// 言語データ情報から翻訳済み言語情報ファイルを作成する。
        /// </summary>
        /// <param name="path">翻訳済み言語情報ファイル</param>
        /// <param name="dataInfo">言語データ情報</param>
        public static void SaveToFile(
            string path, CrsTransDataInfo dataInfo)
        {
            foreach (var dataFile in dataInfo.Items.Values)
            {
                var folderPath = Path.GetDirectoryName(path);
                CrsFileUtils.SafeCreateDirectory(folderPath);
                using (var sw = new StreamWriter(path, false))
                {
                    var bw = new BinaryWriter(sw.BaseStream);
                    //// ヘッダー書き込み
                    dataInfo.DataHeader.Write(bw);
                    //// エントリー数書き込み
                    bw.Write(dataFile.Items.Count);
                    foreach (var dataEntry in dataFile.Items.Values)
                    {
                        //// エントリー書き込み
                        dataEntry.Write(bw);
                    }
                }
            }
        }

        /// <summary>
        /// 言語フォルダー内の言語ファイルを読み込みデータ情報を作成する。
        /// </summary>
        /// <param name="dataInfo">データ情報</param>
        /// <param name="folderNameLangInput">言語フォルダー</param>
        public static void LoadFromFolder(
            CrsTransDataInfo dataInfo,
            string folderNameLangInput)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(
                folderNameLangInput, "*", SearchOption.AllDirectories);

            foreach (string f in files)
            {
                LoadFromFile(dataInfo, f);
            }
        }

        /// <summary>
        /// 言語ファイルを読み込み言語データを作成する。
        /// </summary>
        /// <param name="dataInfo">言語データ</param>
        /// <param name="path">言語ファイル</param>
        /// <param name="assetName">アセット名</param>
        public static void LoadFromFile(
            CrsTransDataInfo dataInfo,
            string path,
            string assetName = null)
        {
            if (string.IsNullOrWhiteSpace(assetName))
            {
                assetName = Path.GetFileName(path);
            }

            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                using (BinaryReader br = new BinaryReader(sr.BaseStream))
                {
                    var fileName = Path.GetFileName(path);
                    dataInfo.Read(br, fileName, assetName);
                }
            }
        }
    }
}
