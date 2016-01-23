﻿using UnityEngine;
using System.Collections;

public enum Colortype
{
    White,
    Pink,
    Blue,
    Green,
    Yellow
}

public class Weapon : MonoBehaviour {

    //Weapon Control Variables
    Vector2 MousePos = Vector2.zero;

    //Weapon Variables
    Colortype type;

    void Start()
    {
        type = Colortype.White;
    }
    void Update()
    {
        ReadyWeapon();
    }
    void FixedUpdate()
    {
        this.Aim();
    }
    ///<summary>
    /// Method SetType sets the weaponType type
    ///</summary>
    public void SetType(Colortype type)
    {
        this.type = type;
    }
    ///<summary>
    /// Method Aim will aim the weapon toward the verticle position of the mouse
    ///</summary>
    void Aim()
    {
        MousePos = Camera.main.GetComponent<CameraFollow>().GetMousePos();
        float arrowAngle = AngleLookAt(MousePos, transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, arrowAngle)), Time.deltaTime * 5);
    }
    ///<summary>
    /// Method AngleLookAt returns a float value for the Euler Angle
    ///</summary>
    public float AngleLookAt(Vector3 p1, Vector3 p2)
    {
        float deltaY = p2.y - p1.y;
        float deltaX = p2.x - p1.x;
        return Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI + 180;
    }
    void ReadyWeapon()
    {
        if (Input.GetButton("Fire1"))
        {
            switch (type)
            {
                case Colortype.Blue:
                    //idk what blue do
                    Debug.Log("Shoot blue");
                    break;
                case Colortype.Green:
                    //idk what green do
                    Debug.Log("Shoot green");
                    break;
                case Colortype.Pink:
                    //idk what pink do
                    Debug.Log("Shoot pink");
                    break;
                case Colortype.Yellow:
                    //idk what yellow do
                    Debug.Log("Shoot yellow");
                    break;
                case Colortype.White:
                    //Not working, just puffs out smoke
                    Debug.Log("Shoot white");
                    break;
                default: Debug.Log("Gun is broken"); break;
            }
        }
    }
}
