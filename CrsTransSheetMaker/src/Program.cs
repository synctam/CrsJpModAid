namespace CrsTransSheetMaker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using LibCrsJpMod.TransData;
    using LibCrsJpMod.TransSheet;
    using MonoOptions;
    using S5mDebugTools;

    public class Program
    {
        private static int Main(string[] args)
        {
            try
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

                MakeSheet(opt.Arges);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                TDebugUtils.Pause();
                return 1;
            }
        }

        private static void MakeSheet(TOptions.TArgs opt)
        {
            var dataInfo = new CrsTransDataInfo();
            CrsTransDataDao.LoadFromFolder(
                dataInfo,
                opt.FolderNameLangInput);

            CrsTransSheetDao.SaveToFile(dataInfo, opt.FileNameSheet);
        }
    }
}
