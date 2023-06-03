using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Action<string> onUpdateWeaponStatus;
    public static Action<bool> onReload;  
}
