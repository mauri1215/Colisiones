using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour {
    public Transform Sphere_1;
    public Transform Sphere_2;
    public GameObject Parent;

    float ang = 0.0f;
    float vx1 = 2.0f, vy1 = 2.0f, vx2 = -2.0f, vy2 = -2.0f;
    float px1 = 0.0f, py1 = 0.0f, px2 = 4.0f, py2 = 3.0f;
    float m1 = 1.0f, m2 = 1.0f;
    float e = 1.0f;
    float radio_s = 0.5f;

    void Start() {
        Sphere_1 = this.gameObject.transform.GetChild(0);
        Sphere_2 = this.gameObject.transform.GetChild(1);

        Sphere_1.position = new Vector3(px1, py1, 0);
        Sphere_2.position = new Vector3(px2, py2, 0);
        Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(vx1, vy1, 0));
        Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(vx2, vy2, 0));
    }

    void Update() {
        float aux = 1.0f / (m1 + m2);
        float vp1, vn1, vp2, vn2;

        float distancia = Vector3.Distance(Sphere_1.position, Sphere_2.position);

        Vector3 vel1 = Sphere_1.GetComponent<Sphere>().getVelocidad();
        Vector3 vel2 = Sphere_2.GetComponent<Sphere>().getVelocidad();

        if (distancia <= 2.0 * radio_s) {
            //Angulo del eje de accion al colisionar
            ang = Mathf.Atan2(py1 - py2, px1 - px2);

            //Eje de rotacion de la esfera azul
            vp1 = (vel1.x * Mathf.Cos(ang)) + (vel1.y * Mathf.Sin(ang));
            vn1 = -(vel1.x * Mathf.Sin(ang)) + (vel1.y * Mathf.Cos(ang));

            //Eje de rotacion de la esfera blanca
            vp2 = (vel2.x * Mathf.Cos(ang)) + (vel2.y * Mathf.Sin(ang));
            vn2 = -(vel2.x * Mathf.Sin(ang)) + (vel2.y * Mathf.Cos(ang));

            //Volvemos a calcular las velocidades despues de la colision
            float vp1_new = ((m1 - (e * m2)) * vp1 * aux) + (((1.0f + e) * m2) * vp2 * aux);
            float vp2_new = (((1.0f + e) * m1) * vp1 * aux) + ((m2 - (e * m1)) * vp2 * aux);

            //Eje de rotacion contrario de la esfera azul
            vx1 = (vp1_new * Mathf.Cos(ang)) - (vn1 * Mathf.Sin(ang));
            vy1 = (vp1_new * Mathf.Sin(ang)) + (vn1 * Mathf.Cos(ang));

            //Eje de rotacion contrario de la esfera blanca
            vx2 = (vp2_new * Mathf.Cos(ang)) - (vn2 * Mathf.Sin(ang));
            vy2 = (vp2_new * Mathf.Sin(ang)) + (vn2 * Mathf.Cos(ang));

            //Asignamos las nuevas velocidades a cada esfera
            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(vx1, vy1, 0));
            Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(vx2, vy2, 0));
        }

        //Calculamos las posiciones de las esferas en el eje X
        px1 = px1 + Time.deltaTime * vx1;
        px2 = px2 + Time.deltaTime * vx2;

        //Calculamos las posiciones de las esferas en el eje Y
        py1 = py1 + Time.deltaTime * vy1;
        py2 = py2 + Time.deltaTime * vy2;

        //Asignamos las nuevas posiciones a cada esfera
        Sphere_1.position = new Vector3(px1, py1, 0);
        Sphere_2.position = new Vector3(px2, py2, 0);
    }
}