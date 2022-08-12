using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monosingleton<T>:MonoBehaviour where T:Monosingleton<T>
{

}