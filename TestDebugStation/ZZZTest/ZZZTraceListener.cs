// @(h)ZZZTraceListener.cs ver 0.00 ( '22.05.03 Nov-Lab ) 作成開始
// @(h)ZZZTraceListener.cs ver 0.11 ( '22.05.04 Nov-Lab ) DebugStationTraceListener を作るために必要な分を調査終了
// @(h)ZZZTraceListener.cs ver 0.12 ( '22.05.10 Nov-Lab ) カテゴリ名指定パターンを調査終了
// @(h)ZZZTraceListener.cs ver 0.12a( '22.05.25 Nov-Lab ) その他  ：コメント整理

// @(s)
// 　【TraceListener の既定動作の仕様確認テスト用】

using System;
using System.Diagnostics;
using System.Collections.Generic;


// ＜TraceSourceクラス＞リリースビルドでも有効
// ・TraceTransfer   ：トレース転送メッセージ出力：イベントの種類は Transfer、メッセージ文字列は GUID の文字列表現 となる。
// ・TraceEvent      ：トレースイベント出力。    ：イベントの種類、イベント識別子、メッセージ文字列を指定してトレース情報を出力する。
// ・TraceData       ：トレースデータ出力。      ：イベントの種類、イベント識別子、オブジェクトを指定してトレース情報を出力する。
// ・TraceInformation：情報メッセージ出力        ：TraceEventの簡易版。イベントの種類は Information、イベント識別子は 0 となる。

// ＜Debugクラス＞デバッグビルドのみ有効
// ・Assert          ：フェイルメッセージ表示(条件付き)：メッセージボックス表示は DefaultTraceListener が行う。
// ・Fail            ：フェイルメッセージ表示          ：メッセージボックス表示は DefaultTraceListener が行う。
// ・Print           ：文字列形式出力(終端)            ：WriteLineと同等
// ・Write/If        ：文字列形式出力(続きあり)
// ・WriteLine/If    ：文字列形式出力(終端)

// ＜Traceクラス＞リリースビルドでも有効
// ・Assert          ：フェイルメッセージ表示(条件付き)：メッセージボックス表示は DefaultTraceListener が行う。
// ・Fail            ：フェイルメッセージ表示          ：メッセージボックス表示は DefaultTraceListener が行う。
// ・TraceError      ：エラーメッセージ出力
// ・TraceInformation：情報メッセージ出力
// ・TraceWarning    ：警告メッセージ出力
// ・Write/If        ：文字列形式出力(続きあり)
// ・WriteLine/If    ：文字列形式出力(終端)
// ※TraceEventType に Critical, Verbose, Start, Stop, Suspend, Resume が設定されるメソッドはない

// ・カテゴリ名指定は Debugクラスと Traceクラスの Write系メソッドにのみ用意されている。

#if DEBUG

namespace NovLab.ZZZTest
{
    // ＜メモ＞
    // ・DebugStationTraceListener を作るために必要な分だけを調査したので、すべては網羅していない。

    //====================================================================================================
    /// <summary>
    /// 【TraceListener の既定動作の仕様確認テスト用】
    /// </summary>
    //====================================================================================================
    public class ZZZTraceListener : TraceListener
    {
        //====================================================================================================
        // static フィールド
        //====================================================================================================
        /// <summary>
        /// 【テスト用シングルトンインスタンス】
        /// </summary>
        protected static ZZZTraceListener m_singleton = new ZZZTraceListener();


        //====================================================================================================
        // コンストラクター
        //====================================================================================================

        //--------------------------------------------------------------------------------
        /// <summary>
        /// 【既定のコンストラクター】既定の内容で新しいインスタンスを初期化します。
        /// </summary>
        //--------------------------------------------------------------------------------
        public ZZZTraceListener()
        {
            //------------------------------------------------------------
            /// 既定の内容で新しいインスタンスを初期化する
            //------------------------------------------------------------
            Name = nameof(ZZZTraceListener);                            //// トレースリスナー名 = クラス名
        }


        //====================================================================================================
        // TraceListener メソッドのオーバーライド(文字列形式出力系)
        //====================================================================================================

        //--------------------------------------------------------------------------------
        // 文字列形式出力(未完・メッセージ文字列指定)
        // ・Debug/Trace.Write から呼び出される。
        // ・TraceEvent などでヘッダ部分を書き出すために呼び出される。
        // ・オーバーライド必須メソッドであり、抽象基本メンバーを呼び出すことはできない。
        //--------------------------------------------------------------------------------
        public override void Write(string message)
        {
            M_WriteIndent();
            Console.WriteLine("Write(msg):" + message.XRemoveNewLineChars());
        }


