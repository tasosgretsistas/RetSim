﻿using RetSim.Simulation;
using RetSim.Units;

namespace RetSim.Spells.AuraEffects;

class ModDamageDone : ModifyPercent
{
    public School SchoolMask { get; init; }

    public ModDamageDone(float percent, School schoolMask) : base(percent)
    {
        SchoolMask = schoolMask;
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.DamageDone[SchoolMask] *= GetDifference(Value, target.Auras[aura].Stacks);

        //Program.Logger.Log($"{fight.Timestamp} - {aura.Parent.Name} increased {SchoolMask} damage modifier to {fight.Player.Modifiers.DamageDone[SchoolMask]}");
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        fight.Player.Modifiers.DamageDone[SchoolMask] /= GetDifference(Value, target.Auras[aura].Stacks);

        //Program.Logger.Log($"{fight.Timestamp} - {aura.Parent.Name} decreased {SchoolMask} damage modifier to {fight.Player.Modifiers.DamageDone[SchoolMask]}");
    }
}