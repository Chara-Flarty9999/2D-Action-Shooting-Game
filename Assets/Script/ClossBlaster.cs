using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEditorInternal;

public class ClossBlaster : MonoBehaviour
{
    SpriteRenderer spriterenderer;
    AudioSource audioSource;

    MoveInfo _moveInfo;
    StartInfo _startInfo;
    public float _blasterSize;
    int _beamWait;
    int _beamLoop;
    BlasterColor _blasterColor;

    [SerializeField] GameObject _BeamPrefab;
    [SerializeField] Sprite whileBeam;
    [SerializeField] Sprite endBeam;
    [SerializeField] AudioClip blasterSpawn = default;
    [SerializeField] AudioClip blasterbeam = default;

    [SerializeField] float testX;
    [SerializeField] float testY;
    [SerializeField] int testDi;

    [SerializeField] float testX2;
    [SerializeField] float testY2;
    [SerializeField] int testDi2;



    void Start()
    {
        GameObject spawner = GameObject.Find("SpawnArea");
        KnifeSpawn spawndata = spawner.GetComponent<KnifeSpawn>();
        spriterenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriterenderer.color -= new Color(0f, 0f, 0f, 0f);
        spriterenderer.material.color = new Color(1f, 1f, 1f, 0f);
        new MoveInfo(transform.position, 5);

        _startInfo = spawndata.f_place;
        _moveInfo = spawndata.m_place;
        _blasterSize = spawndata.blasterSize;
        _beamWait = spawndata.beamWait;
        _beamLoop = spawndata.beamTime;
        _blasterColor = spawndata.blasterColor;


        ClossBlasterCharge(_startInfo, _moveInfo, _blasterSize, _beamWait, _beamLoop, _blasterColor);
    }

    // Update is called once per frame
    void Update()
    {
        


        
    }

    /// <summary>
    /// �\���u���X�^�[���쓮�����郁�\�b�h�B�����ʒu��ړ����x�Ȃǂ͎��O�ɕϐ��Ɋi�[���Ă����悤�ɁB
    /// </summary>
    /// <param name="startInfo">��������X���W�AY���W�A�p�x��ݒ肷��B�p�x�͓x���@��OK�B���W��Vector�ł�OK�B</param>
    /// <param name="moveInfo">�r�[�����ˌ�Ƀu���X�^�[�{�̂�X�AY�ɑ΂��āA�܂��p�x����b�ɂǂꂾ������������ݒ肷��B</param>
    /// <param name="size">�u���X�^�[�̃T�C�Y��ݒ肷��B��{��1��OK�B</param>
    /// <param name="waittime">�u���X�^�[�������甭�˂܂ł̒x�����Ԃ�ݒ肷��B</param>
    /// <param name="looplong">�r�[�����˂̏���������J��Ԃ����A�܂艽�b�ԃr�[���𔭎˂��邩�ݒ肷��B</param>
    /// <param name="beamcolor">�������I�����W��ݒ肷��B</param>
    public void ClossBlasterCharge(StartInfo startInfo, MoveInfo moveInfo, float size, int waittime, int looplong, BlasterColor beamcolor)
    {
        this.transform.localScale = new Vector3 (size,size);
        this.transform.position = startInfo.vector3;
        this.transform.eulerAngles = new Vector3(0,0, startInfo.di);
        audioSource.PlayOneShot(blasterSpawn);
        this.spriterenderer.material.DOFade(1f, 1.5f);

        Debug.Log("����������");

        StartCoroutine(ClossBlasterFire(moveInfo, waittime, looplong, beamcolor));
    }

    IEnumerator ClossBlasterFire(MoveInfo moveinfo, int wait, int looplong,BlasterColor beamcolor)
    {
        yield return new WaitForSeconds(wait);
        spriterenderer.sprite = whileBeam;
        audioSource.PlayOneShot(blasterbeam);
        Debug.Log("�X�v���C�g�܂ł͕ς��܂���");
        //Instantiate��ǉ�����
        var parent = this.transform;
        Instantiate(_BeamPrefab, Vector3.zero, Quaternion.identity, parent);
        for (int i = 0; i < (looplong + 2) * 50; i++) 
        {
            Debug.Log("������");
            transform.position += moveinfo.vector3;
            transform.eulerAngles += new Vector3(0, 0, moveinfo.di);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("���[�v������");
        spriterenderer.sprite = endBeam;
        GameObject gameObject = GameObject.Find("ClossBeam(Clone)");
        ClossBlasterBeam clossBlasterBeam = gameObject.GetComponent<ClossBlasterBeam>();
        clossBlasterBeam.ClossBlasterBeamExit();
        this.spriterenderer.material.DOFade(0f, 0.5f);
    }


    /// <summary>
    /// �u���X�^�[�̈ړ�������͂���
    /// </summary>
    public struct MoveInfo
    {
        public float x;
        public float y;
        public Vector3 vector3;
        public int di;

        public MoveInfo(Vector3 vector3, int di)
        {
            this.vector3 = vector3;
            this.di = di;
            this.x = vector3.x;
            this.y = vector3.y;
        }
        public MoveInfo(float x, float y, int di)
        {
            this.x = x;
            this.y = y;
            this.di = di;
            this.vector3 = new Vector3(x, y);
        }
    }
    /// <summary>
    /// �u���X�^�[�̏����ʒu����͂���
    /// </summary>
    public struct StartInfo
    {
        public float x;
        public float y;
        public Vector3 vector3;
        public int di;

        public StartInfo(Vector3 vector3, int di)
        {
            this.vector3 = vector3;
            this.di = di;
            this.x = vector3.x;
            this.y = vector3.y;
        }

        public StartInfo(float x, float y, int di)
        {
            this.x = x;
            this.y = y;
            this.di = di;
            this.vector3 = new Vector3(x, y);
        }
    }
    public enum BlasterColor
    {
        white,
        orange,
        blue

    }
}
