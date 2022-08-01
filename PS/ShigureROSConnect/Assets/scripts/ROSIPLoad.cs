using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CUSTOMTYPE = DataFormat;

public class ROSIPLoad : MonoBehaviour
{
    // 保持データ
    [SerializeField, Tooltip("保持データ")]
    private CUSTOMTYPE p_Data;

    // 保存ファイル名
    [SerializeField, Tooltip("保存ファイル名")]
    private string p_FileName;

    UnityEngine.TouchScreenKeyboard keyboard;
    public static string keyboardText = "";


    /// <summary>
    /// データを読み込む
    /// </summary>
    /// <returns>CUSTOMTYPE</returns>
    public CUSTOMTYPE LoadData()
    {
        return XMLRead(p_FileName);
    }


    /// <summary>
    /// データを書き込む
    /// </summary>
    /// <param name="a_Data"></param>
    public void SaveData(CUSTOMTYPE a_Data)
    {
        XMLWrite(p_FileName, a_Data);
    }


    /// <summary>
    /// データ読み込み(XML)
    /// </summary>
    /// <param name="a_FileName"></param>
    /// <returns>読み込み成否</returns>
    private CUSTOMTYPE XMLRead(string a_FileName)
    {
        // 読み込み成否
        bool ret = false;
        CUSTOMTYPE resultData = null;

        // オブジェクトの型を指定して Serializer オブジェクトを作成する
        System.Xml.Serialization.XmlSerializer serializer;
        serializer = new System.Xml.Serialization.XmlSerializer(typeof(CUSTOMTYPE));

        // ファイルの存在確認
        if (System.IO.File.Exists(XMLFilePath(a_FileName)) == true)
        {
            // 読み込みファイル
            System.IO.StreamReader streamreader;
#if WINDOWS_UWP
            // 読み込みファイルを開く(UWPアプリではStreamReader(filepath)メソッドは使用不可)
            streamreader = new System.IO.StreamReader((System.IO.Stream)System.IO.File.OpenRead(XMLFilePath(a_FileName)));
#else
            // 読み込みファイルを開く
            streamreader = new System.IO.StreamReader(XMLFilePath(a_FileName), new System.Text.UTF8Encoding(false));
#endif

            // XMLファイルから逆シリアル化する
            resultData = (CUSTOMTYPE)serializer.Deserialize(streamreader);

#if WINDOWS_UWP
            // ファイルを閉じる(UWPアプリではClose()メソッドは使用不可)
            streamreader.Dispose();
#else
            // ファイルを閉じる
            streamreader.Close();
#endif

            // 読み込み成功
            ret = true;
        }

        return resultData;
    }


    /// <summary>
    /// データ書き込み(XML)
    /// </summary>
    /// <param name="a_FileName"></param>
    /// <param name="a_Data"></param>
    /// <returns></returns>
    private bool XMLWrite(string a_FileName, CUSTOMTYPE a_Data)
    {
        Debug.Log("XMLWrite");

        // 書き込み成否
        bool ret = false;

        // オブジェクトの型を指定して Serializer オブジェクトを作成する
        System.Xml.Serialization.XmlSerializer serializer;
        serializer = new System.Xml.Serialization.XmlSerializer(typeof(CUSTOMTYPE));

        // ディレクトリの存在確認
        if (System.IO.Directory.Exists(SettingFileDirectoryPath()) == true)
        {
            // 書き込みファイルを開く
            System.IO.StreamWriter streamwriter;
#if WINDOWS_UWP
            // 書き込みファイルを開く(UWPアプリではStreamWriter(filepath)メソッドは使用不可)
            streamwriter = new System.IO.StreamWriter((System.IO.Stream)System.IO.File.OpenWrite(XMLFilePath(a_FileName)));
#else
            // 書き込みファイルを開く
            streamwriter = new System.IO.StreamWriter(XMLFilePath(a_FileName), false, new System.Text.UTF8Encoding(false));
#endif

            // シリアル化してXMLファイルに保存する
            serializer.Serialize(streamwriter, a_Data);

#if WINDOWS_UWP
            // ファイルを閉じる(UWPアプリではClose()メソッドは使用不可)
            streamwriter.Dispose();
#else
            // ファイルを閉じる
            streamwriter.Close();
#endif

            // 書き込み成功
            ret = true;
        }
        return ret;
    }


    /// <summary>
    /// データ保存ディレクトリパス
    /// 実行環境によって参照ディレクトリを変更する
    /// </summary>
    /// <returns></returns>
    private string SettingFileDirectoryPath()
    {
        string directorypath = "";
#if WINDOWS_UWP
        // HoloLens上での動作の場合、LocalAppData/AppName/LocalStateフォルダを参照する
        directorypath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#else
        // Unity上での動作の場合、Assets/StreamingAssetsフォルダを参照する
        directorypath = UnityEngine.Application.streamingAssetsPath;
#endif
        return directorypath;
    }


    /// <summary>
    /// アプリ設定ファイルパス(XML)
    /// 実行環境によって参照ディレクトリを変更する
    /// </summary>
    /// <param name="a_FileName"></param>
    /// <returns></returns>
    private string XMLFilePath(string a_FileName)
    {
        string filepath = "";
        filepath = System.IO.Path.Combine(SettingFileDirectoryPath(), a_FileName);
        return filepath;
    }
    

    /// <summary>
    /// キーボードの利用
    /// </summary>
    public void OpenSystemKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard != null)
        {
            keyboardText = keyboard.text;
            // Do stuff with keyboardText
        }
    }
}
