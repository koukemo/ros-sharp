using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LaunchIPRecorder : MonoBehaviour
{
    // テキスト表示UI
    [SerializeField, Tooltip("テキスト表示UI")]
    private Text p_MessageText;

    // 保存データ参照
    [SerializeField, Tooltip("保存データ参照")]
    private ROSIPLoad p_DataController;


    // Start is called before the first frame update
    void Start()
    {
        // 履歴データを読み込む
        DataFormat historyData = p_DataController.LoadData();

        if (historyData == null)
        {
            historyData = new DataFormat();
            historyData.MarkDataList = new List<ROSIPData>();
        }

        // 起動時刻を記録する
        ROSIPData launchData = new ROSIPData();
        launchData.ip_address = historyData.MarkDataList.Count.ToString();
        launchData.port = "9090";
        historyData.MarkDataList.Add(launchData);

        // UIにデータを表示する
        string message = "";
        foreach(ROSIPData rosIpData in historyData.MarkDataList)
        {
            message += "ws://" + rosIpData.ip_address + ":" + rosIpData.port + Environment.NewLine;
        }
        p_MessageText.text = message;

        // 履歴データを書き込む
        p_DataController.SaveData(historyData);
    }
}