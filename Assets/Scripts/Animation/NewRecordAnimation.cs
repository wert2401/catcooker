using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecordAnimation : MonoBehaviour
{
    private Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        GameState.Instance.NewRecord += onNewRecord;
    }

    private void onNewRecord()
    {
        anim.Play();
    }
}
