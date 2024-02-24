using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityModel
{
    public EntityModel(List<SkillInfo> skillInfos)
    {
        Skills = skillInfos;
    }
    public string CharacterName { get; set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int MaxExperience { get; private set; }

    public Vector3 SkillCastingTargetPosition { get; set; }
    public Vector3 MoveDestination { get; set; }

    public List<SkillInfo> Skills = new List<SkillInfo>();
    public SkillInfo selectedSkill { get; set; }


    public Action OnStateChanged;
    
    public enum ActionState
    {
        DEFAULT,
        SKILL_PREVIEW,
        SKILL_CASTING
    }
    
    private ActionState _state;
    public ActionState State 
    { 
        get 
        {   
            return _state;
        }
        set 
        { 
            _state = value;
            Debug.Log(_state);
            OnStateChanged?.Invoke();
        } 
    }
    public void TakeDamage(int damage)
    {
        if(damage < 0) throw new ArgumentOutOfRangeException("damage can not be less than 0");
        {
            Health = Mathf.Max(0, Health - damage);
        }
    }

    public void Heal(int value)
    {
        if(value < 0) throw new ArgumentOutOfRangeException("heal can not be less than 0");
        Health = Mathf.Min(MaxHealth, Health + value);
    }

    public void GainExperience(int value)
    {   
        if(value < 0) throw new ArgumentOutOfRangeException("value can not be negative");
        Experience += value;

        if (Experience >= MaxExperience)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if(Level < 30)
            Level++;
    }

    public void SkillStartReload(SkillInfo skill)
    {
        skill.SetColdownToZero();
    }
    
    public void Move(Vector3 destination)
    {
        MoveDestination = destination;
        UnSelectSkill();
        State = ActionState.DEFAULT;
    }
    public void SelectSkillByID(int id)
    {
        if(id >= 0 && id <= Skills.Count)
        {
            selectedSkill = Skills[id];
        }
    }
    public void UnSelectSkill()
    {
        selectedSkill = null;
    }
    public void TimeTick(float time)
    {
        if (time < 0) throw new ArgumentOutOfRangeException("Time cannot be negative");

        foreach (SkillInfo info in Skills)
        {
            info.processTime(time);
        }
    }
}
