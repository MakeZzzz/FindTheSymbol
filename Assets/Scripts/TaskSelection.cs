using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TaskSelection : MonoBehaviour
{
    [SerializeField] private GameObject _taskField;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _dalleImage;
    
    [SerializeField] private GameObject _showImage;
    
    public bool _isCountDown { get; private set; }
    public string tempName { get ; private set; }
    public Sprite tempSprite { get; private set; }
    
    private bool isDalleImage = false;
    private IReadOnlyList<Sprite> _sprites = new List<Sprite>();
    private  List<String> _spritesNames = new List<string>();
    private List<Sprite> _tempDalleSprites = new List<Sprite>();
    private TextMeshPro _textName;
    private int _waitingTime = 3;
    
   
    [UsedImplicitly]
    public void SearchObject()
    {
        StartCoroutine(CountDown());
    }
    
    [UsedImplicitly]
    public void ListClear()
    {
        _spritesNames?.Clear();
        _tempDalleSprites?.Clear();
    }

    private int GetRandomName()
    {
      return  Random.Range(0,_sprites.Count);
    }
    public void Initialize(IReadOnlyList<Sprite> sprites)
    {
        _sprites = sprites;
    }

    [UsedImplicitly]
    public void ShowDalleImage()
    {
        isDalleImage = true;
    }
    public IEnumerator CountDown()
    {
        _showImage.SetActive(false);
        if (_dalleImage.sprite != null)
        {
            _dalleImage.sprite = null;
        }
        _isCountDown = true;
       yield return Timer();
        
        var randomIndex = GetRandomName();
        tempSprite = _sprites[randomIndex];
        tempName = _sprites[randomIndex].name;

        while (_tempDalleSprites.Contains(tempSprite))
        {
            randomIndex = GetRandomName();
            tempName = _sprites[randomIndex].name;
           tempSprite = _sprites[randomIndex];
        }
        _tempDalleSprites.Add(tempSprite);
        _spritesNames.Add(tempName);
       
        DisplayFieldSettings();
        _isCountDown = false;
    }

    private void DisplayFieldSettings()
    {
        Color tempColor = Random.ColorHSV();
        _text.color = tempColor;
        _text.text = "Find "+ tempName;
        _dalleImage.sprite = tempSprite;
        if (isDalleImage)
        { 
            _showImage.SetActive(true);
            _dalleImage.sprite = tempSprite;
        }
    }

    private IEnumerator Timer()
    {
        _taskField.SetActive(true);
        var tempTime = _waitingTime;
        while (tempTime > 0)
        {

            _text.text = tempTime.ToString();
        
            yield return new WaitForSeconds(1f); 
        
            tempTime--; 
        }
        _text.text = "GO!";
        yield return new WaitForSeconds(3f);
    }
    
}
