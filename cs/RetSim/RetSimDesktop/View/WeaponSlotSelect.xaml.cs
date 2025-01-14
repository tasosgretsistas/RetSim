﻿using RetSim.Items;
using RetSim.Misc;
using RetSim.Units.UnitStats;
using RetSimDesktop.Model;
using RetSimDesktop.View;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearSlotSelect.xaml
    /// </summary>
    public partial class WeaponSlotSelect : UserControl
    {
        private static WeaponSim weaponSimWorker = new();

        public List<DisplayWeapon> WeaponList
        {
            get => (List<DisplayWeapon>)GetValue(WeaponListProperty);
            set => SetValue(WeaponListProperty, value);
        }

        public static readonly DependencyProperty WeaponListProperty = DependencyProperty.Register(
            "WeaponList",
            typeof(List<DisplayWeapon>),
            typeof(WeaponSlotSelect));

        public List<Enchant> WeaponEnchantList
        {
            get => (List<Enchant>)GetValue(WeaponEnchantListProperty);
            set => SetValue(WeaponEnchantListProperty, value);
        }

        public static readonly DependencyProperty WeaponEnchantListProperty = DependencyProperty.Register(
            "WeaponEnchantList",
            typeof(List<Enchant>),
            typeof(WeaponSlotSelect));

        public DisplayWeapon SelectedItem
        {
            get => (DisplayWeapon)GetValue(SelectedItemProperty);
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(DisplayWeapon),
            typeof(WeaponSlotSelect),
            new PropertyMetadata(null, CheckIfSelectionIsPresent));

        public Enchant SelectedWeaponEnchant
        {
            get => (Enchant)GetValue(SelectedWeaponEnchantProperty);
            set => SetValue(SelectedWeaponEnchantProperty, value);
        }

        public static readonly DependencyProperty SelectedWeaponEnchantProperty = DependencyProperty.Register(
            "SelectedWeaponEnchant",
            typeof(Enchant),
            typeof(WeaponSlotSelect));


        public WeaponSlotSelect()
        {
            InitializeComponent();

            gearSlot.SetBinding(DataGrid.ItemsSourceProperty, new Binding("WeaponList")
            {
                Source = this,
                Mode = BindingMode.OneWay
            });

            gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });

            WeaponEnchantComboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding("WeaponEnchantList")
            {
                Source = this,
                Mode = BindingMode.OneWay,
            });

            WeaponEnchantComboBox.SetBinding(ComboBox.SelectedItemProperty, new Binding("SelectedWeaponEnchant")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });

            StatConverter statConverter = new();

            Binding strBinding = new("Weapon.Stats[" + StatName.Strength + "]");
            strBinding.Converter = statConverter;
            StrColumn.Binding = strBinding;
            Binding apBinding = new("Weapon.Stats[" + StatName.AttackPower + "]");
            apBinding.Converter = statConverter;
            APColumn.Binding = apBinding;
            Binding agiBinding = new("Weapon.Stats[" + StatName.Agility + "]");
            agiBinding.Converter = statConverter;
            AgiColumn.Binding = agiBinding;
            Binding critBinding = new("Weapon.Stats[" + StatName.CritRating + "]");
            critBinding.Converter = statConverter;
            CritColumn.Binding = critBinding;
            Binding hitBinding = new("Weapon.Stats[" + StatName.HitRating + "]");
            hitBinding.Converter = statConverter;
            HitColumn.Binding = hitBinding;
            Binding hasteBinding = new("Weapon.Stats[" + StatName.HasteRating + "]");
            hasteBinding.Converter = statConverter;
            HasteColumn.Binding = hasteBinding;
            Binding expBinding = new("Weapon.Stats[" + StatName.ExpertiseRating + "]");
            expBinding.Converter = statConverter;
            ExpColumn.Binding = expBinding;
            Binding apenBinding = new("Weapon.Stats[" + StatName.ArmorPenetration + "]");
            apenBinding.Converter = statConverter;
            APenColumn.Binding = apenBinding;
            Binding staBinding = new("Weapon.Stats[" + StatName.Stamina + "]");
            staBinding.Converter = statConverter;
            StaColumn.Binding = staBinding;
            Binding intBinding = new("Weapon.Stats[" + StatName.Intellect + "]");
            intBinding.Converter = statConverter;
            IntColumn.Binding = intBinding;
            Binding mp5Binding = new("Weapon.Stats[" + StatName.ManaPer5 + "]");
            mp5Binding.Converter = statConverter;
            MP5Column.Binding = mp5Binding;
            Binding spBinding = new("Weapon.Stats[" + StatName.SpellPower + "]");
            spBinding.Converter = statConverter;
            SPColumn.Binding = spBinding;
        }

        private void CheckIfSelectionIsPresent()
        {
            if (!WeaponList.Contains(SelectedItem))
            {
                BindingOperations.ClearBinding(gearSlot, DataGrid.SelectedItemProperty);
                gearSlot.SelectedCells.Clear();
            }
            else
            {
                gearSlot.SelectedItem = SelectedItem;
                gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                });
            }
        }

        private void GearSlotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = (DisplayWeapon)gearSlot.SelectedItem;
            gearSlot.SetBinding(DataGrid.SelectedItemProperty, new Binding("SelectedItem")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            });
        }

        private static void CheckIfSelectionIsPresent(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WeaponSlotSelect select)
            {
                select.CheckIfSelectionIsPresent();
            }
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGridCellTarget = (DataGridCell)sender;
            var header = dataGridCellTarget.Column.Header.ToString();

            Socket? selectedSocket = null;
            if (header == "Socket 1")
            {
                selectedSocket = SelectedItem.Weapon.Socket1;
            }
            else if (header == "Socket 2")
            {
                selectedSocket = SelectedItem.Weapon.Socket2;
            }
            else if (header == "Socket 3")
            {
                selectedSocket = SelectedItem.Weapon.Socket3;
            }

            if (selectedSocket != null)
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    GemPickerWindow gemPicker;
                    if (selectedSocket.Color == SocketColor.Meta)
                    {
                        gemPicker = new(RetSim.Data.Items.MetaGems.Values, selectedSocket.SocketedGem);
                    }
                    else
                    {
                        gemPicker = new(RetSim.Data.Items.Gems.Values, selectedSocket.SocketedGem);
                    }

                    retSimUIModel.TooltipSettings.HoverItemID = 0;
                    if (gemPicker.ShowDialog() == true)
                    {
                        selectedSocket.SocketedGem = gemPicker.SelectedGem;

                        retSimUIModel.SelectedGear.OnPropertyChanged("");
                        SelectedItem.OnPropertyChanged("");
                    }
                }
            }
        }

        private void Weapon_Click(object sender, RoutedEventArgs e)
        {
            if (!weaponSimWorker.IsBusy && DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = false;
                weaponSimWorker.RunWorkerAsync(new Tuple<RetSimUIModel, IEnumerable<DisplayWeapon>, int>(retSimUIModel, WeaponList, Constants.EquipmentSlots.Weapon));
            }
        }

        private void ChkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if (WeaponList != null)
            {
                foreach (var displayItem in WeaponList)
                {
                    displayItem.EnabledForGearSim = true;
                }
            }
        }

        private void ChkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            if (WeaponList != null)
            {
                foreach (var displayItem in WeaponList)
                {
                    displayItem.EnabledForGearSim = false;
                }
            }
        }

        private void DataGridCell_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridCell cell)
            {
                if (DataContext is RetSimUIModel retSimUIModel && DataGridRow.GetRowContainingElement(cell).Item is DisplayWeapon displayWeapon)
                {
                    if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
                    {
                        System.Diagnostics.Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://tbc.wowhead.com/item=" + displayWeapon.Weapon.ID,
                            UseShellExecute = true
                        });
                    }
                    else if (e.ChangedButton == MouseButton.Right && e.ButtonState == MouseButtonState.Pressed)
                    {
                        var header = cell.Column.Header.ToString();
                        if (header == "Socket 1" && displayWeapon.Weapon.Socket1 != null)
                        {
                            displayWeapon.Weapon.Socket1.SocketedGem = null;
                        }
                        else if (header == "Socket 2" && displayWeapon.Weapon.Socket2 != null)
                        {
                            displayWeapon.Weapon.Socket2.SocketedGem = null;
                        }
                        else if (header == "Socket 3" && displayWeapon.Weapon.Socket3 != null)
                        {
                            displayWeapon.Weapon.Socket3.SocketedGem = null;
                        }
                        displayWeapon.OnPropertyChanged("");
                        retSimUIModel.SelectedGear.OnPropertyChanged("");
                        DataGridCell_MouseEnter(cell, null);
                    }
                }
            }
        }

        private void DataGridCell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DataGridCell cell)
            {
                if (DataContext is RetSimUIModel retSimUIModel && DataGridRow.GetRowContainingElement(cell).Item is DisplayWeapon displayWeapon)
                {
                    var header = cell.Column.Header.ToString();

                    if (header == "Socket 1" && displayWeapon.Weapon.Socket1 != null && displayWeapon.Weapon.Socket1.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayWeapon.Weapon.Socket1.SocketedGem.ID;
                    }
                    else if (header == "Socket 2" && displayWeapon.Weapon.Socket2 != null && displayWeapon.Weapon.Socket2.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayWeapon.Weapon.Socket2.SocketedGem.ID;
                    }
                    else if (header == "Socket 3" && displayWeapon.Weapon.Socket3 != null && displayWeapon.Weapon.Socket3.SocketedGem != null)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayWeapon.Weapon.Socket3.SocketedGem.ID;
                    }
                    else if (retSimUIModel.TooltipSettings.HoverItemID != displayWeapon.Weapon.ID)
                    {
                        retSimUIModel.TooltipSettings.HoverItemID = displayWeapon.Weapon.ID;
                    }
                }
            }
        }

        private void DataGridCell_MouseLeave(object sender, MouseEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                retSimUIModel.TooltipSettings.HoverItemID = 0;
            }
        }
    }

    public class WeaponSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (((int)value) / 1000f).ToString("0.#");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class WeaponDPSConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((float)value).ToString("0.##");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

