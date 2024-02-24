using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using static EntityModel;

public class EntityView : MonoBehaviour 
{
    private Camera _camera;
    [SerializeField] protected NavMeshAgent _agent;

    [SerializeField] protected List<GameObject> skillsPreview;
    [SerializeField] protected List<GameObject> skillsGO;

    [SerializeField]  protected Animator _animator;

    protected EntiyPresentor  _presentor;
    protected EntityModel _model;
    
    public virtual void Start(){
        _animator = GetComponent<Animator>();
        _model = new EntityModel(new List<SkillInfo>());
        _presentor = new EntiyPresentor(_model);
        _camera = Camera.main;
    }
    public virtual void ChangeAnimationState(){
        Debug.Log("Animation state changed");

        for (int i = 0; i < 10; i++)
        {
            
        }
        
        switch (_model.State)
        {
            case EntityModel.ActionState.DEFAULT:
                _agent.isStopped = false;
                _agent.SetDestination(_model.MoveDestination);
                break;
            case EntityModel.ActionState.SKILL_PREVIEW:
                break;
            case EntityModel.ActionState.SKILL_CASTING:
                LoockAt(GetCursorWorldPosition());
                _animator.SetTrigger("Skill" + _model.Skills.IndexOf(_model.selectedSkill));
                _agent.isStopped = true;
                break;
        }

        foreach (SkillInfo skills in _model.Skills)
        {
            skills.HidePreview();
        }
        if (_model.selectedSkill != null && _model.State != ActionState.SKILL_CASTING) _model.selectedSkill.ShowPreview();
    }

    public void CastingFinished()
    {
        Instantiate(_model.selectedSkill.skillGO, transform.position, transform.rotation);
        _presentor.CastingFinished();
    }

    public virtual void LoockAt(Vector3 position){
        transform.LookAt(position);
    }
    public virtual void Update(){
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                _presentor.Move(hit.point);
            }
        }
        _animator.SetBool("isRunning", !(_agent.velocity.magnitude < 2));
        _presentor.TimeTick(Time.deltaTime);
    }

    Vector3 GetCursorWorldPosition(){
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            return hit.point;
        }
        throw new InvalidOperationException("Raycast failed");
    }
}
