using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClossBlasterBeam : MonoBehaviour
{
    [SerializeField] GameObject _beamHitboxX;
    [SerializeField] GameObject _beamHitboxY;

    SpriteRenderer spriterenderer;
    AudioSource audioSource;
    Animator animator;

    ClossBlaster.MoveInfo _moveInfo;
    float _blasterSize;
    int _beamWait;
    int _beamLoop;
    ClossBlaster.BlasterColor _blasterColor;

    [SerializeField] float _clossBlasterSizeRate;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(_blasterSize * _clossBlasterSizeRate, _blasterSize * _clossBlasterSizeRate);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
