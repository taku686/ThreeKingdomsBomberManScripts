using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandleManager : MonoBehaviour
{
    [SerializeField]
    Button _bombButton;
    [SerializeField]
    Button _skill1Button;
    [SerializeField]
    Button _skill2Button;

    public Button BombButton { get => _bombButton; }
    public Button Skill1Button { get => _skill1Button; }
    public Button Skill2Button { get => _skill2Button; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
