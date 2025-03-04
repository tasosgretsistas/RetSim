﻿using RetSim.Data;
using RetSim.Spells;
using System.Collections.Generic;
using System.ComponentModel;
using static RetSim.Data.Collections;

namespace RetSimDesktop.Model
{
    public class SelectedDebuffs : INotifyPropertyChanged
    {
        private bool judgementoftheCrusaderEnabled = true;
        private JudgementoftheCrusader selectedJudgementoftheCrusader = JudgementoftheCrusader.ImpJudgementoftheCrusader;

        private bool judgementofWisdomEnabled = true;

        private bool armorDebuffEnabled = true;
        private ArmorDebuff selectedArmorDebuff = ArmorDebuff.ImpExposeArmor;

        private bool bloodFrenzyEnabled = true;
        private bool huntersMarkEnabled = true;
        private bool exposeWeaknessEnabled = true;
        private int hunterAgility = 1000;

        private bool faerieFireEnabled = true;
        private FaerieFire selectedFaerieFire = FaerieFire.ImpFaerieFire;

        private bool curseofRecklessnessEnabled = true;

        private bool curseoftheElementsEnabled = true;
        private CurseoftheElements selectedCurseoftheElements = CurseoftheElements.Malediction;

        private bool improvedShadowBoltEnabled = true;
        private bool miseryEnabled = true;
        private bool shadowWeavingEnabled = true;
        private bool improvedScorchEnabled = false;

        public bool JudgementoftheCrusaderEnabled { get => judgementoftheCrusaderEnabled; set { judgementoftheCrusaderEnabled = value; OnPropertyChanged(nameof(JudgementoftheCrusaderEnabled)); } }
        public JudgementoftheCrusader SelectedJudgementoftheCrusader { get => selectedJudgementoftheCrusader; set { selectedJudgementoftheCrusader = value; OnPropertyChanged(nameof(SelectedJudgementoftheCrusader)); } }
        public bool JudgementofWisdomEnabled { get => judgementofWisdomEnabled; set { judgementofWisdomEnabled = value; OnPropertyChanged(nameof(JudgementofWisdomEnabled)); } }
        public bool ArmorDebuffEnabled { get => armorDebuffEnabled; set { armorDebuffEnabled = value; OnPropertyChanged(nameof(ArmorDebuffEnabled)); } }
        public ArmorDebuff SelectedArmorDebuff { get => selectedArmorDebuff; set { selectedArmorDebuff = value; OnPropertyChanged(nameof(SelectedArmorDebuff)); } }
        public bool BloodFrenzyEnabled { get => bloodFrenzyEnabled; set { bloodFrenzyEnabled = value; OnPropertyChanged(nameof(BloodFrenzyEnabled)); } }
        public bool HuntersMarkEnabled { get => huntersMarkEnabled; set { huntersMarkEnabled = value; OnPropertyChanged(nameof(HuntersMarkEnabled)); } }
        public bool ExposeWeaknessEnabled { get => exposeWeaknessEnabled; set { exposeWeaknessEnabled = value; OnPropertyChanged(nameof(ExposeWeaknessEnabled)); } }
        public int HunterAgility
        {
            get => hunterAgility;
            set
            {
                hunterAgility = value;
                Manager.RecalculateExposeWeakness(HunterAgility);
                OnPropertyChanged(nameof(HunterAgility));
            }
        }
        public bool FaerieFireEnabled { get => faerieFireEnabled; set { faerieFireEnabled = value; OnPropertyChanged(nameof(FaerieFireEnabled)); } }
        public FaerieFire SelectedFaerieFire { get => selectedFaerieFire; set { selectedFaerieFire = value; OnPropertyChanged(nameof(SelectedFaerieFire)); } }
        public bool CurseofRecklessnessEnabled { get => curseofRecklessnessEnabled; set { curseofRecklessnessEnabled = value; OnPropertyChanged(nameof(CurseofRecklessnessEnabled)); } }
        public bool CurseoftheElementsEnabled { get => curseoftheElementsEnabled; set { curseoftheElementsEnabled = value; OnPropertyChanged(nameof(CurseoftheElementsEnabled)); } }
        public CurseoftheElements SelectedCurseoftheElements { get => selectedCurseoftheElements; set { selectedCurseoftheElements = value; OnPropertyChanged(nameof(SelectedCurseoftheElements)); } }
        public bool ImprovedShadowBoltEnabled { get => improvedShadowBoltEnabled; set { improvedShadowBoltEnabled = value; OnPropertyChanged(nameof(ImprovedShadowBoltEnabled)); } }
        public bool MiseryEnabled { get => miseryEnabled; set { miseryEnabled = value; OnPropertyChanged(nameof(MiseryEnabled)); } }
        public bool ShadowWeavingEnabled { get => shadowWeavingEnabled; set { shadowWeavingEnabled = value; OnPropertyChanged(nameof(ShadowWeavingEnabled)); } }
        public bool ImprovedScorchEnabled { get => improvedScorchEnabled; set { improvedScorchEnabled = value; OnPropertyChanged(nameof(ImprovedScorchEnabled)); } }


