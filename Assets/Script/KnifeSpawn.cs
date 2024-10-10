using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class KnifeSpawn : MonoBehaviour
{

    [SerializeField] UnityEvent _actions;
    public int damage; //特に使ってない

    float y;
    float x;
    
    //ブラスター専用の情報
    /// <summary>ブラスターの初期位置</summary>
    public ClossBlaster.StartInfo f_place;
    /// <summary>ブラスターの移動情報</summary>
    public ClossBlaster.MoveInfo m_place;
    /// <summary>ブラスターのサイズ</summary>
    public float blasterSize;
    /// <summary>ブラスター発射までの遅延時間</summary>
    public int beamWait;
    /// <summary>ブラスターの発射期間(ループ回数)</summary>
    public int beamTime;
    /// <summary>ブラスターの色の指定</summary>
    public ClossBlaster.BlasterColor blasterColor;


    /// <summary>
    /// 直進するナイフ。
    /// </summary>
    [SerializeField] GameObject m_spawnPrefab = default;
    /// <summary>
    /// 進行方向が指定できるナイフ。(上下左右)
    /// </summary>
    [SerializeField] GameObject m_spawn2Prefab = default;
    /// <summary>
    /// 炸裂弾。
    /// </summary>
    [SerializeField] GameObject m_spawn3Prefab = default;
    /// <summary>
    /// クロスブラスター
    /// </summary>
    [SerializeField] GameObject m_clossBlasterPrefab = default;


    public int rote;
    public float blastWaitTime;
    public float magnification;
    GameObject _player;
    public PlayerHitbox life;
    public int movedirection = 0;
    public int bulletNumber;

    [SerializeField] AudioClip _heal;
    AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _player = GameObject.Find("Player");
        life = _player.GetComponent<PlayerHitbox>();
        StartCoroutine("Test");
    }

    IEnumerator Test()
    {
        f_place = new ClossBlaster.StartInfo(-3,-3, 0);
        m_place = new ClossBlaster.MoveInfo(0,0, 0);
        blasterSize = 1.2f;
        beamWait = 1;
        beamTime = 3;
        Instantiate(m_clossBlasterPrefab);
        yield return new WaitForEndOfFrame();
        f_place = new ClossBlaster.StartInfo(3,3, 0);
        m_place = new ClossBlaster.MoveInfo(0,0, 0);
        blasterSize = 1.2f;
        beamWait = 1;
        beamTime = 3;
        Instantiate(m_clossBlasterPrefab);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator Hard()
    {
        magnification = 20f;



        for (int i = 0; i < 30; i++)
        {
             
            //transform.position = Vector3.zero;
            rote = UnityEngine.Random.Range(150, 210);
            blastWaitTime = 0.2f;
            float x = UnityEngine.Random.Range(10.0f, 15.0f);
            y = UnityEngine.Random.Range(-6.0f, 6.0f);
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);

            yield return new WaitForSeconds(0.2f);

        }

        

        yield return new WaitForSeconds(0.4f);
        y = 6;
        for (int i = 0; i < 10; i++)
        {
            rote = 180;
            x = 12;
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);


            yield return new WaitForSeconds(0.1f);
            y -= 1;
        }
        y = -6;
        yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < 10; i++)
        {
            rote = 180;
            x = 12;
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);


            yield return new WaitForSeconds(0.1f);
            y += 1;
        }

        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < 10; i++)
        {
            x = 12;
            y = UnityEngine.Random.Range(-6.0f, 6.0f);
            rote = 200;
            for (int j = 0; j < 10; j++)
            {
                Vector3 spawn = new Vector3(x, y);
                Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                yield return new WaitForEndOfFrame();
                rote -= 5;
            }
            yield return new WaitForSeconds(0.7f);
        }
        x = 12;
        for (int i = 0; i < 5; i++)
        {
            magnification = 8;
            for (int j = 0; j < 2; j++)
            {
                y = 0;
                rote = 90;
                movedirection = 2;
                Vector3 spawn = new Vector3(x, y);
                Instantiate(m_spawn2Prefab, spawn, Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
                y = 6;
                rote = 90;
                movedirection = 2;
                spawn = new Vector3(x, y);
                Instantiate(m_spawn2Prefab, spawn, Quaternion.identity);
                yield return new WaitForEndOfFrame();
                y = -6;
                rote = 90;
                movedirection = 2;
                spawn = new Vector3(x, y);
                Instantiate(m_spawn2Prefab, spawn, Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }
            y = 4;
            magnification = 20;
            for (int m = 0; m < 6; m++)
            {
                rote = 180;
                Vector3 spawn = new Vector3(x, y);
                Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                yield return new WaitForEndOfFrame();
                y -= 0.5f;
            }
            y = -4.5f;
            magnification = 20;
            for (int m = 0; m < 6; m++)
            {
                rote = 180;
                Vector3 spawn = new Vector3(x, y);
                Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                yield return new WaitForEndOfFrame();
                y += 0.5f;
            }
        }
        yield return new WaitForSeconds(0.6f);
        rote = 180;
        for (int m = 0; m < 50; m++)
        {
            x = -12;
            y = 5;
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();
            y = -5;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();
            x = 12;
            y = 5;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();
            y = -5;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            rote -= 35;
        }
        yield return new WaitForSeconds(0.5f);

        for (int m = 0; m <= 25; m++)
        {
            y = UnityEngine.Random.Range(-6.0f, 6.0f);
            x = UnityEngine.Random.Range(-13.0f, 13.0f);
            Vector2 spawn = new Vector2(x, y);
            //Vector2 dir = _player.transform.position - spawn;
            transform.position = spawn;
            this.transform.LookAt(_player.transform);
            Quaternion dir = this.transform.rotation;
            rote = (int)(Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        for (int m = 0; m <= 40; m++)
        {
            x = 13;
            y = UnityEngine.Random.Range(-6.0f, 6.0f);
            Vector2 spawn = new Vector2(x, y);
            rote = 180;
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();
            x = -13;
            y = UnityEngine.Random.Range(-6.0f, 6.0f);
            spawn = new Vector2(x, y);
            rote = 0;
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }

        _AudioSource.PlayOneShot(_heal);
        life.Heal();
        yield return new WaitForSeconds(0.9f);

        for (int i = 0; i < 20; i++)
        {

            //transform.position = Vector3.zero;
            rote = UnityEngine.Random.Range(0, 360);
            blastWaitTime = 0.2f;
            bulletNumber = 8;
            float x = UnityEngine.Random.Range(-12.0f, 12.0f);
            y = UnityEngine.Random.Range(-6.0f, 6.0f);
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);

            yield return new WaitForSeconds(0.4f);

        }

        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < 4; i++)
        {
            float y = PlayerHitbox.player.transform.position.y;
            //transform.position = Vector3.zero;
            rote = UnityEngine.Random.Range(0, 360);
            blastWaitTime = 0.8f;
            bulletNumber = 12;
            float x = PlayerHitbox.player.transform.position.x;
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(1f);


            y = PlayerHitbox.player.transform.position.y;
            x = PlayerHitbox.player.transform.position.x - 5;
            rote = 0;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = PlayerHitbox.player.transform.position.y;
            x = PlayerHitbox.player.transform.position.x + 5;
            rote = 180;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = PlayerHitbox.player.transform.position.y - 5;
            x = PlayerHitbox.player.transform.position.x;
            rote = 90;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = PlayerHitbox.player.transform.position.y + 5;
            x = PlayerHitbox.player.transform.position.x;
            rote = 270;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(0.8f);

            y = PlayerHitbox.player.transform.position.y;
            //transform.position = Vector3.zero;
            rote = UnityEngine.Random.Range(0, 360);
            bulletNumber = 12;
            x = PlayerHitbox.player.transform.position.x;
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(1f);


            y = PlayerHitbox.player.transform.position.y + 5;
            x = PlayerHitbox.player.transform.position.x - 5;
            rote = 315;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = PlayerHitbox.player.transform.position.y + 5;
            x = PlayerHitbox.player.transform.position.x + 5;
            rote = 225;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = PlayerHitbox.player.transform.position.y - 5;
            x = PlayerHitbox.player.transform.position.x - 5;
            rote = 45;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = PlayerHitbox.player.transform.position.y - 5;
            x = PlayerHitbox.player.transform.position.x + 5;
            rote = 135;
            spawn = new Vector3(x, y);
            Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            yield return new WaitForSeconds(0.8f);

        }

        blastWaitTime = 0.2f;

        for (int i = 0; i < 3; i++)
        {
            y = 0;
            x = 8;
            rote = 0;
            bulletNumber = 8;
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = 3;
            x = 11;
            rote = 0;
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = -3;
            x = 11;
            rote = 0;
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);

            for (int j = 0; j < 8; j++)
            {
                y = UnityEngine.Random.Range(-5, 5);
                x = 12;
                magnification = 17;
                rote = 90;
                movedirection = 2;
                spawn = new Vector3(x, y);
                Instantiate(m_spawn2Prefab, spawn, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.6f);

            y = 0;
            x = -8;
            rote = 0;
            bulletNumber = 8;
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = 3;
            x = -11;
            rote = 0;
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = -3;
            x = -11;
            rote = 0;
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);

            for (int j = 0; j < 8; j++)
            {
                y = UnityEngine.Random.Range(-5, 5);
                x = -12;
                magnification = 17;
                rote = 90;
                movedirection = 3;
                spawn = new Vector3(x, y);
                Instantiate(m_spawn2Prefab, spawn, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.6f);
        }


        for (int i = 0; i < 5; i++)
        {
            y = 5;
            x = 7;
            rote = UnityEngine.Random.Range(0, 360);
            bulletNumber = 10;
            Vector3 spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = -5;
            x = 7;
            rote = UnityEngine.Random.Range(0, 360);
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = 5;
            x = -7;
            rote = UnityEngine.Random.Range(0, 360);
            bulletNumber = 10;
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            y = -5;
            x = -7;
            rote = UnityEngine.Random.Range(0, 360);
            spawn = new Vector3(x, y);
            Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
            yield return new WaitForEndOfFrame();

            for (int j = 0; j < 6; j++)
            {
                float x = 11f;
                float y = PlayerHitbox.player.transform.position.y;
                magnification = 25;
                rote = 180;
                spawn = new Vector3(x, y);
                Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                yield return new WaitForEndOfFrame();
                for (int k = 0; k < 6; k++)
                {
                    y = 2.7f;
                    x = 11;
                    magnification = 25;
                    rote = 180;
                    spawn = new Vector3(x, y);
                    Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                    yield return new WaitForEndOfFrame();
                    y = -2.7f;
                    spawn = new Vector3(x, y);
                    Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                    yield return new WaitForSeconds(0.1f);
                }

            }
        }


        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                y = PlayerHitbox.player.transform.position.y;
                x = 11;
                magnification = 25;
                rote = 180;
                Vector3 spawn = new Vector3(x, y);
                Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                yield return new WaitForEndOfFrame();
                y = 7;
                x = PlayerHitbox.player.transform.position.x;
                rote = 270;
                spawn = new Vector3(x, y);
                Instantiate(m_spawnPrefab, spawn, Quaternion.identity);
                yield return new WaitForSeconds(0.05f);

            }
            for (x = -11; x <= 11; x += 4)
            {
                bulletNumber = 10;
                y = 6;
                rote = UnityEngine.Random.Range(0, 360);
                Vector3 spawn = new Vector3(x, y);
                Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
                yield return new WaitForEndOfFrame();
                y = -6;
                rote = UnityEngine.Random.Range(0, 360);
                spawn = new Vector3(x, y);
                Instantiate(m_spawn3Prefab, spawn, Quaternion.identity);
                yield return new WaitForSeconds(0.4f);
            }

            yield return new WaitForSeconds(0.8f);
        }



    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlasterTest()
    {
        StartCoroutine("Test");
    }

    
}
