using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManagerVanish : MonoBehaviour
{
    /// <summary>初期動作を設定できる。</summary>
    [SerializeField, Tooltip("リフトの移動方向")] liftmode liftmoving = default;
    int _liftmode;
    [SerializeField, Tooltip("プレイヤーが乗った時に作動するか(基本ON)")] bool playOnCollision = default;
    [SerializeField, Tooltip("リフトの移動速度")] float m_animSpeed = default;
    Animator m_anim = default;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        _liftmode = (int)liftmoving;
        Debug.Log(_liftmode);
        m_anim.SetInteger("Liftmoving", _liftmode);
        
    }

    // Update is called once per frame
    void Update()
    {
        m_anim.SetBool("PlayOnCollision", playOnCollision);
        m_anim.speed = m_animSpeed;
/*        if (!playOnCollision)
        {
            playOnCollision = true;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playOnCollision == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                playOnCollision = false;
            }
        }
    }

    private void Reseter()
    {
        playOnCollision = true;
        m_anim.SetBool("Reset?", true);
    }

    private void Starter()
    {
        m_anim.SetBool("Reset?", false);
    }

    enum liftmode
    {
        /// <summary>
        /// 左に動いて消える。
        /// </summary>
        Left,
        /// <summary>
        /// 左上に動く。
        /// </summary>
        LeftUp,
        /// <summary>
        /// 上に動く。
        /// </summary>
        Up,
        /// <summary>
        /// 右上に動く。
        /// </summary>
        RightUp,
        /// <summary>
        /// 右に動く。
        /// </summary>
        Right,
        /// <summary>
        /// 落ちる挙動。
        /// </summary>
        Fall,
    }
}
