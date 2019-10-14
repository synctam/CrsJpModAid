namespace LibCrsJpMod.TransData
{
    using System;
    using System.IO;
    using System.Text;
    using FsbCommonLib;

    /// <summary>
    /// 言語データエントリー
    /// </summary>
    public class CrsTransDataEntry
    {
        public string TranslationId { get; private set; } = string.Empty;

        public string TranslationText { get; private set; } = string.Empty;

        public long LastModificationTicks { get; private set; } = 0;

        public string HumanlyReadableDate { get; private set; } = string.Empty;

        /// <summary>
        /// Streamから言語データエントリーを読み込む。
        /// </summary>
        /// <param name="br">Stream</param>
        public void Read(BinaryReader br)
        {
            this.TranslationId = FsbBinUtils.ReadString(br);
            this.TranslationText = FsbBinUtils.ReadString(br);
            this.LastModificationTicks = br.ReadInt64();
            this.HumanlyReadableDate = FsbBinUtils.ReadString(br);
        }

        /// <summary>
        /// 言語データエントリーをStreamに書き出す。
        /// </summary>
        /// <param name="bw">Stream</param>
        public void Write(BinaryWriter bw)
        {
            FsbBinUtils.WriteString(bw, this.TranslationId);
            FsbBinUtils.WriteString(bw, this.TranslationText);
            bw.Write(this.LastModificationTicks);
            FsbBinUtils.WriteString(bw, this.HumanlyReadableDate);
        }

        /// <summary>
        /// 翻訳済みテキストを返す。
        /// </summary>
        /// <param name="textEn">原文</param>
        /// <param name="textJp">翻訳文</param>
        /// <param name="textMT">機械翻訳文</param>
        /// <param name="useMT">機械翻訳の使用有無</param>
        /// <returns>翻訳済みテキスト</returns>
        public CrsTransDataEntry GetTranslatedEntry(string textEn, string textJp, string textMT, bool useMT)
        {
            var result = this.Clone();

            if (string.IsNullOrWhiteSpace(textJp) && string.IsNullOrWhiteSpace(textMT))
            {
                //// 日本語：なし、機械翻訳：なし
                //// 原文を返す。
                ////return textEn;
            }
            else if (!string.IsNullOrWhiteSpace(textJp) && string.IsNullOrWhiteSpace(textMT))
            {
                //// 日本語：あり、機械翻訳：なし
                //// 日本語訳を返す。
                result.TranslationText = textJp;
            }
            else if (string.IsNullOrWhiteSpace(textJp) && !string.IsNullOrWhiteSpace(textMT))
            {
                //// 日本語：なし、機械翻訳：あり
                if (useMT)
                {
                    if (textMT.Contains("{") || textMT.Contains("<"))
                    {
                        //// TAG または 置換文字がある場合は適用しない。
                        //// 原文を返す。
                    }
                    else
                    {
                        //// 機械翻訳を返す。
                        result.TranslationText = textMT;
                    }
                }
                else
                {
                    //// 原文を返す。
                }
            }
            else if (!string.IsNullOrWhiteSpace(textJp) && !string.IsNullOrWhiteSpace(textMT))
            {
                //// 日本語：あり、機械翻訳：あり
                //// 日本語訳を返す。
                result.TranslationText = textJp;
            }
            else
            {
                //// 設計エラー
                throw new Exception($"Unknown error.");
            }

            return result;
        }

        /// <summary>
        /// Debug情報出力用
        /// </summary>
        /// <returns>デバッグ情報</returns>
        public override string ToString()
        {
            var tab1 = "\t";
            var tab2 = "\t\t";

            var buff = new StringBuilder();
            buff.AppendLine($"{tab1}TranslationId({this.TranslationId}) LastUpdate({this.HumanlyReadableDate})");
            buff.AppendLine($"{tab2}TranslationId({this.TranslationText})");

            return buff.ToString();
        }

        /// <summary>
        /// 言語データエントリーのクローンの作成
        /// </summary>
        /// <returns>言語データエントリーのクローン</returns>
        private CrsTransDataEntry Clone()
        {
            var entry = new CrsTransDataEntry();

            entry.HumanlyReadableDate = this.HumanlyReadableDate;
            entry.LastModificationTicks = this.LastModificationTicks;
            entry.TranslationId = this.TranslationId;
            entry.TranslationText = this.TranslationText;

            return entry;
        }
    }
}
