using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Function of the numeric keypad
/// </summary>
public class TenKeyInput : MonoBehaviour
{
    [Serializable] public class MyEvent : UnityEvent<string> { }
    [SerializeField] MyEvent _tenKyeevent;

     string _numberField;

    //通常の数値を入力する。
    public void KeyInput(float number)
    {
        _numberField += number.ToString() ;
        _tenKyeevent.Invoke(_numberField);
    }
}