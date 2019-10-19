# CrsJpModAid
Crying Suns Demo 版を日本語化するためのツールです。

開発には Visual Studio 2017 Community版を使用しています。


## CrsTransSheetMaker

    翻訳シートを作成する。
      usage: CrsTransSheetMaker.exe -i <lang folder path> -s <trans sheet path> [-r]
    OPTIONS:
      -i, --in=VALUE             オリジナル版の言語ファイルのあるフォルダーのパスを指定する。
      -s, --sheet=VALUE          CSV形式の翻訳シートのパス名。
      -r                         翻訳シートが既に存在する場合はを上書きする。
      -h, --help                 ヘルプ
    Example:
      オリジナルの言語ファイルのあるフォルダー(-i)から翻訳シート(-s)を作成する。
        CrsTransSheetMaker.exe -i data\lang -s data\csv\CrsTransSheet.csv
    終了コード:
     0  正常終了
     1  異常終了


## CrsJpModMaker

    日本語化MODを作成する。
      usage: CrsJpModMaker.exe -i <original lang file path> -o <japanized lang file path> -s <Trans Sheet path> [-m] [-r]
    OPTIONS:
      -i, --in=VALUE             オリジナル版の言語ファイルのパスを指定する。
      -o, --out=VALUE            日本語化された言語ファイルのパスを指定する。
      -s, --system=VALUE         CSV形式の翻訳シートのパス名。
      -m                         有志翻訳がない場合は機械翻訳を使用する。
                                   注意事項：機械翻訳を使用した場合は、ゲームがフリーズすることがあります。
      -r                         出力用言語ファイルが既に存在する場合はを上書きする。
      -h, --help                 ヘルプ
    Example:
      翻訳シート(-s)とオリジナルの言語ファイル(-i)から日本語化MOD(-o)を作成する。
        CrsJpModMaker.exe -i original\resources_00002.-5 -o new\resources_00002.-5 -s CrsTransSheet.csv
    終了コード:
     0  正常終了
     1  異常終了
 
