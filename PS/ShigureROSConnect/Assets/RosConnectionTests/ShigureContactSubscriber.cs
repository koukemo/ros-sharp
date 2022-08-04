using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;

public class ShigureContactSubscriber : UnitySubscriber<RosSharp.RosBridgeClient.MessageTypes.ShigureCoreRos1.ContactedList>
{
    private bool receivedMsg = false;

    private TrackingObjectCube object_cude_list;

    // 描画オブジェクト
    private GameObject bounding3d;
    private GameObject bounding3d_line;

    protected override void Start() 
    {
        base.Start();
    }

    public void OnClickStart()
    {
        CreateBoundingBox();
        //base.Start();
    }

    protected override void ReceiveMessage(RosSharp.RosBridgeClient.MessageTypes.ShigureCoreRos1.ContactedList message)
    {
        receivedMsg = true;
        
        if (message.contacted_list != null)
        {
            var contacted_list_num = 0;

            object_cude_list.x = message.contacted_list[contacted_list_num].object_cube.x;
            object_cude_list.y = message.contacted_list[contacted_list_num].object_cube.y;
            object_cude_list.z = message.contacted_list[contacted_list_num].object_cube.z;
            object_cude_list.width = message.contacted_list[contacted_list_num].object_cube.width;
            object_cude_list.height = message.contacted_list[contacted_list_num].object_cube.height;
            object_cude_list.depth = message.contacted_list[contacted_list_num].object_cube.depth;
        }
    }

    private void CreateBoundingBoxLine(GameObject prefab_line, string create_line_name, float px, float py, float pz, float qx, float qy, float qz)
    {
        var create_line = Instantiate(prefab_line, Vector3.zero, Quaternion.identity);
        create_line.name = create_line_name;
        create_line.transform.SetParent(GameObject.Find("BoundingBox3D").transform);

        var pos = create_line.transform.position;
        pos.x += px;
        pos.y += py;
        pos.z += pz;

        create_line.transform.position = pos;
        create_line.transform.rotation = Quaternion.Euler(qx, qy, qz);
    }

    private void CreateBoundingBox()
    {
        if (bounding3d == null)
        {
            bounding3d = new GameObject("BoundingBox3D");
        }

        bounding3d_line = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bounding3d_line.transform.localScale = new Vector3(0.1f, 0.003f, 0.003f);
        bounding3d_line.name = "bounding_box_line";

        // Bottom
        CreateBoundingBoxLine(bounding3d_line, "bottom_front", 0f, -0.05f, -0.05f, 0f, 0f, 0f);
        CreateBoundingBoxLine(bounding3d_line, "bottom_left", -0.05f, -0.05f, 0f, 0f, 90f, 0f);
        CreateBoundingBoxLine(bounding3d_line, "bottom_right", 0.05f, -0.05f, 0f, 0f, 90f, 0f);
        CreateBoundingBoxLine(bounding3d_line, "bottom_behind", 0f, -0.05f, 0.05f, 0f, 0f, 0f);

        // Top
        CreateBoundingBoxLine(bounding3d_line, "top_front", 0f, 0.05f, -0.05f, 0f, 0f, 0f);
        CreateBoundingBoxLine(bounding3d_line, "top_left", -0.05f, 0.05f, 0f, 0f, 90f, 0f);
        CreateBoundingBoxLine(bounding3d_line, "top_right", 0.05f, 0.05f, 0f, 0f, 90f, 0f);
        CreateBoundingBoxLine(bounding3d_line, "top_behind", 0f, 0.05f, 0.05f, 0f, 0f, 0f);

        // Side
        CreateBoundingBoxLine(bounding3d_line, "side_front_l", -0.05f, 0f, -0.05f, 0f, 0f, 90f);
        CreateBoundingBoxLine(bounding3d_line, "side_front_r", 0.05f, 0f, -0.05f, 0f, 0f, 90f);
        CreateBoundingBoxLine(bounding3d_line, "side_behind_l", -0.05f, 0f, 0.05f, 0f, 0f, 90f);
        CreateBoundingBoxLine(bounding3d_line, "side_behind_r", 0.05f, 0f, 0.05f, 0f, 0f, 90f);

        bounding3d_line.SetActive(false);
    }

    public void Update()
    {
        if(receivedMsg)
        {
            receivedMsg = false;
        }
    }
}
