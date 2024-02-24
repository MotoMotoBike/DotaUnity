using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public SkillInfo(string name, int manaCost, float coldown, GameObject preView, GameObject skillGO)
    {
        _name = name;
        _manaCost = manaCost;
        _coldown = coldown;
        _preView = preView;
        this.skillGO = skillGO;
    }

    protected string _name;
    protected int _manaCost;
    protected float _currentColdown;
    protected float _coldown;
    protected GameObject _preView;
    public GameObject skillGO { get; private set; }
    SkillStage stage;

    public void SetColdownToZero()
    {
        _currentColdown = 0;
    }
    public void processTime(float time)
    {
        _currentColdown += time;
    }
    public void HidePreview()
    {
        _preView.SetActive(false);
    }
    public void ShowPreview()
    {
        _preView.SetActive(true);
    }
}
public enum SkillStage{
    OFF,
    PlayingAnimation,
    Casting
}