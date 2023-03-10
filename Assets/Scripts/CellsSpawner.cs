using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsSpawner : MonoBehaviour
{
    [SerializeField] private TaskSelection _taskSelection;
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private GetRandom _getRandom;
    [SerializeField] private SymbolsSetView _symbolsSetView;
    [SerializeField] private ScriptableObject _scriptableObject;
    
    [SerializeField] private Canvas _currentLevel;
    
    [SerializeField] private GameObject _cell;
    public void CreateLevel(IReadOnlyList<Sprite> sprites)
    {
        if (_scriptableObject.Cells.Count > 0)
        {
            _scriptableObject.ListClear(_scriptableObject._cells);
        }
        int x = _levelSettings.startPositionX;
        int y = _levelSettings.startPositionY;
        var cellCount = _levelSettings.columnsCount * _levelSettings.elementsСountPerLine;
    
        var tempSprite= _getRandom.GetRandomObject(sprites, cellCount);
        _taskSelection.Initialize(tempSprite);
        
        int spriteNumber = 0;
        for (int k = 0; k < _levelSettings.columnsCount; k++)
        {
            for (int i = 0; i < _levelSettings.elementsСountPerLine; i++)
            {
                var cell = Instantiate(_cell,_currentLevel.transform);
                _scriptableObject._cells.Add(cell);
                cell.transform.position = new Vector3(x, y, 0);
    
                var icon = Instantiate(_symbolsSetView,cell.transform);
                icon.Initialize(tempSprite[spriteNumber]);
                
                icon.transform.position = new Vector3(x, y, 0);
                icon.name = tempSprite[spriteNumber].name;
                x += _levelSettings.distanceBetweenElements;
                
                spriteNumber++;
            }
            x = _levelSettings.startPositionX;
            y -= _levelSettings.lineSpacing;
        }
    }
    
}
