using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumExtention
{
    public static int ParseToInt<T>(string uiName) where T : struct
    {
        return (int)Enum.Parse(typeof(T), uiName, true);
    }

    public static T ParseToEnum<T>(string uiName) where T : struct
    {
        return (T)Enum.Parse(typeof(T), uiName, true);
    }
}
