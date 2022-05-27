// @(h)FrmAppTestDebugStation.cs ver 0.00 ( '22.04.20 Nov-Lab ) 作成開始
// @(h)FrmAppTestDebugStation.cs ver 0.51 ( '22.05.19 Nov-Lab ) ベータ版完成
// @(h)FrmAppTestDebugStation.cs ver 0.51a( '22.05.24 Nov-Lab ) その他  ：コメント整理

// @(s)
// 　【メイン画面】Test for デバッグステーションのメイン画面です。

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Reflection;

using NovLab;
using NovLab.DebugStation;
using NovLab.DebugSupport;

#if DEBUG
using NovLab.ZZZTest;
#endif


namespace TestDebugStation
{
    //====================================================================================================
    /// <summary>
    /// 【メイン画面】Test for デバッグステーションのメイン画面です。
    /// </summary>
    //====================================================================================================
    public partial class FrmAppTestDebugStation : Form
    {
        /// <summary>
        /// 【テスト用オブジェクト】
        /// </summary>
        protected static KeyValuePair<string, string> m_testKVP = new KeyValuePair<string, string>("キー", "値");


        //====================================================================================================
        // フォームイベント
        //====================================================================================================

        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【メインフォーム_コンストラクター】新しいインスタンスを初期化します。
        /// </summary>
        //--------------------------------------------------------------------------------
        public FrmAppTestDebugStation()
        {
            //------------------------------------------------------------
            // 自動生成された部分
            //------------------------------------------------------------
            InitializeComponent();
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【メインフォーム_Load】本フォームを初期化します。
        /// </summary>
        //--------------------------------------------------------------------------------
        private void FrmAppTestDebugStation_Load(object sender, EventArgs e)
        {
            //------------------------------------------------------------
            /// DEBUGビルドの場合はWin32定義テストの準備をする
            //------------------------------------------------------------
#if DEBUG   // DEBUGビルドのみ有効
#if false   // Win32定義テストを行うときは true にする
            NovLab.Win32.TestWin32Define.PrepareTest();
#endif
#endif


            //------------------------------------------------------------
            /// アプリケーションを初期化する
            //------------------------------------------------------------
            DebugStationTraceListener.RegisterListener();

            NLDebug.SendProcessStart();
            Debug.Print("Hello world, from " + this.Name);

            Application.ApplicationExit +=                              //// アプリケーション_ApplicationExit ハンドラを設定する
                new EventHandler(OnApplicationExit);


            //------------------------------------------------------------
            /// 手動テスト用メソッドを列挙してテスト項目リストボックスに設定する
            //------------------------------------------------------------
#if DEBUG   // DEBUGビルドのみ有効
            foreach (var typeInfo in Assembly.GetExecutingAssembly().GetTypes())
            {                                                           //// 実行中アセンブリ内の型情報を繰り返す
                var testMethodinfos =                                   /////  手動テスト用メソッド情報を列挙する
                    TestMethodInfo.EnumManualTest(typeInfo);
                foreach (var info in testMethodinfos)
                {                                                       /////  手動テスト用メソッド情報配列を繰り返す
                    LstTestMenu.Items.Add(info);                        //////   テスト項目リストボックスに追加する
                }
            }
#else
            LstTestMenu.Items.Add("DEBUGビルドでのみ動作します");
#endif

            if (LstTestMenu.Items.Count >= 1)
            {                                                           //// テスト項目リストボックスに項目がある場合
                LstTestMenu.SelectedIndex = 0;                          /////  先頭の項目を選択状態にする
            }
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【アプリケーション_ApplicationExit】
        /// アプリケーションが終了するときに呼び出されます。<br></br>
        /// ・デバッグステーションアプリケーションへプロセス終了シグナルを送信します。<br></br>
        /// </summary>
        //--------------------------------------------------------------------------------
        private void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                NLDebug.SendProcessExit();
            }
            catch { }
        }


