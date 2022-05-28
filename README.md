# TestDebugStation アプリケーション

デバッグ支援アプリ DebugStation 用のテストプロジェクトです。


# 動作環境

- Windows 8.1以降
- .NET Framework 4.0 以降、または互換性のある .NET 実装


# 依存リポジトリ

- [DebugStation](https://github.com/Nov-Lab/DebugStation) アプリケーション
- [NovLab.Base](https://github.com/Nov-Lab/NovLab.Base) クラスライブラリ
- [NovLab.Windows.Forms](https://github.com/Nov-Lab/NovLab.Windows.Forms) クラスライブラリ

### ローカルリポジトリにおけるフォルダー配置について

本リポジトリのソリューションと、依存リポジトリのソリューションは、以下のように同じ親フォルダーの下へ配置してください。
```
＜親フォルダー＞
  ├ DebugStation ソリューション
  ├ NovLab.Base ソリューション
  ├ NovLab.Windows.Forms ソリューション
  └ TestDebugStation ソリューション
```


# 使い方

DebugStation を起動した状態で本アプリケーションを実行し、テスト項目を選んで実行します。


# フォルダー構成

- `binfile` ：DEBUGビルドでコンパイル済みのバイナリーファイルです。
- `TestDebugStation` ：TestDebugStation のプロジェクトです。


# ライセンス

本ソフトウェアは、MITライセンスに基づいてライセンスされています。

ただし、改変する場合は、namespace の名前を変えて重複や混乱を避けることを強く推奨します。


# 開発環境

## 開発ツール、SDKなど
- Visual Studio Community 2019
  - ワークロード：.NET デスクトップ開発

## 言語
- C#


# その他

Nov-Lab 独自の記述ルールと用語については [NovLabRule.md](https://github.com/Nov-Lab/Nov-Lab/NovLabRule.md) を参照してください。
