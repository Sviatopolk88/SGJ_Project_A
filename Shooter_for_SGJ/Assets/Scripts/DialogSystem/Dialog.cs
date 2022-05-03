using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AssemblyCSharp.Assets.Scripts.Player;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private string[] _sentences;
    [SerializeField] private int _index;
    [SerializeField] private float _typeSpeed;
    [SerializeField] private GameObject _dialogPanel;
    //[SerializeField] private GameObject _pizza;
    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GameObject.Find("DialogField").GetComponent<TextMeshProUGUI>();
        _dialogPanel = GameObject.FindWithTag("BossDialog");
        _dialogPanel.SetActive(false);
       // _pizza = GameObject.Find("Pizza");
        //_pizza.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialog()
    {
        _dialogPanel.SetActive(true);
        _textMesh.text = string.Empty;
        FindObjectOfType<WeaponSwitcher>().SelectPizza();
        FindObjectOfType<SoundManager>().StopPlayer();
        FindObjectOfType<SoundManager>().Devil();
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char x in _sentences[_index].ToCharArray())
        {
            _textMesh.text += x;
            yield return new WaitForSeconds(_typeSpeed);
           
        }
       
        //_pizza.transform.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(2f);
        if (_index == _sentences.Length - 1)
        {
            FindObjectOfType<PlayerUI>().EndDialog();
            var pizza = GameObject.Find("pizzabox").gameObject;
            pizza.SetActive(false);
        }
    }

    public void NextSentence()
    {
        if (_index < _sentences.Length - 1)
        {
            _index++;
            _textMesh.text = "";
            StartCoroutine(Type());
        }
        
        
    }
}
