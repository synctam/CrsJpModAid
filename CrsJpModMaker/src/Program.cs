namespace CrsJpModMaker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibCrsJpMod.TransData;
    using LibCrsJpMod.Translation;
    using LibCrsJpMod.TransSheet;
    using MonoOptions;
    using S5mDebugTools;

    public class Program
    {
        private static int Main(string[] args)
        {
            TOptions opt = new TOptions(args);
            if (opt.IsError)
            {
                TDebugUtils.Pause();
                return 1;
            }

            if (opt.Arges.Help)
            {
                opt.ShowUsage();

                TDebugUtils.Pause();
                return 1;
            }

            MakeMod(opt.Arges);

            TDebugUtils.Pause();
            return 0;
        }

        private static void MakeMod(TOptions.TArgs opt)
        {
            var dataInfo = new CrsTransDataInfo();
            CrsTransDataDao.LoadFromFile(
                dataInfo,
                opt.FileNameInput,
                string.Empty);

            var sheetInfo = new CrsTransSheetInfo();
            CrsTransSheetDao.LoadFromFile(
                sheetInfo,
                opt.FileNameSheet);

            var translatedInfo = CrsTranslation.Translate(dataInfo, sheetInfo, opt.UseMachineTrans);
            CrsTransDataDao.SaveToFile(opt.FileNameOutput, translatedInfo);
        }
    }
}
