using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BombView : MonoBehaviour
{
    private const string _explosion = "Explosion";
    private const string _bombManagerTag = "BombManager";
    private const string _ExplosionFire = "Effect_17_Explosion";
    private const string _ExplosionCircle = "ExplodudesBombExplosion";
    private const float _gridUnitSize = 1f;
    private int _bombDamage;
    private int _playerID;
    private float _bombDelayBeforeExplosion = 3;
    private float _bombExplosionActiveDuration = 0.5f;
    private float _bombAdditionalDelayBeforeDestruction = 1.5f;
    private float _bombDistanceInGridUnits;
    private bool _exploded;

    private Animator _animator;
    private UserData _userData;
    private BombManager _bombManager;
    private BoxCollider _boxCollider;
    private BombView _otherBomb;
    private PhotonView _photonView;

    private WaitForSeconds _bombDuration;
    private WaitForSeconds _explosionDuration;
    private WaitForSeconds _additionalDelayBeforeDestruction;

    public ParticleSystem DirectedExplosionN;
    public ParticleSystem DirectedExplosionS;
    public ParticleSystem DirectedExplosionE;
    public ParticleSystem DirectedExplosionW;
    public ParticleSystem ExplodudesBombExplosion;

    public Vector3 RaycastOffset = new Vector3(0, 0.5f, 0);
    public float MaximumRaycastDistance = 50f;
    public LayerMask ObstaclesMask;
    public LayerMask DamageLayerMask;
    public Transform _bombModel;
    public GameObject _owner;

    private GameObject _explosionFire;
    private GameObject _explosionCircle;

    private RaycastHit _raycastNorth;
    private RaycastHit _raycastSouth;
    private RaycastHit _raycastEast;
    private RaycastHit _raycastWest;

    private Vector3 _damageAreaPosition;
    private Vector3 _damageAreaSize;

    private float _obstacleNorthDistance = 0f;
    private float _obstacleEastDistance = 0f;
    private float _obstacleWestDistance = 0f;
    private float _obstacleSouthDistance = 0f;
    private float _skinWidth = 0.01f;

    private DamageOnTouch _damageAreaEast;
    private DamageOnTouch _damageAreaWest;
    private DamageOnTouch _damageAreaNorth;
    private DamageOnTouch _damageAreaSouth;
    private DamageOnTouch _damageAreaCenter;
    private Coroutine _delayBeforeExplosionCoroutine;

    public int PlayerID { get => _playerID; private set => _playerID = value; }


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _bombManager = GameObject.FindGameObjectWithTag(_bombManagerTag).GetComponent<BombManager>();

        _bombDuration = new WaitForSeconds(_bombDelayBeforeExplosion);
        _explosionDuration = new WaitForSeconds(_bombExplosionActiveDuration);
        _additionalDelayBeforeDestruction = new WaitForSeconds(_bombAdditionalDelayBeforeDestruction);

        _boxCollider = this.gameObject.GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;

        _userData = SaveSystem.Instance.UserData;
        _bombDamage = _userData._currentCharacter._attack;
        _bombDistanceInGridUnits = _userData._currentCharacter._firePower;

        //   CreateExplosionEffect();
        _damageAreaEast = CreateDamageArea("East");
        _damageAreaWest = CreateDamageArea("West");
        _damageAreaSouth = CreateDamageArea("South");
        _damageAreaNorth = CreateDamageArea("North");
        _damageAreaCenter = CreateDamageArea("Center");

        _damageAreaSize.x = _gridUnitSize / 2f;
        _damageAreaSize.y = _gridUnitSize / 2f;
        _damageAreaSize.z = _gridUnitSize / 2f;

        _damageAreaPosition = this.transform.position + Vector3.up * _gridUnitSize / 2f;
        _damageAreaCenter.gameObject.transform.position = _damageAreaPosition;
        _damageAreaCenter.gameObject.GetComponent<BoxCollider>().size = _damageAreaSize;
    }

    private void OnEnable()
    {
        /*
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            //   _photonView.RPC(nameof(ResetBomb), RpcTarget.All);
        }
        else
        {
            ResetBomb();
        }
        */
    }
    public void Initialize(int playerID, GameObject owner)
    {
        PlayerID = playerID;
        _owner = owner;
        _photonView = GetComponent<PhotonView>();
    }


    [PunRPC]
    public void ResetBomb()
    {
        if (!_photonView.IsMine)
        {
            return;
        }
        _bombManager.BombAdd();
        _boxCollider.enabled = true;
        _boxCollider.isTrigger = true;
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            _photonView.RPC(nameof(ChangeBombModelActiveState), RpcTarget.All, true);
        }
        else
        {
            _bombModel.gameObject.SetActive(true);
        }
        _exploded = false;
        _delayBeforeExplosionCoroutine = StartCoroutine(DelayBeforeExplosionCoroutine());
    }


    private void CastRays()
    {
        float boxWidth = (_boxCollider.bounds.size.x / 2f) + _skinWidth;
        //boxWidth = 0f;

        _raycastEast = MMDebug.Raycast3D(this.transform.position + Vector3.right * boxWidth + RaycastOffset, Vector3.right, MaximumRaycastDistance, ObstaclesMask, Color.red, true);
        if (_raycastEast.collider != null) { _obstacleEastDistance = _raycastEast.distance; } else { _obstacleEastDistance = 0f; }

        _raycastNorth = MMDebug.Raycast3D(this.transform.position + Vector3.forward * boxWidth + RaycastOffset, Vector3.forward, MaximumRaycastDistance, ObstaclesMask, Color.red, true);
        if (_raycastNorth.collider != null) { _obstacleNorthDistance = _raycastNorth.distance; } else { _obstacleNorthDistance = 0f; }

        _raycastSouth = MMDebug.Raycast3D(this.transform.position + Vector3.back * boxWidth + RaycastOffset, Vector3.back, MaximumRaycastDistance, ObstaclesMask, Color.red, true);
        if (_raycastSouth.collider != null) { _obstacleSouthDistance = _raycastSouth.distance; } else { _obstacleSouthDistance = 0f; }

        _raycastWest = MMDebug.Raycast3D(this.transform.position + Vector3.left * boxWidth + RaycastOffset, Vector3.left, MaximumRaycastDistance, ObstaclesMask, Color.red, true);
        if (_raycastWest.collider != null) { _obstacleWestDistance = _raycastWest.distance; } else { _obstacleWestDistance = 0f; }

    }

    private DamageOnTouch CreateDamageArea(string name)
    {
        GameObject damageAreaGameObject = new GameObject();
        damageAreaGameObject.SetActive(false);
        damageAreaGameObject.transform.SetParent(this.transform);
        damageAreaGameObject.name = "ExplodudesBombDamageArea" + name;
        damageAreaGameObject.layer = LayerMask.NameToLayer("Enemies");

        DamageOnTouch damageOnTouch = damageAreaGameObject.AddComponent<DamageOnTouch>();
        damageOnTouch.DamageCaused = _bombDamage;
        damageOnTouch.TargetLayerMask = DamageLayerMask;
        damageOnTouch.DamageTakenEveryTime = 0;
        damageOnTouch.InvincibilityDuration = 0f;
        damageOnTouch.DamageTakenEveryTime = 10;

        BoxCollider colllider = damageAreaGameObject.AddComponent<BoxCollider>();
        colllider.isTrigger = true;

        return damageOnTouch;
    }
    /*
    private void CreateExplosionEffect()
    {
        DirectedExplosionE = Instantiate(_explosionFire, Vector3.zero, Quaternion.Euler(Vector3.zero), this.transform).GetComponent<ParticleSystem>();
        DirectedExplosionW = Instantiate(_explosionFire, Vector3.zero, Quaternion.Euler(Vector3.zero), this.transform).GetComponent<ParticleSystem>();
        DirectedExplosionN = Instantiate(_explosionFire, Vector3.zero, Quaternion.Euler(Vector3.zero), this.transform).GetComponent<ParticleSystem>();
        DirectedExplosionS = Instantiate(_explosionFire, Vector3.zero, Quaternion.Euler(Vector3.zero), this.transform).GetComponent<ParticleSystem>();
        ExplodudesBombExplosion = Instantiate(_explosionCircle, Vector3.zero, Quaternion.Euler(Vector3.zero), this.transform).GetComponent<ParticleSystem>();
        DirectedExplosionE.gameObject.SetActive(false);
        DirectedExplosionW.gameObject.SetActive(false);
        DirectedExplosionN.gameObject.SetActive(false);
        DirectedExplosionS.gameObject.SetActive(false);
        ExplodudesBombExplosion.gameObject.SetActive(false);
    }
    */
    private IEnumerator DelayBeforeExplosionCoroutine()
    {
        yield return _bombDuration;
        /*
        if (_photonView.IsMine && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            _photonView.RPC(nameof(Detonate), RpcTarget.All);
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            Detonate();
        }
        */
        Detonate();
    }

    //   [PunRPC]
    public void Detonate()
    {
        if (_exploded || !_photonView.IsMine)
        {
            return;
        }
        StartCoroutine(DetonateCoroutine());
    }

    private IEnumerator DetonateCoroutine()
    {
        _exploded = true;
        _boxCollider.enabled = false;

        StopCoroutine(_delayBeforeExplosionCoroutine);

        CastRays();
        DirectedExplosion(_raycastEast, _damageAreaEast, DirectedExplosionE, 90f);
        DirectedExplosion(_raycastWest, _damageAreaWest, DirectedExplosionW, 90f);
        DirectedExplosion(_raycastNorth, _damageAreaNorth, DirectedExplosionN, 0f);
        DirectedExplosion(_raycastSouth, _damageAreaSouth, DirectedExplosionS, 0f);

        _damageAreaCenter.gameObject.SetActive(true);

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            _photonView.RPC(nameof(ChangeBombModelActiveState), RpcTarget.All, false);
        }
        else
        {
            _bombModel.gameObject.SetActive(false);
        }
        ExplodudesBombExplosion.gameObject.SetActive(true);
        _bombManager._deletePos = transform.position;
        _bombManager.BombDelete();

        yield return _explosionDuration;
        _damageAreaEast.gameObject.SetActive(false);
        _damageAreaWest.gameObject.SetActive(false);
        _damageAreaNorth.gameObject.SetActive(false);
        _damageAreaSouth.gameObject.SetActive(false);
        _damageAreaCenter.gameObject.SetActive(false);

        yield return _additionalDelayBeforeDestruction;
        ExplodudesBombExplosion.gameObject.SetActive(false);
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            _photonView.RPC(nameof(ChangeBombGameObjectActiveState), RpcTarget.All, false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void DirectedExplosion(RaycastHit hit, DamageOnTouch damageArea, ParticleSystem explosion, float angle)
    {
        float hitDistance = hit.distance;

        // if what we find has a Health component, it's gonna be destroyed and needs to be covered by the explosion
        if (hit.collider.gameObject.GetComponent<Health>() != null)
        {
            hitDistance += _gridUnitSize;
        }

        // if what we find has a Bomb component, it's gonna be destroyed and needs to be covered by the explosion
        _otherBomb = hit.collider.gameObject.GetComponent<BombView>();
        //   Debug.Log(_otherBomb);
        if ((_otherBomb != null) && (hitDistance <= _bombDistanceInGridUnits))
        {
            // Debug.Log("誘爆");
            hitDistance += _gridUnitSize;
            // we detonate the other bomb
            _otherBomb.Detonate();
        }

        // if we're colliding with an obstacle, we stop this explosion
        if (hitDistance <= _gridUnitSize / 2f)
        {
            return;
        }

        // otherwise we explode

        // we compute the size of the explosion
        float explosionLength;
        float adjustedDistance = hitDistance - _gridUnitSize / 2f;
        float maxExplosionLength = _bombDistanceInGridUnits * _gridUnitSize;
        explosionLength = Mathf.Min(adjustedDistance, maxExplosionLength);
        explosionLength -= _gridUnitSize / 2f;

        // we set our damage size and position
        _damageAreaSize.x = _gridUnitSize / 2f;
        _damageAreaSize.y = _gridUnitSize / 2f;
        _damageAreaSize.z = explosionLength;

        _damageAreaPosition = this.transform.position
                            + (hit.point - (this.transform.position + RaycastOffset)).normalized * (explosionLength / 2f + _gridUnitSize / 2f)
                            + Vector3.up * _gridUnitSize / 2f;

        damageArea.gameObject.transform.position = _damageAreaPosition;
        damageArea.gameObject.transform.LookAt(this.transform.position + Vector3.up * (_gridUnitSize / 2f));

        // we activate our damage area
        damageArea.gameObject.SetActive(true);
        damageArea.gameObject.GetComponent<BoxCollider>().size = _damageAreaSize;

        // we activate our VFX explosion
        explosion.gameObject.SetActive(true);
        explosion.transform.position = _damageAreaPosition;
        ParticleSystem.ShapeModule shape = explosion.shape;
        shape.scale = new Vector3(0.1f, 0.1f, explosionLength);
        shape.rotation = new Vector3(0f, angle, 0f);
    }
    [PunRPC]
    private void ChangeBombModelActiveState(bool activeState)
    {
        _bombModel.gameObject.SetActive(activeState);
    }

    [PunRPC]
    private void ChangeBombGameObjectActiveState(bool activeState)
    {
        this.gameObject.SetActive(activeState);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _owner)
        {
            _boxCollider.isTrigger = false;
        }
    }
}
