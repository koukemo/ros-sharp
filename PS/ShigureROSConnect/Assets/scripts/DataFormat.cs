using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 保存データフォーマット
[Serializable]
public class DataFormat
{
    public List<ROSIPData> MarkDataList;
}

// 記録IP
[Serializable]
public class ROSIPData
{
    // IPアドレス
    public string ip_address;

    // ポート
    public string port;
}