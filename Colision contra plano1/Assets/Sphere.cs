using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {
    float vel_x = 0.0f, vel_y = 0.0f, vel_z = 0.0f;
    float pos_x = 0.0f, pos_y = 0.0f, pos_z = 0.0f;

    public Vector3 getVelocidad() {
        return new Vector3(vel_x, vel_y, vel_z);
    }

    public void setVelocidad(Vector3 vel) {
        vel_x = vel.x;
        vel_y = vel.y;
        vel_z = vel.z;
    }

    public Vector3 getPosicion() {
        return new Vector3(pos_x, pos_y, pos_z);
    }

    public void setPosicion(Vector3 pos) {
        pos_x = pos.x;
        pos_y = pos.y;
        pos_z = pos.z;
    }
}