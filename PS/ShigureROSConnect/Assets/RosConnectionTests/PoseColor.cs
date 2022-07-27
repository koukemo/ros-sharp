using UnityEngine;

[System.Serializable]
public class PoseColors
{
    public static readonly Color nose = new Color(255, 0, 85);
    public static readonly Color neck = new Color(255, 0, 0);

    public static readonly Color right_shoulder = new Color(255, 85, 0);
    public static readonly Color right_elbow = new Color(255, 170, 0);
    public static readonly Color right_wrist = new Color(255, 255, 0);

    public static readonly Color left_shoulder = new Color(170, 255, 0);
    public static readonly Color left_elbow = new Color(85, 255, 0);
    public static readonly Color left_wrist = new Color(0, 255, 0);

    public static readonly Color mid_hip = new Color(255, 0, 0);

    public static readonly Color right_hip = new Color(0, 255, 85);
    public static readonly Color right_knee = new Color(0, 255, 170);
    public static readonly Color right_ankle = new Color(0, 255, 255);

    public static readonly Color left_hip = new Color(0, 170, 255);
    public static readonly Color left_knee = new Color(0, 85, 255);
    public static readonly Color left_ankle = new Color(0, 0, 255);

    public static readonly Color right_eye = new Color(255, 0, 170);
    public static readonly Color left_eye = new Color(170, 0, 255);
    public static readonly Color right_ear = new Color(255, 0, 255);
    public static readonly Color left_ear = new Color(85, 0, 255);

    public static readonly Color left_big_toe = new Color(0, 0, 255);
    public static readonly Color left_small_toe = new Color(0, 0, 255);
    public static readonly Color left_heel = new Color(0, 0, 255);

    public static readonly Color right_big_toe = new Color(0, 255, 255);
    public static readonly Color right_small_toe = new Color(0, 255, 255);
    public static readonly Color right_heel = new Color(0, 255, 255);

    public Color[] pose_color_list()
    {
        Color[] color_list = new Color[25];

        color_list[0] = nose;
        color_list[1] = neck;

        color_list[2] = right_shoulder;
        color_list[3] = right_elbow;
        color_list[4] = right_wrist;

        color_list[5] = left_shoulder;
        color_list[6] = left_elbow;
        color_list[7] = left_wrist;

        color_list[8] = mid_hip;

        color_list[9] = right_hip;
        color_list[10] = right_knee;
        color_list[11] = right_ankle;

        color_list[12] = left_hip;
        color_list[13] = left_knee;
        color_list[14] = left_ankle;

        color_list[15] = right_eye;
        color_list[16] = left_eye;
        color_list[17] = right_ear;
        color_list[18] = left_ear;

        color_list[19] = left_big_toe;
        color_list[20] = left_small_toe;
        color_list[21] = left_heel;

        color_list[22] = right_big_toe;
        color_list[23] = right_small_toe;
        color_list[24] = right_heel;

        return color_list;
    }
}
