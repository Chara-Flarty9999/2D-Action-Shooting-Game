using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    /// <summary>初期動作を設定できる。</summary>
    [SerializeField] liftmode liftmoving = default;
    int _liftmode;
    [SerializeField] bool playOnCollision = default;
    [SerializeField] float m_animSpeed = default;
    AudioSource m_audioSource;
    Animator m_anim = default;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        _liftmode = (int)liftmoving;
        Debug.Log(_liftmode);
        m_anim.SetInteger("Liftmoving", _liftmode);
        
    }

    // Update is called once per frame
    void Update()
    {
        m_anim.SetBool("PlayOnCollision", playOnCollision);
        m_anim.speed = m_animSpeed;
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

    public void SwitchOn()
    {
        playOnCollision = false;
    }

    public void LiftSound()
    {
        m_audioSource.Play();
    }

    enum liftmode
    {
        /// <summary>
        /// 上に動く。
        /// </summary>
        Up_RightDown,
        /// <summary>
        /// 下に動く。
        /// </summary>
        Down_LeftDown,
        /// <summary>
        /// 左に動く。
        /// </summary>
        Left_LeftUp,
        /// <summary>
        /// 右に動く。
        /// </summary>
        Right_RightUp,
    }
}