        //--------------------------------------------------------------------------------
        // 文字列形式出力(終端・メッセージ文字列指定)
        // ・Debug.Print や Debug/Trace.WriteLine から呼び出される。
        // ・TraceEvent などで本体部分を書き出すために呼び出される。
        // ・オーバーライド必須メソッドであり、抽象基本メンバーを呼び出すことはできない。
        //--------------------------------------------------------------------------------
        public override void WriteLine(string message)
        {
            M_WriteIndent();
            Console.WriteLine("WriteLine(msg):" + message.XRemoveNewLineChars());
        }


        //--------------------------------------------------------------------------------
        // 文字列形式出力(未完・メッセージ文字列＋カテゴリ名指定)
        // ・Debug/Trace.Write から呼び出される。
        //--------------------------------------------------------------------------------
        public override void Write(string message, string category)
        {
            M_WriteIndent();
            Console.WriteLine("Write(msg, cat):" + category.XRemoveNewLineChars() + " - " + message.XRemoveNewLineChars());

            IndentLevel++;
            base.Write(message, category);
            IndentLevel--;
        }


        //--------------------------------------------------------------------------------
        // 文字列形式出力(終端・メッセージ文字列＋カテゴリ名指定)
        // ・Debug/Trace.Write から呼び出される。
        //--------------------------------------------------------------------------------
        public override void WriteLine(string message, string category)
        {
            M_WriteIndent();
            Console.WriteLine("WriteLine(msg, cat):" + category.XRemoveNewLineChars() + " - " + message.XRemoveNewLineChars());

            IndentLevel++;
            base.WriteLine(message, category);
            IndentLevel--;
        }


        //====================================================================================================
        // TraceListener メソッドのオーバーライド(フェイルメッセージ出力系)
        //====================================================================================================

        //--------------------------------------------------------------------------------
        // ・Fail(msg) や Assert(msg) から呼び出される。
        //--------------------------------------------------------------------------------
        public override void Fail(string message)
        {
            M_WriteIndent();
            Console.WriteLine("Fail(msg)");

            IndentLevel++;
            base.Fail(message);
            IndentLevel--;
        }


        //--------------------------------------------------------------------------------
        // ・Fail(msg, detail) や Assert(msg, detail) から呼び出される。
        //--------------------------------------------------------------------------------
        public override void Fail(string message, string detailMessage)
        {
            M_WriteIndent();
            Console.WriteLine("Fail(msg, detail)");

            IndentLevel++;
            base.Fail(message, detailMessage);
            IndentLevel--;
        }


        //====================================================================================================
        // TraceListener メソッドのオーバーライド(トレースイベント出力系)
        //====================================================================================================

        //--------------------------------------------------------------------------------
        // ・省略可能。既定の動作では TraceEvent(メッセージ指定) を呼び出す。
        // ・TraceSource.TraceEvent(メッセージなし) から呼び出される。
        //--------------------------------------------------------------------------------
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            M_WriteIndent();
            Console.WriteLine("TraceEvent():" + M_GetOutlineString(source, eventCache, eventType));

            IndentLevel++;
            base.TraceEvent(eventCache, source, eventType, id);
            IndentLevel--;
        }


        //--------------------------------------------------------------------------------
        // ・TraceSource.TraceEvent(メッセージ指定) や Trace.TraceInformation/Warning/Error(メッセージ指定) から呼び出される。
        //--------------------------------------------------------------------------------
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            M_WriteIndent();
            Console.WriteLine("TraceEvent(msg):" + M_GetOutlineString(source, eventCache, eventType));

