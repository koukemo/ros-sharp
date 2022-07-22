using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;

public class ShigurePeopleSubscriber : UnitySubscriber<RosSharp.RosBridgeClient.MessageTypes.ShigureCoreRos1.PoseKeyPointsList>
{
    // shigure messages
    private int people_number = 0;
    private string people_id;
    public BodyPartInfo[] bodyPartInfos = new BodyPartInfo[25];
    public PoseColors poseColors;
    private bool receivedMsg = false;

    // 描画オブジェクト
    private bool create_flag = true;
    private GameObject parentObject;
    private GameObject _prefabBodyPart;
    private GameObject _prefabEdge;
    public UnityPoseList unityPoseList;


    protected override void Start() 
    {
        _prefabBodyPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _prefabBodyPart.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _prefabBodyPart.name = "_prefabBodyPart";
        _prefabEdge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _prefabEdge.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        _prefabEdge.name = "_prefabEdge";

        base.Start();
    }

    protected override void ReceiveMessage(RosSharp.RosBridgeClient.MessageTypes.ShigureCoreRos1.PoseKeyPointsList message)
    {
        receivedMsg = true;
        people_id = message.pose_key_points_list[people_number].people_id;

        int part_count = message.pose_key_points_list[people_number].point_data.Length;

        for (int i = 0; i < part_count; i++)
        {
            bodyPartInfos[i].part_name = message.pose_key_points_list[people_number].point_data[i].body_part_name;
            bodyPartInfos[i].x = message.pose_key_points_list[people_number].point_data[i].projection_point.x;
            bodyPartInfos[i].y = message.pose_key_points_list[people_number].point_data[i].projection_point.y;
            bodyPartInfos[i].z = message.pose_key_points_list[people_number].point_data[i].projection_point.z;
            bodyPartInfos[i].score = message.pose_key_points_list[people_number].point_data[i].score;
            bodyPartInfos[i].color = poseColors.pose_color_list()[i];
        }
    }

    private void DebugMessage()
    {
        string bodyPartInfosStr = "";
        for (int i = 0; i < bodyPartInfos.Length; i++)
        {
            bodyPartInfosStr += "  part_name: " + bodyPartInfos[i].part_name + "\n";
            bodyPartInfosStr += "    x: " + bodyPartInfos[i].x.ToString() + "\n";
            bodyPartInfosStr += "    y: " + bodyPartInfos[i].y.ToString() + "\n";
            bodyPartInfosStr += "    z: " + bodyPartInfos[i].z.ToString() + "\n";
            bodyPartInfosStr += "    score: " + bodyPartInfos[i].score.ToString() + "\n";
            //bodyPartInfosStr += "    color: " + bodyPartInfos[i].color.ToString() + "\n";
        }

        Debug.Log(
            "people_id: " + people_id + "\n" + 
            bodyPartInfosStr + "\n" +
            "-----------" + "\n"
        );
    }


    private void CreateBodyPart(BodyPartInfo body_part_info_list)
    {
        if (parentObject == null)
        {
            parentObject = new GameObject("HumanBody");
        }
        GameObject gameObject = Instantiate(_prefabBodyPart, Vector3.zero, Quaternion.identity);
        gameObject.name = body_part_info_list.part_name;
        gameObject.transform.parent = parentObject.transform;
        gameObject.GetComponent<Renderer>().material.color =    body_part_info_list.color;

        create_flag = false;
    }

    private void CreateEdge(GameObject part1, GameObject part2)
    {
        if (parentObject == null)
        {
            parentObject = new GameObject("HumanBody");
        }
        GameObject gameObject = Instantiate(_prefabEdge, Vector3.zero, Quaternion.identity);
        gameObject.name = part1.name + "TO" + part2.name;
        gameObject.GetComponent<Renderer>().material.color = new Color(173, 255, 47); // 色変更

        // 位置の調整
        float distance = Vector3.Distance(part1.transform.position, part2.transform.position);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, distance);  // エッジの長さを変更

        gameObject.transform.position = (part1.transform.position + part2.transform.position) * 0.5f;   // エッジの中心を変更

        gameObject.transform.LookAt(part1.transform);   // エッジの角度を変更

        gameObject.transform.parent = parentObject.transform;
    }

    private void CreateEdgeSet()
    {
        // TODO : Edgeが正しく表示されない
        // face 
        CreateEdge(unityPoseList.nose, unityPoseList.left_eye);
        CreateEdge(unityPoseList.nose, unityPoseList.right_eye);
        CreateEdge(unityPoseList.left_eye, unityPoseList.left_ear);
        CreateEdge(unityPoseList.right_eye, unityPoseList.right_ear);

        // body
        CreateEdge(unityPoseList.nose, unityPoseList.neck);
        CreateEdge(unityPoseList.neck, unityPoseList.background);

        // arm
        CreateEdge(unityPoseList.neck, unityPoseList.left_shoulder);
        CreateEdge(unityPoseList.neck, unityPoseList.right_shoulder);
        CreateEdge(unityPoseList.left_shoulder, unityPoseList.left_elbow);
        CreateEdge(unityPoseList.right_shoulder, unityPoseList.right_elbow);
        CreateEdge(unityPoseList.left_elbow, unityPoseList.left_wrist);
        CreateEdge(unityPoseList.right_elbow, unityPoseList.right_wrist);

        // leg
        CreateEdge(unityPoseList.background, unityPoseList.left_hip);
        CreateEdge(unityPoseList.background, unityPoseList.right_hip);
        CreateEdge(unityPoseList.left_hip, unityPoseList.left_knee);
        CreateEdge(unityPoseList.right_hip, unityPoseList.right_knee);
        CreateEdge(unityPoseList.left_knee, unityPoseList.left_ankle);
        CreateEdge(unityPoseList.right_knee, unityPoseList.right_ankle);
        CreateEdge(unityPoseList.left_ankle, unityPoseList.left_big_toe);
        CreateEdge(unityPoseList.left_ankle, unityPoseList.left_heel);
        CreateEdge(unityPoseList.right_ankle, unityPoseList.right_big_toe);
        CreateEdge(unityPoseList.right_ankle, unityPoseList.right_heel);
        CreateEdge(unityPoseList.left_big_toe, unityPoseList.left_small_toe);
        CreateEdge(unityPoseList.right_big_toe, unityPoseList.right_small_toe);
    }

    public void Update()
    {
        if(receivedMsg)
        {
            // debug logにpeople_id[0]と, その人の関節点情報を出力
            DebugMessage();

            if (create_flag){
                for (int i = 0; i < bodyPartInfos.Length; i++)
                {
                    CreateBodyPart(bodyPartInfos[i]);
                }
                CreateEdgeSet();
            }

            receivedMsg = false;
        }
    }
}
