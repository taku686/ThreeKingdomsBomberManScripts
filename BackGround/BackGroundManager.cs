using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField]
    List<Material> _backGroundList = new List<Material>();
    Material _currentMaterial;
    Material _prevMaterial;
    float _rotationSpeed = 40;
    float _timer;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _currentMaterial = _backGroundList[Random.Range(0, _backGroundList.Count - 1)];
        RenderSettings.skybox = _currentMaterial;
    }

    private void Update()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer >= _rotationSpeed)
        {
            ChangeBackGround();
        }
        if (_timer > _rotationSpeed)
        {
            _timer = 0;
        }
        float rotation = 360 * (_timer / _rotationSpeed);
        _currentMaterial.SetFloat("_Rotation", rotation);

    }

    private void ChangeBackGround()
    {
        _prevMaterial = _currentMaterial;
        _backGroundList.Remove(_prevMaterial);
        _currentMaterial = _backGroundList[Random.Range(0, _backGroundList.Count - 1)];
        _backGroundList.Add(_prevMaterial);
        RenderSettings.skybox = _currentMaterial;
    }
}
