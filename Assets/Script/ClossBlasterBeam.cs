using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClossBlasterBeam : MonoBehaviour
{
    [SerializeField] GameObject _beamHitboxX;
    BoxCollider2D _beamColliderX;
    [SerializeField] GameObject _beamHitboxY;
    BoxCollider2D _beamColliderY;

    GameObject _blasterBody;

    SpriteRenderer spriterenderer;
    AudioSource audioSource;
    Animator m_anim;

    ClossBlaster.MoveInfo _moveInfo;
    float _blasterSize;
    int _beamWait;
    int _beamLoop;
    ClossBlaster.BlasterColor _blasterColor;

    [SerializeField] float _clossBlasterSizeRate;

    bool _Start;
    bool _Exit;

    void Awake()
    {
        _Start = true;
    }
    void Start()
    {
        m_anim = GetComponent<Animator>();
        _blasterBody = GameObject.Find("ClossBlaster(Clone)");
        ClossBlaster _blasterSizeInfo = _blasterBody.GetComponent<ClossBlaster>();
        _blasterSize = _blasterSizeInfo._blasterSize;
        _beamColliderX = _beamHitboxX.GetComponent<BoxCollider2D>();
        _beamColliderY = _beamHitboxY.GetComponent<BoxCollider2D>();
        _beamColliderX.size = new Vector2(72, 0.75f * _blasterSize);
        _beamColliderY.size = new Vector2(0.75f * _blasterSize, 98);
        spriterenderer = GetComponent<SpriteRenderer>();

    }

    public void ClossBlasterBeamExit()
    {
        _Exit = true;
        this.spriterenderer.material.DOFade(0f, 1.5f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(_clossBlasterSizeRate, _clossBlasterSizeRate);
        m_anim.SetBool("Start", _Start);
        m_anim.SetBool("Exit", _Exit);
    }
}



//