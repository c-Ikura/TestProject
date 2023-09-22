using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{

    public static Action onWallDisable;
    public static void OnWallDisable()
    {
        onWallDisable?.Invoke();
    }
}
