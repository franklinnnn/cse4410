using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AnimationClip clip;

    private void OnEnable() 
    {
        Invoke("Disable", clip.length);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable() 
    {
        CancelInvoke();
    }
}
