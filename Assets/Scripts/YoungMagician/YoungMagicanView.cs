using static EntityModel;
using UnityEngine;
using System.Collections.Generic;

public class YoungMagicanView : EntityView
{
    public override void Start(){
        base.Start();

        List<SkillInfo> skills = new List<SkillInfo>();
        skills.Add(new SkillInfo("FireBlast", 10, 20, skillsPreview[0], skillsGO[0]));
        skills.Add(new SkillInfo("FireBall", 10, 20, skillsPreview[1], skillsGO[1]));
        _model = new YoungMagicanModel(skills);

        _presentor = new YoungMagicanPresentor(_model);
        _model.OnStateChanged += ChangeAnimationState;
    }

    public override void Update(){
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            _presentor.TargetInput(transform.position, Input.mousePosition);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _presentor.ProcessSkillInput(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            _presentor.ProcessSkillInput(1);
        }

        
    }
    public override void ChangeAnimationState()
    {
        base.ChangeAnimationState();
    }
}
