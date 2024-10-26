using UnityEngine;
using DG.Tweening;
using System.Collections;
using static ClossBlaster;

public class NormalBlaster : MonoBehaviour
{
    SpriteRenderer spriterenderer;
    AudioSource audioSource;
    Animator m_anim;
    Rigidbody2D rigidbody2d;

    ClossBlaster.BlasterColor _blasterColor;
    ClossBlaster.MoveInfo _moveInfo;
    ClossBlaster.StartInfo _startInfo;
    public float _blasterSize;
    int _beamWait;
    int _beamLoop;
    Vector3 recoilMove;

    bool firstAnim = false;
    bool endBeamAnim = false;
    

    [SerializeField] GameObject _BeamPrefab;
    [SerializeField] AudioClip blasterSpawn = default;
    [SerializeField] AudioClip blasterbeam = default;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawner = GameObject.Find("SpawnArea");
        KnifeSpawn spawndata = spawner.GetComponent<KnifeSpawn>();
        spriterenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        spriterenderer.material.color = new Color(1f, 1f, 1f, 0f);

        //この下の6つをwave等のスクリプトで設定してくだされ
        _startInfo = spawndata.f_place;
        _moveInfo = spawndata.m_place;
        _blasterSize = spawndata.blasterSize;
        _beamWait = spawndata.beamWait;
        _beamLoop = spawndata.beamTime;
        _blasterColor = spawndata.blasterColor;

        StartCoroutine(NormalBlasterCharge(_startInfo,_moveInfo,_blasterSize,_beamWait, _beamLoop,_blasterColor));
    }

    public static Vector3 AngleToVector2(float angle)
    {
        var radian = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NormalBlasterCharge(StartInfo startInfo, MoveInfo moveInfo, float size, int waittime, int looplong, BlasterColor color)
    {
        
        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOMove(moveInfo.vector3, 0.8f).SetEase(Ease.OutQuart));
        sequence.Join(this.transform.DORotate(new Vector3(0,0,moveInfo.di), 0.8f).SetEase(Ease.OutQuart));
        sequence.Join(this.spriterenderer.material.DOFade(1f, 0.5f));

        this.transform.localScale = new Vector3(size, size);
        this.transform.position = startInfo.vector3;
        this.transform.eulerAngles = new Vector3(0, 0, startInfo.di);

        audioSource.PlayOneShot(blasterSpawn);

        bool firstmove = false;

        sequence.Play().OnComplete(() =>
        {
            firstmove = true;

        });
        yield return new WaitUntil(() => firstmove);

        yield return new WaitForSeconds(waittime);
        firstAnim = true;
        m_anim.SetBool("firstAnim", firstAnim);
        recoilMove = AngleToVector2(moveInfo.di);
        yield return new WaitForSeconds(0.25f);
        audioSource.PlayOneShot(blasterbeam);

        StartCoroutine("BlasterRecoil");
        var parent = this.transform;
        Instantiate(_BeamPrefab, Vector3.zero, Quaternion.identity, parent);
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < (looplong + 2) * 10; i++)
        {
            yield return new WaitForSeconds(0.01f);
        }
        GameObject gameObject = transform.GetChild(0).gameObject;
        NormalBlasterBeam normalBlasterBeam = gameObject.GetComponent<NormalBlasterBeam>();
        normalBlasterBeam.NormalBlasterBeamExit();
        this.spriterenderer.material.DOFade(0f, 0.5f);
        Invoke("ToDestroy", 1f);
    }

    IEnumerator BlasterRecoil()
    {
        for (int i = 0; i < 50; i++)
        {
            rigidbody2d.AddForce(-1 * recoilMove * 30); //ForceMode2D.Impulse
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        rigidbody2d.velocity = Vector3.zero;
    }
    public void ToDestroy()
    {
        Destroy(this.gameObject);
    }
}
