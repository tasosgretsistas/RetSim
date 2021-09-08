﻿using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.Tactics
{
    abstract public class Tactic
    {
        public abstract List<Event> PreFight(FightSimulation fight);

        public abstract Event GetActionBetween(int start, int end, FightSimulation fight);
    }
}