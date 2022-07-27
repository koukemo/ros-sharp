using UnityEngine;

[System.Serializable]
public class UnityPoseList
{
    // Body part
    public GameObject nose;
    public GameObject neck;

    public GameObject right_shoulder;
    public GameObject right_elbow;
    public GameObject right_wrist;

    public GameObject left_shoulder;
    public GameObject left_elbow;
    public GameObject left_wrist;

    public GameObject mid_hip;

    public GameObject right_hip;
    public GameObject right_knee;
    public GameObject right_ankle;

    public GameObject left_hip;
    public GameObject left_knee;
    public GameObject left_ankle;

    public GameObject right_eye;
    public GameObject left_eye;
    public GameObject right_ear;
    public GameObject left_ear;

    public GameObject left_big_toe;
    public GameObject left_small_toe;
    public GameObject left_heel;

    public GameObject right_big_toe;
    public GameObject right_small_toe;
    public GameObject right_heel;

    // Body part edge
    public GameObject noseTOleft_eye;
    public GameObject noseTOright_eye;
    public GameObject left_eyeTOleft_ear;
    public GameObject right_eyeTOright_ear;

    public GameObject noseTOneck;
    public GameObject neckTOmid_hip;

    public GameObject neckTOleft_shoulder;
    public GameObject neckTOright_shoulder;
    public GameObject left_shoulderTOleft_elbow;
    public GameObject right_shoulderTOright_elbow;
    public GameObject left_elbowTOleft_wrist;
    public GameObject right_elbowTOright_wrist;

    public GameObject mid_hipTOleft_hip;
    public GameObject mid_hipTOright_hip;
    public GameObject left_hipTOleft_knee;
    public GameObject right_hipTOright_knee;
    public GameObject left_kneeTOleft_ankle;
    public GameObject right_kneeTOright_ankle;
    public GameObject left_ankleTOleft_big_toe;
    public GameObject left_ankleTOleft_heel;
    public GameObject right_ankleTOright_big_toe;
    public GameObject right_ankleTOright_heel;
    public GameObject left_big_toeTOleft_small_toe;
    public GameObject right_big_toeTOright_small_toe;

    public void find_bodypart() 
    {
        nose = GameObject.Find( "nose" );
        neck = GameObject.Find( "neck" );

        right_shoulder = GameObject.Find( "right_shoulder" );
        right_elbow = GameObject.Find( "right_elbow" );
        right_wrist = GameObject.Find( "right_wrist" );

        left_shoulder = GameObject.Find( "left_shoulder" );
        left_elbow = GameObject.Find( "left_elbow" );
        left_wrist = GameObject.Find( "left_wrist" );

        mid_hip = GameObject.Find( "mid_hip" );

        right_hip = GameObject.Find( "right_hip" );
        right_knee = GameObject.Find( "right_knee" );
        right_ankle = GameObject.Find( "right_ankle" );

        left_hip = GameObject.Find( "left_hip" );
        left_knee = GameObject.Find( "left_knee" );
        left_ankle = GameObject.Find( "left_ankle" );

        right_eye = GameObject.Find( "right_eye" );
        left_eye = GameObject.Find( "left_eye" );
        right_ear = GameObject.Find( "right_ear" );
        left_ear = GameObject.Find( "left_ear" );

        left_big_toe = GameObject.Find( "left_big_toe" );
        left_small_toe = GameObject.Find( "left_small_toe" );
        left_heel = GameObject.Find( "left_heel" );

        right_big_toe = GameObject.Find( "right_big_toe" );
        right_small_toe = GameObject.Find( "right_small_toe" );
        right_heel = GameObject.Find( "right_heel" );
    }

