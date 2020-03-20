using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursionTree : MonoBehaviour {
    public Material mat;
    private UnityEngine.UI.Slider my_ang_slider;
    private UnityEngine.UI.Slider my_len_slider;
    public UnityEngine.UI.Slider ang_slider;
    public UnityEngine.UI.Slider len_slider;
    int len = 30;
    float publicang;
    float publiclen;
    // Start is called before the first frame update
    void Start () {
        my_ang_slider = ang_slider.GetComponent<UnityEngine.UI.Slider> ();
        my_len_slider = len_slider.GetComponent<UnityEngine.UI.Slider> ();

    }
    void OnRenderObject () {
        publicang = my_ang_slider.value;
        publiclen = my_len_slider.value;
        drawLine (Screen.width / 2, 0, Screen.width / 2, publiclen * 1.1f);
        branch (Screen.width / 2, publiclen * 1.1f, 90, publiclen);
    }

    void branch (float x1, float y1, float ang, float len) {
        if (len < publiclen / 100) {
            return;
        }
        len *= 0.7f;
        transform.localPosition = new Vector3 (x1, y1, 0);
        transform.localEulerAngles = new Vector3 (0, 0, -1 * publicang);
        float x2 = second_point_x (x1, ang - (publicang * -1), len);
        float y2 = second_point_y (y1, ang - (publicang * -1), len);
        drawLine (x1, y1, x2, y2);
        branch (x2, y2, ang - (publicang * -1), len);

        x2 = second_point_x (x1, ang - publicang, len);
        y2 = second_point_y (y1, ang - publicang, len);
        drawLine (x1, y1, x2, y2);
        branch (x2, y2, ang - publicang, len);

        return;

    }

    void drawLine (float x1, float y1, float x2, float y2) {
        GL.PushMatrix ();
        mat.SetPass (0);
        GL.LoadPixelMatrix ();
        GL.Begin (GL.LINES);
        GL.Color (Color.white);
        GL.Vertex3 (x1, y1, 0);
        GL.Vertex3 (x2, y2, 0);
        GL.End ();
        GL.PopMatrix ();
    }

    float second_point_x (float x1, float ang, float len) {
        ang = (float) (Math.PI / 180.0) * ang;
        return (x1 + (float) (Math.Cos (ang) * len));
    }
    float second_point_y (float y1, float ang, float len) {
        ang = (float) (Math.PI / 180.0) * ang;
        return (y1 + (float) (Math.Sin (ang) * len));
    }
}