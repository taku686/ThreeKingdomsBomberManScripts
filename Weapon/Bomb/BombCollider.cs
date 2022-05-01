using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollider : MonoBehaviour
{
    private BoxCollider _boxCollider;
    [HideInInspector]
    public GameObject _owner;

    private void Awake()
    {
        _boxCollider = this.gameObject.GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    public void Initialize(GameObject owner)
    {
        _owner = owner;
    }


}
