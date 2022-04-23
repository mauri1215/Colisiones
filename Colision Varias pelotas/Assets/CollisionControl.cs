using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour {
    public Transform Sphere_1;
    public Transform Sphere_2;
    public Transform Sphere_3;
    public Transform Sphere_4;
    public Transform Sphere_5;
    public Transform Sphere_6;
    public GameObject Parent;

    float ang = 0.0f;
    float vx1 = 2.0f, vy1 = 2.0f, vx2 = -2.0f, vy2 = -2.0f;
    float vx3 = 2.0f, vy3 = 0;
    float vx4 = -2.0f, vy4 = -2.0f;
    float vx5 = 2.0f, vy5 = 2.0f;
    float vx6 = -2.0f, vy6 = -2.0f;

    float px1 = 0.0f, py1 = 0.0f, px2 = 4.0f, py2 = 3.0f;
    float px3 = -4.0f, py3 = 3.0f;
    float px4 = 0.0f, py4 = 4.0f;
    float px5 = 0.0f, py5 = -4.0f;
    float px6 = 5.0f, py6 = 0.0f;
    float m1 = 1.0f, m2 = 1.0f;
    float e = 1.0f;
    float radio_s = 0.5f;

    void Start() {
        Sphere_1 = this.gameObject.transform.GetChild(0);
        Sphere_2 = this.gameObject.transform.GetChild(1);
        Sphere_3 = this.gameObject.transform.GetChild(2);
        Sphere_4 = this.gameObject.transform.GetChild(3);
        Sphere_5 = this.gameObject.transform.GetChild(4);
        Sphere_6 = this.gameObject.transform.GetChild(5);

        Sphere_1.position = new Vector3(px1, py1, 0);
        Sphere_2.position = new Vector3(px2, py2, 0);
        Sphere_3.position = new Vector3(px3, py3, 0);
        Sphere_4.position = new Vector3(px4, py4, 0);
        Sphere_5.position = new Vector3(px5, py5, 0);
        Sphere_6.position = new Vector3(px6, py6, 0);
        Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(vx1, vy1, 0));
        Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(vx2, vy2, 0));
        Sphere_3.GetComponent<Sphere>().setVelocidad(new Vector3(vx3, vy3, 0));
        Sphere_4.GetComponent<Sphere>().setVelocidad(new Vector3(vx4, vy4, 0));
        Sphere_5.GetComponent<Sphere>().setVelocidad(new Vector3(vx5, vy5, 0));
        Sphere_6.GetComponent<Sphere>().setVelocidad(new Vector3(vx6, vy6, 0));
    }

    void Update() {

        if (Input.GetKey("escape")) Application.Quit();

        float aux = 1.0f / (m1 + m2);
        float vp1, vn1, vp2, vn2, vp3, vn3, vp4, vn4, vp5, vn5, vp6, vn6;

        float distancia = Vector3.Distance(Sphere_1.position, Sphere_2.position);
        float distancia2 = Vector3.Distance(Sphere_3.position, Sphere_4.position);
        float distancia3 = Vector3.Distance(Sphere_5.position, Sphere_6.position);
        float distancia4 = Vector3.Distance(Sphere_1.position, Sphere_3.position);

        Vector3 vel1 = Sphere_1.GetComponent<Sphere>().getVelocidad();
        Vector3 vel2 = Sphere_2.GetComponent<Sphere>().getVelocidad();
        Vector3 vel3 = Sphere_3.GetComponent<Sphere>().getVelocidad();
        Vector3 vel4 = Sphere_4.GetComponent<Sphere>().getVelocidad();
        Vector3 vel5 = Sphere_5.GetComponent<Sphere>().getVelocidad();
        Vector3 vel6 = Sphere_6.GetComponent<Sphere>().getVelocidad();

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

        if (distancia2 <= 2.0 * radio_s)
        {
            //Angulo del eje de accion al colisionar
            ang = Mathf.Atan2(py3 - py4, px3 - px4);

            //Eje de rotacion de la esfera azul
            vp3 = (vel3.x * Mathf.Cos(ang)) + (vel3.y * Mathf.Sin(ang));
            vn3 = -(vel3.x * Mathf.Sin(ang)) + (vel3.y * Mathf.Cos(ang));

            //Eje de rotacion de la esfera blanca
            vp4 = (vel4.x * Mathf.Cos(ang)) + (vel4.y * Mathf.Sin(ang));
            vn4 = -(vel4.x * Mathf.Sin(ang)) + (vel4.y * Mathf.Cos(ang));

            //Volvemos a calcular las velocidades despues de la colision
            float vp3_new = ((m1 - (e * m2)) * vp3 * aux) + (((1.0f + e) * m2) * vp4 * aux);
            float vp4_new = (((1.0f + e) * m1) * vp3 * aux) + ((m2 - (e * m1)) * vp4 * aux);

            //Eje de rotacion contrario de la esfera azul
            vx3 = (vp3_new * Mathf.Cos(ang)) - (vn3 * Mathf.Sin(ang));
            vy3 = (vp3_new * Mathf.Sin(ang)) + (vn3 * Mathf.Cos(ang));

            //Eje de rotacion contrario de la esfera blanca
            vx4 = (vp4_new * Mathf.Cos(ang)) - (vn4 * Mathf.Sin(ang));
            vy4 = (vp4_new * Mathf.Sin(ang)) + (vn4 * Mathf.Cos(ang));

            //Asignamos las nuevas velocidades a cada esfera
            Sphere_3.GetComponent<Sphere>().setVelocidad(new Vector3(vx3, vy3, 0));
            Sphere_4.GetComponent<Sphere>().setVelocidad(new Vector3(vx4, vy4, 0));
        }

        if (distancia3 <= 2.0 * radio_s)
        {
            //Angulo del eje de accion al colisionar
            ang = Mathf.Atan2(py5 - py6, px5 - px6);

            //Eje de rotacion de la esfera azul
            vp5 = (vel5.x * Mathf.Cos(ang)) + (vel5.y * Mathf.Sin(ang));
            vn5 = -(vel5.x * Mathf.Sin(ang)) + (vel5.y * Mathf.Cos(ang));

            //Eje de rotacion de la esfera blanca
            vp6 = (vel6.x * Mathf.Cos(ang)) + (vel6.y * Mathf.Sin(ang));
            vn6 = -(vel6.x * Mathf.Sin(ang)) + (vel6.y * Mathf.Cos(ang));

            //Volvemos a calcular las velocidades despues de la colision
            float vp5_new = ((m1 - (e * m2)) * vp5 * aux) + (((1.0f + e) * m2) * vp6 * aux);
            float vp6_new = (((1.0f + e) * m1) * vp5 * aux) + ((m2 - (e * m1)) * vp6 * aux);

            //Eje de rotacion contrario de la esfera azul
            vx5 = (vp5_new * Mathf.Cos(ang)) - (vn5 * Mathf.Sin(ang));
            vy5 = (vp5_new * Mathf.Sin(ang)) + (vn5 * Mathf.Cos(ang));

            //Eje de rotacion contrario de la esfera blanca
            vx6 = (vp6_new * Mathf.Cos(ang)) - (vn6 * Mathf.Sin(ang));
            vy6 = (vp6_new * Mathf.Sin(ang)) + (vn6 * Mathf.Cos(ang));

            //Asignamos las nuevas velocidades a cada esfera
            Sphere_5.GetComponent<Sphere>().setVelocidad(new Vector3(vx5, vy5, 0));
            Sphere_6.GetComponent<Sphere>().setVelocidad(new Vector3(vx6, vy6, 0));
        }

        if (distancia4 <= 2.0 * radio_s)
        {
            //Angulo del eje de accion al colisionar
            ang = Mathf.Atan2(py1 - py3, px1 - px3);

            //Eje de rotacion de la esfera azul
            vp1 = (vel1.x * Mathf.Cos(ang)) + (vel1.y * Mathf.Sin(ang));
            vn1 = -(vel1.x * Mathf.Sin(ang)) + (vel1.y * Mathf.Cos(ang));

            //Eje de rotacion de la esfera blanca
            vp3 = (vel3.x * Mathf.Cos(ang)) + (vel3.y * Mathf.Sin(ang));
            vn3 = -(vel3.x * Mathf.Sin(ang)) + (vel3.y * Mathf.Cos(ang));

            //Volvemos a calcular las velocidades despues de la colision
            float vp1_new = ((m1 - (e * m2)) * vp1 * aux) + (((1.0f + e) * m2) * vp3 * aux);
            float vp3_new = (((1.0f + e) * m1) * vp1 * aux) + ((m2 - (e * m1)) * vp3 * aux);

            //Eje de rotacion contrario de la esfera azul
            vx1 = (vp1_new * Mathf.Cos(ang)) - (vn1 * Mathf.Sin(ang));
            vy1 = (vp1_new * Mathf.Sin(ang)) + (vn1 * Mathf.Cos(ang));

            //Eje de rotacion contrario de la esfera blanca
            vx3 = (vp3_new * Mathf.Cos(ang)) - (vn3 * Mathf.Sin(ang));
            vy3 = (vp3_new * Mathf.Sin(ang)) + (vn3 * Mathf.Cos(ang));

            //Asignamos las nuevas velocidades a cada esfera
            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(vx1, vy1, 0));
            Sphere_3.GetComponent<Sphere>().setVelocidad(new Vector3(vx3, vy3, 0));
        }

        //Calculamos las posiciones de las esferas en el eje X
        px1 = px1 + Time.deltaTime * vx1;
        px2 = px2 + Time.deltaTime * vx2;
        px3 = px3 + Time.deltaTime * vx3;
        px4 = px4 + Time.deltaTime * vx4;
        px5 = px5 + Time.deltaTime * vx5;
        px6 = px6 + Time.deltaTime * vx6;

        //Calculamos las posiciones de las esferas en el eje Y
        py1 = py1 + Time.deltaTime * vy1;
        py2 = py2 + Time.deltaTime * vy2;
        py3 = py3 + Time.deltaTime * vy3;
        py4 = py4 + Time.deltaTime * vy4;
        py5 = py5 + Time.deltaTime * vy5;
        py6 = py6 + Time.deltaTime * vy6;

        //Asignamos las nuevas posiciones a cada esfera
        Sphere_1.position = new Vector3(px1, py1, 0);
        Sphere_2.position = new Vector3(px2, py2, 0);
        Sphere_3.position = new Vector3(px3, py3, 0);
        Sphere_4.position = new Vector3(px4, py4, 0);
        Sphere_5.position = new Vector3(px5, py5, 0);
        Sphere_6.position = new Vector3(px6, py6, 0);
    }
}