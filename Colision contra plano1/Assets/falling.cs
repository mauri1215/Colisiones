using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling : MonoBehaviour
{
    public GameObject[] sphere;

    float[] x = new float[4];
    float[] y = new float[4];
    float[] z = new float[4];
    
    float[] xm = new float[] {0.001f,-0.001f };//roja y amarilla en X
    float[] zm = new float[] {0, 0, -0.001f, 0.001f };//azul y verde en z

    float Planox = -1, Planoy = -8, Planoz = 3;

    bool[] up = new bool[] { false, false, false, false };//inicio de movimento
    bool[] fall = new bool[] { false, false, false, false };//detccion de plano
    int[] Rebote = new int[] { 500, 500, 500, 500 };//indice de rebote
    int[] Rebotecount = new int[] { 0, 0, 0, 0 };

    float Colx, Colz;
    int i;
    
    void Start()
    {
        for (i = 0; i < 4; i++)
        {
            x[i] = sphere[i].transform.position.x;
            y[i] = sphere[i].transform.position.y;
            z[i] = sphere[i].transform.position.z;
        }
    }

    void Update()
    {

        for (i = 0; i < 4; i++)
        {
            if (i < 2) x[i] += xm[i];
            if (i > 1) z[i] += zm[i];

            sphere[i].transform.position = new Vector3(x[i], y[i], z[i]);

            if (!up[i])
            {
                if ((y[i] > Planoy + 0.5) && !fall[i]) y[i] -= 0.01f;

                if (y[i] < -7.5f && !fall[i]) up[i] = true;
                if (fall[i] && y[i] > -100) y[i] -= 0.01f;
            }

            Colx = Mathf.Abs(x[i] - Planox);
            Colz = Mathf.Abs(z[i] - Planoz);

            if (up[i])
            {
                y[i] += 0.01f * Rebote[i] / 100; 
                Rebote[i]--;

                if ((Colx > 5f || Colz > 5f) && Rebote[i] < 0) { up[i] = false; fall[i] = true; }
                if (Rebote[i] < 0) { up[i] = false; Rebotecount[i]++; Rebote[i] = 500 - Rebotecount[i] * 100; }

            }
        }


    }
}
