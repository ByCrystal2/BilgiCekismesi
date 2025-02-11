using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    [SerializeField] GameObject Bird;
    [SerializeField] GameObject Stick;

    public static BirdManager instance { get;private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void BirdActivated(bool _active)
    {
        Bird.SetActive(_active);
        Stick.SetActive(_active);
    }
}
