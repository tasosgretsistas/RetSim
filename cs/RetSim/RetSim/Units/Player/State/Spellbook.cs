﻿using RetSim.Simulation.Events;
using RetSim.Spells;

namespace RetSim.Units.Player.State;

public class Spellbook : Dictionary<int, SpellState>
{
    public Spellbook()
    {
        foreach (var spell in Data.Collections.Spells.Values)
            Add(spell);
    }

    public void Add(Spell spell)
    {
        if (spell != null)
            Add(spell.ID, new SpellState(spell));
    }

    public static void StartCooldown(SpellState state, CooldownEndEvent cooldown)
    {
        state.CooldownEnd = cooldown;
    }

    public static void EndCooldown(SpellState state)
    {
        state.CooldownEnd = null;
    }

    public bool IsOnCooldown(Spell spell)
    {
        return this[spell.ID].CooldownEnd != null;
    }
}