        public List<Spell> GetDebuffs()
        {
            HashSet<Spell> result = new();
            if (judgementoftheCrusaderEnabled && Spells.ContainsKey((int)JudgementoftheCrusader.JudgementoftheCrusader) && Spells.ContainsKey((int)selectedJudgementoftheCrusader))
            {
                result.Add(Spells[(int)JudgementoftheCrusader.JudgementoftheCrusader]);
                result.Add(Spells[(int)selectedJudgementoftheCrusader]);
            }
            if (judgementofWisdomEnabled && Spells.ContainsKey(20354))
            {
                result.Add(Spells[20354]);
            }
            if (armorDebuffEnabled)
            {
                switch (selectedArmorDebuff)
                {
                    case ArmorDebuff.ExposeArmor:
                        result.Add(Spells[(int)ArmorDebuff.ExposeArmor]);
                        break;
                    case ArmorDebuff.SunderArmor:
                        result.Add(Spells[(int)ArmorDebuff.SunderArmor]);
                        break;
                    case ArmorDebuff.ImpExposeArmor:
                        result.Add(Spells[(int)ArmorDebuff.ExposeArmor]);
                        result.Add(Spells[(int)ArmorDebuff.ImpExposeArmor]);
                        break;
                }
            }
            if (bloodFrenzyEnabled && Spells.ContainsKey(30070))
            {
                result.Add(Spells[30070]);
            }
            if (huntersMarkEnabled && Spells.ContainsKey(14325))
            {
                result.Add(Spells[14325]);
            }
            if (exposeWeaknessEnabled && Spells.ContainsKey(34501))
            {
                result.Add(Spells[34501]);
            }
            if (faerieFireEnabled && Spells.ContainsKey((int)FaerieFire.FaerieFire) && Spells.ContainsKey((int)selectedFaerieFire))
            {
                result.Add(Spells[(int)FaerieFire.FaerieFire]);
                result.Add(Spells[(int)selectedFaerieFire]);
            }
            if (curseofRecklessnessEnabled && Spells.ContainsKey(27226))
            {
                result.Add(Spells[27226]);
            }
            if (curseoftheElementsEnabled && Spells.ContainsKey((int)CurseoftheElements.CurseoftheElements) && Spells.ContainsKey((int)selectedCurseoftheElements))
            {
                result.Add(Spells[(int)CurseoftheElements.CurseoftheElements]);
                result.Add(Spells[(int)selectedCurseoftheElements]);
            }
            if (improvedShadowBoltEnabled && Spells.ContainsKey(17800))
            {
                result.Add(Spells[17800]);
            }
            if (miseryEnabled && Spells.ContainsKey(33200))
            {
                result.Add(Spells[33200]);
            }
            if (shadowWeavingEnabled && Spells.ContainsKey(15258))
            {
                result.Add(Spells[15258]);
            }
            if (improvedScorchEnabled && Spells.ContainsKey(22959))
            {
                result.Add(Spells[22959]);
            }

            return new List<Spell>(result);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum JudgementoftheCrusader
    {
        JudgementoftheCrusader = 27159,
        ImpJudgementoftheCrusader = 20337
    }
    public enum ArmorDebuff
    {
        ExposeArmor = 26866,
        SunderArmor = 25225,
        ImpExposeArmor = 14169
    }

    public enum FaerieFire
    {
        FaerieFire = 26993,
        ImpFaerieFire = 33602
    }

    public enum CurseoftheElements
    {
        CurseoftheElements = 27228,
        Malediction = 32484
    }
}
