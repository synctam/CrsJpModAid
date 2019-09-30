namespace LibCrsJpMod.TransData
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FsbCommonLib;

    /// <summary>
    /// 言語データヘッダー
    /// </summary>
    public class CrsDataHeader
    {
        public int GameObjectFileID { get; private set; } = 0;

        public long GameObjectPathID { get; private set; } = 0;

        public bool Enabled { get; private set; } = true;

        public int ScriptFileID { get; private set; } = 0;

        public long ScriptPathID { get; private set; } = 0;

        public string Name { get; private set; } = string.Empty;

        public string LanguageKey { get; private set; } = string.Empty;

        public string MasterLanguageKey { get; private set; } = string.Empty;

        /// <summary>
        /// Streamからヘッダー情報を読み込む。
        /// </summary>
        /// <param name="br">stream</param>
        public void Read(BinaryReader br)
        {
            this.GameObjectFileID = br.ReadInt32();
            this.GameObjectPathID = br.ReadInt64();
            this.Enabled = FsbBinUtils.ReadBoolean(br);
            this.ScriptFileID = br.ReadInt32();
            this.ScriptPathID = br.ReadInt64();
            this.Name = FsbBinUtils.ReadString(br);
            this.LanguageKey = FsbBinUtils.ReadString(br);
            this.MasterLanguageKey = FsbBinUtils.ReadString(br);
        }

        /// <summary>
        /// ヘッダー情報をStreamに書き出す。
        /// </summary>
        /// <param name="bw">stream</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.GameObjectFileID);
            bw.Write(this.GameObjectPathID);
            FsbBinUtils.WriteBoolean(bw, this.Enabled);
            bw.Write(this.ScriptFileID);
            bw.Write(this.ScriptPathID);
            FsbBinUtils.WriteString(bw, this.Name);
            FsbBinUtils.WriteString(bw, this.LanguageKey);
            FsbBinUtils.WriteString(bw, this.MasterLanguageKey);
        }

        /// <summary>
        /// ヘッダーのクローンを返す。
        /// </summary>
        /// <returns>ヘッダーのクローン</returns>
        public CrsDataHeader Clone()
        {
            var result = new CrsDataHeader();

            result.Enabled = this.Enabled;
            result.GameObjectFileID = this.GameObjectFileID;
            result.GameObjectPathID = this.GameObjectPathID;
            result.LanguageKey = this.LanguageKey;
            result.MasterLanguageKey = this.MasterLanguageKey;
            result.Name = this.Name;
            result.ScriptFileID = this.ScriptFileID;
            result.ScriptPathID = this.ScriptPathID;

            return result;
        }
    }
}
