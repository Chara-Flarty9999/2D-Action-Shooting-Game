using UnityEngine;
using UnityEngine.UI;

public class PlayerHitbox : MonoBehaviour
{
    float invTime = 1.5f;
    AudioSource m_AudioSource;
    int life = 20;
    public static bool inv = false;
    GameObject m_lifeText;
    GameObject m_gameover;
    GameObject m_bgm;
    GameObject m_spawn;
    public GameObject ButtonUIObj;
    AudioSource m_BGM;
    [SerializeField] AudioClip m_Explo;
    [SerializeField] AudioClip m_Damage;
    SpriteRenderer m_playerRenderer;
    public static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        ButtonUIObj.SetActive(false);
        player = GameObject.Find("Player");
        m_playerRenderer = player.GetComponent<SpriteRenderer>();
        m_AudioSource = GetComponent<AudioSource>();
        m_lifeText = GameObject.Find("HP Text");
        m_gameover = GameObject.Find("Gameover");
        m_bgm = GameObject.Find("BGM");
        m_BGM = m_bgm.GetComponent<AudioSource>();
        m_spawn = GameObject.Find("SpawnArea");
        Text lifeText = m_lifeText.GetComponent<Text>();
        lifeText.text = " HP :" + life.ToString() + " / 20";
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && inv == false)
        {
            life -= 1;
            if (life < 1)
            {
                Destroy(player); Destroy(m_spawn);
                m_AudioSource.PlayOneShot(m_Explo);
                Text gameoverText = m_gameover.GetComponent<Text>();
                gameoverText.text = "GAMEOVER";
                ButtonUIObj.SetActive(true);
                Text lifeText = m_lifeText.GetComponent<Text>();
                lifeText.text = " ";
                Destroy(gameObject,2f);
                m_BGM.mute = true;
            }
            else
            {
                m_playerRenderer.material.color = new Color32(255, 0, 0, 150);
                inv = true;
                m_AudioSource.PlayOneShot(m_Damage);
                Text lifeText = m_lifeText.GetComponent<Text>();
                lifeText.text = " HP :" + life.ToString() + " / 20  ";

                Invoke("Invincible", invTime);
            }
            

        }
    }

    void Invincible()
    {
        m_playerRenderer.material.color = new Color32(255, 0, 0, 255);
        inv = false;
    }

    public void Heal()
    {
        life = 20;
        Text lifeText = m_lifeText.GetComponent<Text>();
        lifeText.text = " HP :" + life.ToString() + " / 20  ";
    }
}
