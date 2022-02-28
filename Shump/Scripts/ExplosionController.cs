using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AnimationClip clip;

    void OnEnable()
    {
        Invoke("Disable", clip.length);
    }

    void Disable()
    {
        Destroy(gameObject);
    }
}