    public void find_bodypart_edge()
    {
        noseTOleft_eye = GameObject.Find("noseTOleft_eye");
        noseTOright_eye = GameObject.Find("noseTOright_eye");
        left_eyeTOleft_ear = GameObject.Find("left_eyeTOleft_ear");
        right_eyeTOright_ear = GameObject.Find("right_eyeTOright_ear");

        noseTOneck = GameObject.Find("noseTOneck");
        neckTOmid_hip = GameObject.Find("neckTOmid_hip");

        neckTOleft_shoulder = GameObject.Find("neckTOleft_shoulder");
        neckTOright_shoulder = GameObject.Find("neckTOright_shoulder");
        left_shoulderTOleft_elbow = GameObject.Find("left_shoulderTOleft_elbow");
        right_shoulderTOright_elbow = GameObject.Find("right_shoulderTOright_elbow");
        left_elbowTOleft_wrist = GameObject.Find("left_elbowTOleft_wrist");
        right_elbowTOright_wrist = GameObject.Find("right_elbowTOright_wrist");

        mid_hipTOleft_hip = GameObject.Find("mid_hipTOleft_hip");
        mid_hipTOright_hip = GameObject.Find("mid_hipTOright_hip");
        left_hipTOleft_knee = GameObject.Find("left_hipTOleft_knee");
        right_hipTOright_knee = GameObject.Find("right_hipTOright_knee");
        left_kneeTOleft_ankle = GameObject.Find("left_kneeTOleft_ankle");
        right_kneeTOright_ankle = GameObject.Find("right_kneeTOright_ankle");
        left_ankleTOleft_big_toe = GameObject.Find("left_ankleTOleft_big_toe");
        left_ankleTOleft_heel = GameObject.Find("left_ankleTOleft_heel");
        right_ankleTOright_big_toe = GameObject.Find("right_ankleTOright_big_toe");
        right_ankleTOright_heel = GameObject.Find("right_ankleTOright_heel");
        left_big_toeTOleft_small_toe = GameObject.Find("left_big_toeTOleft_small_toe");
        right_big_toeTOright_small_toe = GameObject.Find("right_big_toeTOright_small_toe");
    }

    private void PoseActiveControl(GameObject edge, GameObject part1, GameObject part2)
    {
        if (part1.transform.localScale == Vector3.zero || part2.transform.localScale == Vector3.zero)
        {
            edge.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        } else{
            edge.transform.localScale = new Vector3(0.05f, 0.05f, edge.transform.localScale.z);
        }
    }

    public void EdgeActiveControls() 
    {
        find_bodypart();
        find_bodypart_edge();

        // face 
        PoseActiveControl(noseTOleft_eye, nose, left_eye);
        PoseActiveControl(noseTOright_eye, nose, right_eye);
        PoseActiveControl(left_eyeTOleft_ear, left_eye, left_ear);
        PoseActiveControl(right_eyeTOright_ear, right_eye, right_ear);

        // body
        PoseActiveControl(noseTOneck, nose, neck);
        PoseActiveControl(neckTOmid_hip, neck, mid_hip);

        // arm
        PoseActiveControl(neckTOleft_shoulder, neck, left_shoulder);
        PoseActiveControl(neckTOright_shoulder, neck, right_shoulder);
        PoseActiveControl(left_shoulderTOleft_elbow, left_shoulder, left_elbow);
        PoseActiveControl(right_shoulderTOright_elbow, right_shoulder, right_elbow);
        PoseActiveControl(left_elbowTOleft_wrist, left_elbow, left_wrist);
        PoseActiveControl(right_elbowTOright_wrist, right_elbow, right_wrist);

        // leg
        PoseActiveControl(mid_hipTOleft_hip, mid_hip, left_hip);
        PoseActiveControl(mid_hipTOright_hip, mid_hip, right_hip);
        PoseActiveControl(left_hipTOleft_knee, left_hip, left_knee);
        PoseActiveControl(right_hipTOright_knee, right_hip, right_knee);
        PoseActiveControl(left_kneeTOleft_ankle, left_knee, left_ankle);
        PoseActiveControl(right_kneeTOright_ankle, right_knee, right_ankle);
        PoseActiveControl(left_ankleTOleft_big_toe, left_ankle, left_big_toe);
        PoseActiveControl(left_ankleTOleft_heel, left_ankle, left_heel);
        PoseActiveControl(right_ankleTOright_big_toe, right_ankle, right_big_toe);
        PoseActiveControl(right_ankleTOright_heel, right_ankle, right_heel);
        PoseActiveControl(left_big_toeTOleft_small_toe, left_big_toe, left_small_toe);
        PoseActiveControl(right_big_toeTOright_small_toe, right_big_toe, right_small_toe);
    }
}
