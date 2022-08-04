using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using HoloToolkit.Unity.InputModule;
using System.Runtime.InteropServices;
using RosSharp.RosBridgeClient.MessageTypes.Tf2;
using RosSharp.RosBridgeClient;
using RosSharp;

public class Tf2Subscriber : MonoBehaviour
{

    private RosSocket rosSocket;
    private string advertise_id;
    private RosSharp.RosBridgeClient.MessageTypes.Std.String message;
    private bool isMessageReceived = true;
    public Dictionary<string, Transform> tfs;
    public Transform camera1;
    private Quaternion _q;
    private Vector3 _v;
    private string _name;
    // Start is called before the first frame update
    void Start()
    {
        rosSocket = GetComponent<RosConnector>().RosSocket;
        //�g�s�b�N����chatter,�^��String
        advertise_id = rosSocket.Subscribe<RosSharp.RosBridgeClient.MessageTypes.Tf2.TFMessage>("/tf_camera", Room_camera1);

        tfs = new Dictionary<string, Transform>();
        tfs.Add("Realsense_1", camera1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Room_camera1(RosSharp.RosBridgeClient.MessageTypes.Tf2.TFMessage message)
    {
        if (tfs.ContainsKey(message.transforms[0].child_frame_id))
        {
            _v = new Vector3((float)message.transforms[0].transform.translation.x,
                (float)message.transforms[0].transform.translation.y,
                (float)message.transforms[0].transform.translation.z);
            _q = new Quaternion((float)message.transforms[0].transform.rotation.x,
                (float)message.transforms[0].transform.rotation.y,
                (float)message.transforms[0].transform.rotation.z,
                (float)message.transforms[0].transform.rotation.w);
            _name = message.transforms[0].child_frame_id;
        }


    }
    public void TFtransform() 
    {
        tfs[_name].localPosition = _v.Ros2Unity();
        tfs[_name].localRotation = _q.Ros2Unity();
    }

}