            IndentLevel++;
            base.TraceEvent(eventCache, source, eventType, id, message);
            IndentLevel--;
        }


        //--------------------------------------------------------------------------------
        // ・省略不可
        // ・TraceSource.TraceEvent(書式指定) や Trace.TraceInformation/Warning/Error(書式指定) から呼び出される。
        //--------------------------------------------------------------------------------
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            M_WriteIndent();
            Console.WriteLine("TraceEvent(fmt):" + M_GetOutlineString(source, eventCache, eventType));

            IndentLevel++;
            base.TraceEvent(eventCache, source, eventType, id, format, args);
            IndentLevel--;
        }


        //====================================================================================================
        // TraceListener メソッドのオーバーライド(トレースデータ出力系)
        //====================================================================================================

        //--------------------------------------------------------------------------------
        // TraceSource.TraceData(object) から呼び出される。
        //--------------------------------------------------------------------------------
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            M_WriteIndent();
            Console.WriteLine("TraceData(obj):" + M_GetOutlineString(source, eventCache, eventType));

            IndentLevel++;
            base.TraceData(eventCache, source, eventType, id, data);
            IndentLevel--;

        }


        //--------------------------------------------------------------------------------
        // TraceSource.TraceData(object[]) から呼び出される。
        //--------------------------------------------------------------------------------
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            M_WriteIndent();
            Console.WriteLine("TraceData(obj[]):" + M_GetOutlineString(source, eventCache, eventType));

            IndentLevel++;
            base.TraceData(eventCache, source, eventType, id, data);
            IndentLevel--;
        }


        //--------------------------------------------------------------------------------
        // TraceSource.TraceTransfer から呼び出される。
        //--------------------------------------------------------------------------------
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            M_WriteIndent();
            Console.WriteLine("TraceTransfer:" + M_GetOutlineString(source, eventCache, TraceEventType.Transfer));

            IndentLevel++;
            base.TraceTransfer(eventCache, source, id, message, relatedActivityId);
            IndentLevel--;
        }


        //--------------------------------------------------------------------------------
        // 必須ではないと思われる。Debug, Trace, TraceSource の各メソッドからは呼び出されないらしい。
        //--------------------------------------------------------------------------------
        public override void Flush()
        {
            M_WriteIndent();
            Console.WriteLine("Flush");

            base.Flush();
        }


        //--------------------------------------------------------------------------------
        // 必須ではないと思われる。Debug, Trace, TraceSource の各メソッドからは呼び出されないらしい。
        //--------------------------------------------------------------------------------
        protected override void WriteIndent()
        {
            M_WriteIndent();
            Console.WriteLine("WriteIndent");

            base.WriteIndent();
        }



        //====================================================================================================
        // 内部メソッド
        //====================================================================================================
        protected string M_GetOutlineString(string source, TraceEventCache eventCache, TraceEventType eventType)
        {
            return source + "[" + eventCache.ProcessId + "," + eventCache.ThreadId + "]" + eventCache.DateTime.ToLocalTime() + "," + eventType;
        }

        protected void M_WriteIndent()
        {
            if (IndentLevel <= 0)
            {
                return;
            }

            for (int cnt = 1; cnt < IndentLevel; cnt++)
            {
                Console.Write("--");
            }
            Console.Write("->");
        }


        //====================================================================================================
        // 既定動作の仕様確認用 static メソッド
        //====================================================================================================

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Write に続けて TraceInformation を呼び出した場合、メッセージが結合されて出力される。<br></br>
        /// "メッセージ：category1: message1category2: message2TestDebugStation.exe Information: 0 : 情報メッセージ"
        /// </summary>
        //--------------------------------------------------------------------------------
        public static void TestWriteContinue()
        {
            Trace.Write("メッセージ：");
            Trace.Write("message1", "category1");
            Trace.Write("message2", "category2");
            Trace.TraceInformation("情報メッセージ");
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// TraceSource クラスからの呼び出しテスト
        /// </summary>
        //--------------------------------------------------------------------------------
        public static void TestTraceSource()
        {
            // TraceSource メソッド ：既定の動作                              ：コンソールへの出力内容
            //--------------------------------------------------------------------------------------------------------------
            // TraceTransfer        ：TraceTransfer -> TraceEvent(msg) -> W+WL："MySource Transfer: 135 : MyTransfer, relatedActivityId=00000000-0000-0000-0000-000000000000"
            // TraceData(obj)       ：TraceData(obj)   -> Write+WriteLine     ："MySource Verbose: 123 : [key, value]"
            // TraceData(obj)       ：TraceData(obj)   -> Write+WriteLine     ："MySource Verbose: 456 : System.ArgumentException: 値が有効な範囲にありません。"
            // TraceData(obj[])     ：TraceData(obj[]) -> Write+WriteLine     ："MySource Verbose: 789 : [key, value], System.ArgumentException: 値が有効な範囲にありません。"
            // TraceEvent()         ：TraceEvent() -> TraceEvent(msg) -> W+WL ："MySource Verbose: 123 : "
            // TraceEvent(msg)      ：TraceEvent(msg) -> Write+WriteLine      ："MySource Verbose: 456 : Message"
            // TraceEvent(fmt)      ：TraceEvent(fmt) -> Write+WriteLine      ："MySource Verbose: 789 : 1,2"
            // TraceInformation(fmt)：TraceEvent(fmt) -> Write+WriteLine      ："MySource Information: 0 : Message"
            // TraceInformation(fmt)：TraceEvent(fmt) -> Write+WriteLine      ："MySource Information: 0 : 1,2"

            var tmpKVP = new KeyValuePair<string, string>("key", "value");
            var tmpEx = new ArgumentException();


            var ts = new TraceSource("MySource", SourceLevels.All);
            ts.Listeners.Add(m_singleton);


            ts.TraceTransfer(135, "MyTransfer", new Guid());

            ts.TraceData(TraceEventType.Verbose, 123, tmpKVP);
            ts.TraceData(TraceEventType.Verbose, 456, tmpEx);
            ts.TraceData(TraceEventType.Verbose, 789, tmpKVP, tmpEx);

            ts.TraceEvent(TraceEventType.Verbose, 123);
            ts.TraceEvent(TraceEventType.Verbose, 456, "Message");
            ts.TraceEvent(TraceEventType.Verbose, 789, "{0:d},{1:d}", 1, 2);

            ts.TraceInformation("Message");
            ts.TraceInformation("{0:d},{1:d}", 1, 2);
        }


        //--------------------------------------------------------------------------------
        /// <summary>
        /// Debug クラスからの呼び出しテスト
        /// </summary>
        //--------------------------------------------------------------------------------
        public static void TestDebug()
        {
            // Debugメソッド      ：既定の動作                                  ：コンソールへの出力内容
            //---------------------------------------------------------------------------------------------------
            // Print(msg)         ：WriteLine(msg)                              ："デバッグ出力"
            // Print(fmt)         ：WriteLine(msg)                              ："123 to 456"
            // Write(msg)         ：Write(msg)                                  ："文字列出力、"(改行なし)
            // Write(msg, cat)    ：Write(msg, cat)->Write(msg)                 ："カテゴリ: 文字列出力、"(改行なし)
            // WriteLine(msg)     ：WriteLine(msg)                              ："終端付き文字列出力"
            // WriteLine(msb, cat)：WriteLine(msg, cat)->WriteLine(msg)         ："カテゴリ: 終端付き文字列出力"
            // WriteLine(fmt)     ：WriteLine(msg)                              ："123 to 456"
            // Fail(msg)          ：Fail(msg)->Fail(msg, detail)->WriteLine(msg)："失敗: フェイル"
            // Fail(msg, detail)  ：Fail(msg, detail)->WriteLine(msg)           ："失敗: フェイル 詳細メッセージ"
            // Assert()           ：Fail(msg)->Fail(msg, detail)->WriteLine(msg)："失敗: "
            // Assert(msg)        ：Fail(msg)->Fail(msg, detail)->WriteLine(msg)："失敗: アサート"
            // Assert(msg, detail)：Fail(msg, detail)->WriteLine(msg)           ："失敗: アサート 詳細"
            // Assert(fmt)        ：Fail(msg, detail)->WriteLine(msg)           ："失敗: アサート 123 to 456"
            //
            // 後回し：Write(obj), Write(obj, cat), WriteLine(obj), WriteLine(obj, cat)
            // 省略  ：WriteIf, WriteLineIf
            try
            {
                Debug.Listeners.Add(m_singleton);

                Debug.Print("デバッグ出力");
                Debug.Print("{0:d} to {1:d}", 123, 456);

                Debug.Write("文字列出力、");
                Debug.WriteLine("終端付き文字列出力");
                Debug.WriteLine("{0:d} to {1:d}", 123, 456);

                // カテゴリ付きの書式指定タイプはない。
                Debug.Write("文字列出力、", "カテゴリ");
                Debug.WriteLine("終端付き文字列出力", "カテゴリ");

#if false   // 省略したいときは false
                Debug.Fail("フェイル");
                Debug.Fail("フェイル", "詳細メッセージ");

                Debug.Assert(false);
                Debug.Assert(false, "アサート");
                Debug.Assert(false, "アサート", "詳細");
                Debug.Assert(false, "アサート", "{0:d} to {1:d}", 123, 456);
#endif
            }
            finally
            {
                Debug.Listeners.Remove(m_singleton);
            }
        }
    }
}

#endif
