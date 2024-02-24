using UnityEngine.AI;
using UnityEngine;
using static EntityModel;
using Unity.VisualScripting;

public class EntiyPresentor
{
    protected EntityModel _model;

    public EntiyPresentor(EntityModel model)
    {
        _model = model;
    }

    public void Move(Vector3 destination)
    {
        Debug.Log("Move complete at " + destination);
        _model.selectedSkill = null;
        _model.State = ActionState.DEFAULT;
        _model.Move(destination);
    }
    
    public virtual void TimeTick(float deltaTime){
        _model.TimeTick(deltaTime);
    }
    public virtual void ProcessSkillInput(int skillId)
    {
        if (_model.State == ActionState.SKILL_CASTING) return;

        if (_model.State == ActionState.DEFAULT)
        {
            _model.SelectSkillByID(skillId);
            _model.State = ActionState.SKILL_PREVIEW;
        }
    }
    public virtual void TargetInput(Vector3 from,Vector3 to){
        if (_model.State == ActionState.DEFAULT) return;

        _model.MoveDestination = from;
        _model.SkillCastingTargetPosition = to;
        if (_model.State == ActionState.SKILL_PREVIEW)
        {
            _model.State = ActionState.SKILL_CASTING;
        }

    }
    public virtual void CastingFinished()
    {
        _model.UnSelectSkill();
        _model.State = ActionState.DEFAULT;
    }


}
