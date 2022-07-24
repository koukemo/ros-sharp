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
    private GameObject console;

    private bool create_flag = true;
    private GameObject parentObject;
    private GameObject _prefabBodyPart;
    private GameObject _prefabEdge;
    public UnityPoseList unityPoseList;


    protected override void Start() 
    {
        _prefabBodyPart = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _prefabBodyPart.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        _prefabBodyPart.name = "_prefabBodyPart";
        _prefabEdge = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _prefabEdge.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        _prefabEdge.name = "_prefabEdge";

        console = GameObject.Find("Console");
        console.SetActive(false);

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
        console.SetActive(true);

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
        gameObject.GetComponent<Renderer>().material.color = body_part_info_list.color;
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

    private void MoveBodyPart()
    {
        GameObject bodypart;
        Vector3 now_pos = transform.position;
        for (int i = 0; i < bodyPartInfos.Length; i++)
        {
            bodypart = GameObject.Find(bodyPartInfos[i].part_name);
            now_pos.x = bodyPartInfos[i].x / 500;
            now_pos.y = -1 * bodyPartInfos[i].y / 500;
            now_pos.z = bodyPartInfos[i].z / 500;

            if (bodyPartInfos[i].x == 0.0f && bodyPartInfos[i].y == 0.0f && bodyPartInfos[i].z == 0.0f)
            {
                bodypart.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            } else
            {
                if (bodypart.transform.localScale == Vector3.zero)
                {
                    bodypart.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                bodypart.transform.position = now_pos;
            }
            
        }
    }

    private void MoveEdge(GameObject edge, GameObject part1, GameObject part2)
    {
        // 位置の調整
        float distance = Vector3.Distance(part1.transform.position, part2.transform.position);
        edge.transform.localScale = new Vector3(edge.transform.localScale.x, edge.transform.localScale.y, distance);  // エッジの長さを変更

        edge.transform.position = (part1.transform.position + part2.transform.position) * 0.5f;   // エッジの中心を変更

        edge.transform.LookAt(part1.transform);   // エッジの角度を変更

        unityPoseList.EdgeActiveControls();
    }

    

    private void CreateEdgeSet()
    {
        unityPoseList.find_bodypart();
        // face 
        CreateEdge(unityPoseList.nose, unityPoseList.left_eye);
        CreateEdge(unityPoseList.nose, unityPoseList.right_eye);
        CreateEdge(unityPoseList.left_eye, unityPoseList.left_ear);
        CreateEdge(unityPoseList.right_eye, unityPoseList.right_ear);

        // body
        CreateEdge(unityPoseList.nose, unityPoseList.neck);
        CreateEdge(unityPoseList.neck, unityPoseList.mid_hip);

        // arm
        CreateEdge(unityPoseList.neck, unityPoseList.left_shoulder);
        CreateEdge(unityPoseList.neck, unityPoseList.right_shoulder);
        CreateEdge(unityPoseList.left_shoulder, unityPoseList.left_elbow);
        CreateEdge(unityPoseList.right_shoulder, unityPoseList.right_elbow);
        CreateEdge(unityPoseList.left_elbow, unityPoseList.left_wrist);
        CreateEdge(unityPoseList.right_elbow, unityPoseList.right_wrist);

        // leg
        CreateEdge(unityPoseList.mid_hip, unityPoseList.left_hip);
        CreateEdge(unityPoseList.mid_hip, unityPoseList.right_hip);
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

    private void MoveEdgeSet()
    {
        unityPoseList.find_bodypart();
        unityPoseList.find_bodypart_edge();
        // face 
        MoveEdge(unityPoseList.noseTOleft_eye, unityPoseList.nose, unityPoseList.left_eye);
        MoveEdge(unityPoseList.noseTOright_eye, unityPoseList.nose, unityPoseList.right_eye);
        MoveEdge(unityPoseList.left_eyeTOleft_ear, unityPoseList.left_eye, unityPoseList.left_ear);
        MoveEdge(unityPoseList.right_eyeTOright_ear, unityPoseList.right_eye, unityPoseList.right_ear);

        // body
        MoveEdge(unityPoseList.noseTOneck, unityPoseList.nose, unityPoseList.neck);
        MoveEdge(unityPoseList.neckTOmid_hip, unityPoseList.neck, unityPoseList.mid_hip);

        // arm
        MoveEdge(unityPoseList.neckTOleft_shoulder, unityPoseList.neck, unityPoseList.left_shoulder);
        MoveEdge(unityPoseList.neckTOright_shoulder, unityPoseList.neck, unityPoseList.right_shoulder);
        MoveEdge(unityPoseList.left_shoulderTOleft_elbow, unityPoseList.left_shoulder, unityPoseList.left_elbow);
        MoveEdge(unityPoseList.right_shoulderTOright_elbow, unityPoseList.right_shoulder, unityPoseList.right_elbow);
        MoveEdge(unityPoseList.left_elbowTOleft_wrist, unityPoseList.left_elbow, unityPoseList.left_wrist);
        MoveEdge(unityPoseList.right_elbowTOright_wrist, unityPoseList.right_elbow, unityPoseList.right_wrist);

        // leg
        MoveEdge(unityPoseList.mid_hipTOleft_hip, unityPoseList.mid_hip, unityPoseList.left_hip);
        MoveEdge(unityPoseList.mid_hipTOright_hip, unityPoseList.mid_hip, unityPoseList.right_hip);
        MoveEdge(unityPoseList.left_hipTOleft_knee, unityPoseList.left_hip, unityPoseList.left_knee);
        MoveEdge(unityPoseList.right_hipTOright_knee, unityPoseList.right_hip, unityPoseList.right_knee);
        MoveEdge(unityPoseList.left_kneeTOleft_ankle, unityPoseList.left_knee, unityPoseList.left_ankle);
        MoveEdge(unityPoseList.right_kneeTOright_ankle, unityPoseList.right_knee, unityPoseList.right_ankle);
        MoveEdge(unityPoseList.left_ankleTOleft_big_toe, unityPoseList.left_ankle, unityPoseList.left_big_toe);
        MoveEdge(unityPoseList.left_ankleTOleft_heel, unityPoseList.left_ankle, unityPoseList.left_heel);
        MoveEdge(unityPoseList.right_ankleTOright_big_toe, unityPoseList.right_ankle, unityPoseList.right_big_toe);
        MoveEdge(unityPoseList.right_ankleTOright_heel, unityPoseList.right_ankle, unityPoseList.right_heel);
        MoveEdge(unityPoseList.left_big_toeTOleft_small_toe, unityPoseList.left_big_toe, unityPoseList.left_small_toe);
        MoveEdge(unityPoseList.right_big_toeTOright_small_toe, unityPoseList.right_big_toe, unityPoseList.right_small_toe);

        unityPoseList.EdgeActiveControls();
    }

    public void Update()
    {
        if(receivedMsg)
        {
            // debug logにpeople_id[0]と, その人の関節点情報を出力
            DebugMessage();

            // Cube Humanoidの骨格作成
            if (create_flag){
                for (int i = 0; i < bodyPartInfos.Length; i++)
                {
                    CreateBodyPart(bodyPartInfos[i]);
                }
                CreateEdgeSet();

                GameObject.Find("_prefabBodyPart").SetActive(false);
                GameObject.Find("_prefabEdge").SetActive(false);
                create_flag = false;
            }

            MoveBodyPart();
            MoveEdgeSet();

            receivedMsg = false;
        }
    }
}
