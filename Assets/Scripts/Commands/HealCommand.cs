using System.Collections;
using System.Collections.Generic;
using TBRPG.Attributes;
using TBRPG.Commands;
using UnityEngine;

public class HealCommand : ICommand
{
    private float healAmount;
    private Transform Target;
    private Health healthComponent;

    public HealCommand(float healAmount, Transform target)
    {
        this.healAmount = healAmount;
        Target = target;
        healthComponent=target.GetComponent<Health>();
    }

    public void Execute()
    {
        //You need to undersatnd the Ability System to move forwared from this point
        //healthComponent.
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