        //====================================================================================================
        // メニューイベント
        //====================================================================================================

        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【デバッグ - デバッグステーションを起動メニュー_Click】デバッグステーションを起動します。
        /// </summary>
        //--------------------------------------------------------------------------------
        private void MnuDebug_LaunchDebugStation_Click(object sender, EventArgs e)
        {
            //------------------------------------------------------------
            /// デバッグステーションを起動する
            //------------------------------------------------------------
            var path = Path.GetFullPath("..\\..\\..\\..\\DebugStation\\DebugStation\\bin\\Debug\\DebugStation.exe");

            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("起動できません：" + path + "\n\n" + ex.Message,
                    null,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        //====================================================================================================
        // コントロールイベント
        //====================================================================================================

        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【テスト項目リストボックス_KeyPress】Enterキーが押された場合、テスト項目リストボックスで選択されているテストを実行します。
        /// </summary>
        //--------------------------------------------------------------------------------
        private void LstTestMenu_KeyPress(object sender, KeyPressEventArgs e)
        {
            //------------------------------------------------------------
            /// Enterキーが押された場合はテストを実行する
            //------------------------------------------------------------
            if (e.KeyChar == '\r')
            {                                                           //// 押されたキー = Enter の場合
                M_ExecuteTest();                                        /////  テスト実行処理を行う
            }
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【テスト項目リストボックス_DoubleClick】テスト項目リストボックスで選択されているテストを実行します。
        /// </summary>
        //--------------------------------------------------------------------------------
        private void LstTestMenu_DoubleClick(object sender, EventArgs e)
        {
            M_ExecuteTest();
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【テスト実行ボタン_Click】テスト項目リストボックスで選択されているテストを実行します。
        /// </summary>
        //--------------------------------------------------------------------------------
        private void BtnExecuteTest_Click(object sender, EventArgs e)
        {
            M_ExecuteTest();
        }


        //====================================================================================================
        // テスト用メソッド
        //====================================================================================================

        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【テスト実行】テスト項目リストボックスで選択されているテストを実行します。
        /// </summary>
        //--------------------------------------------------------------------------------
        protected void M_ExecuteTest()
        {
#if DEBUG   // DEBUGビルドのみ有効
            //------------------------------------------------------------
            /// テスト項目リストボックスで選択されているテストを実行する
            //------------------------------------------------------------
            if (LstTestMenu.SelectedIndex == -1)
            {                                                           //// テスト項目リストボックスで選択されている項目がない場合
                SystemSounds.Beep.Play();                               /////  Beep音を鳴らす
                Console.WriteLine("テスト項目が選択されていません。");  /////  エラーメッセージ出力
                return;                                                 /////  関数終了
            }

            var info = (TestMethodInfo)LstTestMenu.SelectedItem;        //// 選択中項目からテストメソッド情報を取得する
            ManualTestMethodAttribute.Invoke(info.methodInfo);          //// 手動テスト用メソッドを実行する
#endif
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【インデント開始】Debug と Trace のインデントを開始します。
        /// </summary>
        /// <remarks>
        /// ・条件付き属性(Conditional)を持つメソッドはデリゲート化できないため、このメソッドを通じてデリゲート化します。
        /// </remarks>
        //--------------------------------------------------------------------------------
        protected static void M_Indent()
        {
            Trace.Indent(); // Debug と Trace の IndentLevel は共有らしいので Trace 側だけ呼び出せばOK(Debug 側だとリリースビルドで動作しなくなる)
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【インデント終了】Debug と Trace のインデントを終了します。
        /// </summary>
        /// <remarks>
        /// ・条件付き属性(Conditional)を持つメソッドはデリゲート化できないため、このメソッドを通じてデリゲート化します。
        /// </remarks>
        //--------------------------------------------------------------------------------
        protected static void M_Unindent()
        {
            Trace.Unindent();   // Debug と Trace の IndentLevel は共有らしいので Trace 側だけ呼び出せばOK(Debug 側だとリリースビルドで動作しなくなる)
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【デモデータ(ホテルの予約システム風)】
        /// </summary>
        //--------------------------------------------------------------------------------
        [ManualTestMethod("デモデータ(ホテルの予約システム風)")]
        public static void ZZZ_DemoHotel()
        {
            const string PROCNAME_UI = "宿泊予約システム";              // フロントエンド側(UI側)のプロセス名
            const string PROCNAME_SV = "宿泊予約サーバー";              // バックエンド側(サーバー側)のプロセス名
            const string TASKNAME = "新規予約[フロントNo03端末]";       // タスク名


            //------------------------------------------------------------
            /// 前準備
            //------------------------------------------------------------
            var prevProcName = DebugData.CurrentProcessName;            //// カレントプロセス名を退避する


            //------------------------------------------------------------
            /// デモデータを流し込む
            //------------------------------------------------------------
            Debug.Print("▼デモデータ(ホテルの予約システム風)");

            //----------------------------------------
            // 仮予約失敗パターン
            //----------------------------------------
            DebugData.CurrentProcessName = PROCNAME_UI;
            Trace.WriteLine("仮予約要求送信：22/05/03-22/05/05 シングル3, ダブル1, 同棟割り当て", TASKNAME);

            System.Threading.Thread.Sleep(10);
            DebugData.CurrentProcessName = PROCNAME_SV;
            Trace.WriteLine("仮予約要求受信：22/05/03-22/05/05 シングル3, ダブル1, 同棟割り当て", TASKNAME);
            Trace.WriteLine("空部屋検索結果：22/05/03-22/05/05 本館[シングル5, ダブル0] 新館[シングル0, ダブル1]", TASKNAME);
            Trace.WriteLine("仮予約結果送信：仮予約不可(代案あり)\n代案1:シングル5, 同棟割り当て\n代案2:シングル3, ダブル1, 別棟割り当て", TASKNAME);

            System.Threading.Thread.Sleep(10);
            DebugData.CurrentProcessName = PROCNAME_UI;
            Trace.WriteLine("仮予約結果受信：仮予約不可(代案あり)\n代案1:シングル5, 同棟割り当て\n代案2:シングル3, ダブル1, 別棟割り当て", TASKNAME);
            Trace.WriteLine("処理完了", TASKNAME);

            System.Threading.Thread.Sleep(300);


            //----------------------------------------
            // 仮予約成功パターン
            //----------------------------------------
            DebugData.CurrentProcessName = PROCNAME_UI;
            Trace.WriteLine("仮予約要求送信：22/05/03-22/05/05 シングル3, ダブル1, 別棟割り当て", TASKNAME);

            System.Threading.Thread.Sleep(10);
            DebugData.CurrentProcessName = PROCNAME_SV;
            Trace.WriteLine("仮予約要求受信：22/05/03-22/05/05 シングル3, ダブル1, 別棟割り当て", TASKNAME);
            Trace.WriteLine("空部屋検索結果：22/05/03-22/05/05 本館[シングル5, ダブル0] 新館[シングル0, ダブル1]", TASKNAME);
            Trace.WriteLine("仮予約処理成功：仮予約番号[PR22673] 22/05/03-22/05/05 本館[シングル3] 新館[ダブル1]", TASKNAME);
            Trace.WriteLine("仮予約結果送信：仮予約完了[PR22673] 22/05/03-22/05/05 本館[シングル3] 新館[ダブル1]", TASKNAME);

            System.Threading.Thread.Sleep(10);
            DebugData.CurrentProcessName = PROCNAME_UI;
            Trace.WriteLine("仮予約結果受信：仮予約完了[PR22673] 22/05/03-22/05/05 本館[シングル3] 新館[ダブル1]", TASKNAME);
            Trace.WriteLine("処理完了", TASKNAME);

            System.Threading.Thread.Sleep(400);

            //----------------------------------------
            // 予約確定パターン
            //----------------------------------------
            DebugData.CurrentProcessName = PROCNAME_UI;
            Trace.WriteLine("予約確定要求送信：仮予約番号[PR22673]", TASKNAME);

            System.Threading.Thread.Sleep(10);
            DebugData.CurrentProcessName = PROCNAME_SV;
            Trace.WriteLine("予約確定要求受信：仮予約番号[PR22673]", TASKNAME);
            Trace.WriteLine("予約確定結果送信：予約完了[RSV220503-037] 仮予約番号[PR22673]", TASKNAME);

            System.Threading.Thread.Sleep(10);
            DebugData.CurrentProcessName = PROCNAME_UI;
            Trace.WriteLine("予約確定結果受信：予約完了[RSV220503-037] 仮予約番号[PR22673]", TASKNAME);
            Trace.WriteLine("処理完了", TASKNAME);


            //------------------------------------------------------------
            /// 後始末
            //------------------------------------------------------------
            DebugData.CurrentProcessName = prevProcName;                //// カレントプロセス名をもとに戻す
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【Debug クラスからの呼び出しテスト(基本)】
        /// </summary>
        //--------------------------------------------------------------------------------
        [ManualTestMethod("Debug クラスからの呼び出し(基本)")]
        public static void ZZZ_TestDebug()
        {
            //------------------------------------------------------------
            Debug.Print("▼Debug クラスからの呼び出しテスト(Fail系以外)");

            using(var cleanup = new Cleanup(M_Indent, M_Unindent))
            {                                                           //// using：前処理=インデント開始、後処理=インデント終了
                Debug.Print("デバッグ出力");
                Debug.Print("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);

                Debug.Write("文字列出力(未完)、");                      // メッセージのみ
                Debug.WriteLine("文字列出力(終端)");

                Debug.Write("文字列出力(未完)、", "カテゴリー");        // メッセージ＋カテゴリー
                Debug.WriteLine("文字列出力(終端)", "カテゴリー");

                Debug.Write(m_testKVP);                                 // オブジェクトのみ
                Debug.WriteLine(m_testKVP);

                Debug.Write(m_testKVP, "カテゴリー");                   // オブジェクト＋カテゴリー
                Debug.WriteLine(m_testKVP, "カテゴリー");

                Debug.WriteLine("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);   // 書式指定
            }
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【Trace クラスからの呼び出しテスト(基本)】
        /// </summary>
        //--------------------------------------------------------------------------------
        [ManualTestMethod("Trace クラスからの呼び出し(基本)")]
        public static void ZZZ_TestTrace()
        {
            //------------------------------------------------------------
            Trace.TraceInformation("▼Trace クラスからの呼び出しテスト(Fail系以外)");

            using (var cleanup = new Cleanup(M_Indent, M_Unindent))
            {                                                           //// using：前処理=インデント開始、後処理=インデント終了
                Trace.TraceError("エラーメッセージ");                   // TraceXXX系
                Trace.TraceWarning("警告メッセージ");
                Trace.TraceInformation("情報メッセージ");

                Trace.TraceError("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);
                Trace.TraceWarning("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);
                Trace.TraceInformation("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);

                Trace.Write("文字列出力(未完)、");                      // Writeに続けてTraceXXX系
                Trace.TraceError("エラーメッセージ");

                Trace.Write("文字列出力(未完)、");
                Trace.TraceWarning("警告メッセージ");

                Trace.Write("文字列出力(未完)、");
                Trace.TraceInformation("情報メッセージ");

                Trace.Write("文字列出力(未完)、");
                Trace.TraceError("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);

                Trace.Write("文字列出力(未完)、");
                Trace.TraceWarning("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);

                Trace.Write("文字列出力(未完)、");
                Trace.TraceInformation("{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);

                Trace.Write("文字列出力(未完)、");                      // メッセージのみ
                Trace.WriteLine("文字列出力(終端)");

                Trace.Write("文字列出力(未完)、", "カテゴリー");        // メッセージ＋カテゴリー
                Trace.WriteLine("文字列出力(終端)", "カテゴリー");

                Trace.Write(m_testKVP);                                 // オブジェクトのみ
                Trace.WriteLine(m_testKVP);

                Trace.Write(m_testKVP, "カテゴリー");                   // オブジェクト＋カテゴリー
                Trace.WriteLine(m_testKVP, "カテゴリー");

                // ＜メモ＞書式指定タイプ：TraceXXX系メソッドにはあるが、WriteLine メソッドにははない
            }
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【インデント処理のテスト】
        /// </summary>
        //--------------------------------------------------------------------------------
        [ManualTestMethod("インデント処理")]
        public static void ZZZ_TestIndent()
        {
            // ＜メモ＞TraceSourceクラスにはインデント機能はない

            //------------------------------------------------------------
            Trace.TraceWarning("▼インデントのテスト(Trace)");

            Trace.TraceWarning("親レベル");
            Trace.Indent();
            Trace.TraceWarning("子レベル");

            Trace.Indent();
            Trace.TraceWarning("孫レベル");
            Trace.Unindent();

            Trace.TraceWarning("子レベル");
            Trace.Unindent();
            Trace.TraceWarning("親レベル");


            //------------------------------------------------------------
            Debug.Print("▼インデントのテスト(Debug)");

            Debug.Print("親レベル");
            Debug.Indent();
            Debug.Print("子レベル");

            Debug.Indent();
            Debug.Print("孫レベル");
            Debug.Unindent();

            Debug.Print("子レベル");
            Debug.Unindent();
            Debug.Print("親レベル");
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【Fail系メソッドのテスト】
        /// </summary>
        //--------------------------------------------------------------------------------
        [ManualTestMethod("Fail系メソッド")]
        public static void ZZZ_TestFail()
        {
            // ↓メッセージボックスの表示を抑制したい場合は有効化する
            Debug.Listeners.Remove("Default");


            //------------------------------------------------------------
            Debug.Print("▼Debug の Fail 系メソッド");

            using (new Cleanup(M_Indent, M_Unindent))
            {                                                           //// using：前処理=インデント開始、後処理=インデント終了
                Debug.Fail("Debugフェイル");
                Debug.Fail("Debugフェイル", "詳細メッセージ");

                Debug.Assert(false);
                Debug.Assert(false, "Debugアサート");
                Debug.Assert(false, "Debugアサート", "詳細メッセージ");
                Debug.Assert(false, "Debugアサート", "{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);

                Debug.Write("文字列出力(未完)、"); Debug.Assert(false);
                Debug.Write("文字列出力(未完)、"); Debug.Assert(false, "Debugアサート");
                Debug.Write("文字列出力(未完)、"); Debug.Assert(false, "Debugアサート", "詳細メッセージ");
                Debug.Write("文字列出力(未完)、"); Debug.Assert(false, "Debugアサート", "{0:g} to {1:g}", DayOfWeek.Sunday, DayOfWeek.Wednesday);
            }


            //------------------------------------------------------------
            Trace.TraceInformation("▼Trace の Fail 系メソッド");

            using (new Cleanup(M_Indent, M_Unindent))
            {                                                           //// using：前処理=インデント開始、後処理=インデント終了
                Trace.Fail("Traceフェイル");
                Trace.Fail("Traceフェイル", "詳細メッセージ");

                Trace.Assert(false);
                Trace.Assert(false, "Traceアサート");
                Trace.Assert(false, "Traceアサート", "詳細メッセージ");

                Trace.Write("文字列出力(未完)、"); Trace.Assert(false);
                Trace.Write("文字列出力(未完)、"); Trace.Assert(false, "Traceアサート");
                Trace.Write("文字列出力(未完)、"); Trace.Assert(false, "Traceアサート", "詳細メッセージ");
                // ＜メモ＞書式指定タイプ：Debug.Assert にはあるが、Trace.Assert にはない
            }
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【TraceListener の既定動作の仕様確認テスト】
        /// </summary>
        //--------------------------------------------------------------------------------
#if DEBUG   // TraceListener の既定動作の仕様確認テスト
        [ManualTestMethod("(TraceListener の既定動作の仕様確認)")]
        public static void ZZZ_TraceListenerTest()
        {
            // ZZZTraceListener.TestWriteContinue();
            // ZZZTraceListener.TestTraceSource();
            ZZZTraceListener.TestDebug();
        }
#endif

    }
}
