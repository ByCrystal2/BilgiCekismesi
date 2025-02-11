using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayMenuClip();
    }
}
