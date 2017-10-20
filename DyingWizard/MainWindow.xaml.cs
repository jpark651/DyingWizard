using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DyingWizard
{
    public partial class MainWindow : Window
    {
        private int currentScreen = 0;

        public MainWindow()
        {
            InitializeComponent();
            descBox1.Text = "• HP is positive.\n\n• You recover HP naturally.";
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset("Normal", Colors.LimeGreen, 0);
            descBox1.Text = "• HP is positive.\n\n• You recover HP naturally.";
        }

        private void Reset(string status, Color color, int screen)
        {
            currentScreen = screen;
            statusBox.Text = status;
            statusBox.Foreground = new SolidColorBrush(color);
            menuHP1.Content = "negative";
            menuDmg1.Content = "lethal";
            menuCheck2.Content = "succeeded at the check";
            menuCheck3.Content = "succeeded at the check";
            menuCheck4.Content = "succeeded at the check";
            menuCheck5.Content = "was dealt lethal damage";
            menuHP1.Visibility = System.Windows.Visibility.Visible;
            menuDmg1.Visibility = System.Windows.Visibility.Visible;
            optionBox1.Visibility = System.Windows.Visibility.Visible;
            optionBox2.Visibility = System.Windows.Visibility.Hidden;
            menuCheck2.Visibility = System.Windows.Visibility.Hidden;
            statusBox2.Visibility = System.Windows.Visibility.Hidden;
            menuCheck3.Visibility = System.Windows.Visibility.Hidden;
            menuCheck4.Visibility = System.Windows.Visibility.Hidden;
            menuCheck5.Visibility = System.Windows.Visibility.Hidden;
        }

        private void VDying()
        {
            menuHP1.Visibility = System.Windows.Visibility.Hidden;
            menuDmg1.Visibility = System.Windows.Visibility.Hidden;
            optionBox1.Visibility = System.Windows.Visibility.Hidden;
            optionBox2.Visibility = System.Windows.Visibility.Visible;
            menuCheck2.Visibility = System.Windows.Visibility.Visible;
            statusBox2.Visibility = System.Windows.Visibility.Hidden;
            menuCheck3.Visibility = System.Windows.Visibility.Hidden;
            menuCheck4.Visibility = System.Windows.Visibility.Hidden;
            menuCheck5.Visibility = System.Windows.Visibility.Hidden;
        }

        private void VStable()
        {
            menuHP1.Visibility = System.Windows.Visibility.Hidden;
            menuDmg1.Visibility = System.Windows.Visibility.Hidden;
            optionBox1.Visibility = System.Windows.Visibility.Hidden;
            optionBox2.Visibility = System.Windows.Visibility.Visible;
            menuCheck2.Visibility = System.Windows.Visibility.Hidden;
            statusBox2.Visibility = System.Windows.Visibility.Hidden;
            menuCheck3.Visibility = System.Windows.Visibility.Visible;
            menuCheck4.Visibility = System.Windows.Visibility.Hidden;
            menuCheck5.Visibility = System.Windows.Visibility.Hidden;
        }

        private void VDisabled()
        {
            VStable();
            if (currentScreen == 3)
            {
                menuCheck3.Visibility = System.Windows.Visibility.Hidden;
                menuCheck5.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Unaided(string status, Color color, int screen)
        {
            menuHP1.Content = "negative";
            menuDmg1.Content = "lethal";
            menuCheck2.Content = "succeeded at the check";
            menuCheck3.Content = "succeeded at the check";
            menuCheck4.Content = "succeeded at the check";
            menuCheck5.Content = "was dealt lethal damage";
            currentScreen = screen;
            statusBox.Text = "(Unaided)";
            statusBox.Foreground = new SolidColorBrush(Colors.Red);
            statusBox2.Text = status;
            statusBox2.Foreground = new SolidColorBrush(color);
            statusBox2.Visibility = System.Windows.Visibility.Visible;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentScreen == 0) // "Normal" screen
            {
                if (menuHP1.Content.Equals("negative"))
                {
                    if (menuDmg1.Content.Equals("lethal"))
                    {
                        Reset("Dying", Colors.Red, 1);
                        VDying();
                        descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• If you begin your turn \"dying\", make a DC 10 Constitution check to become \"stable\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• Failed the check? Lose 1 HP.\n\n• Death occurs when:\nnegative HP >= CON score.";
                    }
                    else // DMG is nonlethal
                    {
                        Reset("Nonlethal Negative HP", Colors.LightGray, 0);
                        descBox1.Text = "• HP is not negative.\n\n• Nonlethal damage > current HP.\n\n• \"Unconscious\" condition.\n\n• Any nonlethal damage that exceeds your max HP (not your current HP) becomes lethal damage.\n\n• You recover HP naturally.";
                    }
                }
                else // HP is "0"
                {
                    if (menuDmg1.Content.Equals("lethal"))
                    {
                        Reset("Disabled", Colors.Yellow, 3);
                        VDisabled();
                        descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                    }
                    else // DMG is nonlethal
                    {
                        Reset("Nonlethal 0 HP", Colors.LimeGreen, 0);
                        descBox1.Text = "• HP is positive.\n\n• Nonlethal damage = current HP.\n\n• \"Staggered\" condition.\n\n• Any nonlethal damage that exceeds your max HP (not your current HP) becomes lethal damage.\n\n• You recover HP naturally.";
                    }
                }
            }

            else if (currentScreen == 1) // "Dying" screen
            {
                if (menuCheck2.Content.Equals("succeeded at the check"))
                {
                    VStable();
                    Unaided("Stable", Colors.Orange, 4);
                    descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• Every hour, make a DC 10 Constitution check to become \"disabled\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• Failed the check? Lose 1 HP.\n\n• Death occurs when:\nnegative HP >= CON score.";
                }
                else if (menuCheck2.Content.Equals("was healed (HP is = 0)"))
                {
                    Reset("Disabled", Colors.Yellow, 3);
                    VDisabled();
                    descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                }
                else if (menuCheck2.Content.Equals("was healed (HP is > 0)"))
                {
                    Reset("Normal", Colors.LimeGreen, 0);
                    descBox1.Text = "• HP is positive.\n\n• You recover HP naturally.";
                }
                else // Stable
                {
                    Reset("Stable", Colors.Orange, 2);
                    VStable();
                    menuCheck3.Visibility = System.Windows.Visibility.Hidden;
                    menuCheck4.Visibility = System.Windows.Visibility.Visible;
                    descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• Every hour, make a DC 10 Constitution check to become \"disabled\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• You recover HP naturally.";
                }
            }

            else if (currentScreen == 2) // "Stable" screen
            {
                if (menuCheck4.Content.Equals("succeeded at the check"))
                {
                    Reset("Disabled", Colors.Yellow, 3);
                    VDisabled();
                    descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                }
                else if (menuCheck4.Content.Equals("was healed (HP is = 0)"))
                {
                    Reset("Disabled", Colors.Yellow, 3);
                    VDisabled();
                    descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                }
                else if (menuCheck4.Content.Equals("was healed (HP is > 0)"))
                {
                    Reset("Normal", Colors.LimeGreen, 0);
                    descBox1.Text = "• HP is positive.\n\n• You recover HP naturally.";
                }
                else // Dealt lethal damage
                {
                    Reset("Dying", Colors.Red, 1);
                    VDying();
                    descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• If you begin your turn \"dying\", make a DC 10 Constitution check to become \"stable\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• Failed the check? Lose 1 HP.\n\n• Death occurs when:\nnegative HP >= CON score.";
                }
            }

            else if (currentScreen == 3) // "Disabled" screen
            {
                if (menuCheck5.Content.Equals("regained HP (HP is > 0)"))
                {
                    Reset("Normal", Colors.LimeGreen, 0);
                    descBox1.Text = "• HP is positive.\n\n• You recover HP naturally.";
                }
                else // Dealt lethal damage
                {
                    Reset("Dying", Colors.Red, 1);
                    VDying();
                    descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• If you begin your turn \"dying\", make a DC 10 Constitution check to become \"stable\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• Failed the check? Lose 1 HP.\n\n• Death occurs when:\nnegative HP >= CON score.";
                }
            }

            else if (currentScreen == 4) // "Unaided Stable" screen
            {
                if (menuCheck3.Content.Equals("succeeded at the check"))
                {
                    VDisabled();
                    Unaided("Disabled", Colors.Yellow, 5);
                    descBox1.Text = "• HP is 0 or lower.\n• \"Staggered\" condition.\n• Move at half speed.\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• Each day, after 8 hours of rest, you can make a DC 10 Constitution check to start recovering HP naturally. Apply your negative HP as a penalty to the roll.\n• Natural 20? Automatic success.\n• Failed the check? Lose 1 HP.\n• Death occurs when:\nnegative HP >= CON score";
                }
                else if (menuCheck3.Content.Equals("was healed (HP is = 0)"))
                {
                    Reset("Disabled", Colors.Yellow, 3);
                    VDisabled();
                    descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                }
                else if (menuCheck3.Content.Equals("was healed (HP is > 0)"))
                {
                    Reset("Normal", Colors.LimeGreen, 0);
                    descBox1.Text = "• HP is positive.\n\n• You recover HP naturally.";
                }
                else if (menuCheck3.Content.Equals("was dealt lethal damage"))
                {
                    Reset("Dying", Colors.Red, 1);
                    VDying();
                    descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• If you begin your turn \"dying\", make a DC 10 Constitution check to become \"stable\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• Failed the check? Lose 1 HP.\n\n• Death occurs when:\nnegative HP >= CON score.";
                }
                else // Stable
                {
                    Reset("Stable", Colors.Orange, 2);
                    VStable();
                    menuCheck3.Visibility = System.Windows.Visibility.Hidden;
                    menuCheck4.Visibility = System.Windows.Visibility.Visible;
                    descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• Every hour, make a DC 10 Constitution check to become \"disabled\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• You recover HP naturally.";
                }
            }

            else if (currentScreen == 5) // "Unaided Disabled" screen
            {
                if (menuCheck3.Content.Equals("succeeded at the check"))
                {
                    Reset("Disabled", Colors.Yellow, 3);
                    VDisabled();
                    descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                }
                else if (menuCheck3.Content.Equals("was healed (HP is = 0)"))
                {
                    Reset("Disabled", Colors.Yellow, 3);
                    VDisabled();
                    descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                }
                else if (menuCheck3.Content.Equals("was healed (HP is > 0)"))
                {
                    Reset("Normal", Colors.LimeGreen, 0);
                    descBox1.Text = "• HP is positive.\n\n• You recover HP naturally.";
                }
                else if (menuCheck3.Content.Equals("was dealt lethal damage"))
                {
                    Reset("Dying", Colors.Red, 1);
                    VDying();
                    descBox1.Text = "• HP is negative.\n\n• \"Unconscious\" condition.\n\n• If you begin your turn \"dying\", make a DC 10 Constitution check to become \"stable\". Apply your negative HP as a penalty to the roll.\n\n• Natural 20? Automatic success.\n\n• Failed the check? Lose 1 HP.\n\n• Death occurs when:\nnegative HP >= CON score.";
                }
                else // Disabled
                {
                    Reset("Disabled", Colors.Yellow, 3);
                    VDisabled();
                    descBox1.Text = "• HP is 0 or lower.\n\n• \"Staggered\" condition.\n\n• Move at half speed.\n\n• Done performing a standard (or strenuous) action? You receive 1 point of lethal damage. If your action did not increase your HP, you are \"dying\".\n\n• You recover HP naturally.";
                }
            }
        }

        private void menuHP1_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void menuDmg1_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void menuCheck2_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void menuCheck3_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void menuCheck4_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void menuCheck5_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void negative1_Click(object sender, RoutedEventArgs e)
        {
            menuHP1.Content = "negative";
        }

        private void zero1_Click(object sender, RoutedEventArgs e)
        {
            menuHP1.Content = "0";
        }

        private void lethal1_Click(object sender, RoutedEventArgs e)
        {
            menuDmg1.Content = "lethal";
        }

        private void nonlethal1_Click(object sender, RoutedEventArgs e)
        {
            menuDmg1.Content = "nonlethal";
        }

        private void succeed2_Click(object sender, RoutedEventArgs e)
        {
            menuCheck2.Content = "succeeded at the check";
        }

        private void healn2_Click(object sender, RoutedEventArgs e)
        {
            menuCheck2.Content = "was healed (HP is < 0)";
        }

        private void heal02_Click(object sender, RoutedEventArgs e)
        {
            menuCheck2.Content = "was healed (HP is = 0)";
        }

        private void healp2_Click(object sender, RoutedEventArgs e)
        {
            menuCheck2.Content = "was healed (HP is > 0)";
        }

        private void succeed3_Click(object sender, RoutedEventArgs e)
        {
            menuCheck3.Content = "succeeded at the check";
        }

        private void healn3_Click(object sender, RoutedEventArgs e)
        {
            menuCheck3.Content = "was healed (HP is < 0)";
        }

        private void heal03_Click(object sender, RoutedEventArgs e)
        {
            menuCheck3.Content = "was healed (HP is = 0)";
        }

        private void healp3_Click(object sender, RoutedEventArgs e)
        {
            menuCheck3.Content = "was healed (HP is > 0)";
        }

        private void dmg3_Click(object sender, RoutedEventArgs e)
        {
            menuCheck3.Content = "was dealt lethal damage";
        }

        private void succeed4_Click(object sender, RoutedEventArgs e)
        {
            menuCheck4.Content = "succeeded at the check";
        }

        private void heal04_Click(object sender, RoutedEventArgs e)
        {
            menuCheck4.Content = "was healed (HP is = 0)";
        }

        private void healp4_Click(object sender, RoutedEventArgs e)
        {
            menuCheck4.Content = "was healed (HP is > 0)";
        }

        private void dmg4_Click(object sender, RoutedEventArgs e)
        {
            menuCheck4.Content = "was dealt lethal damage";
        }

        private void healp5_Click(object sender, RoutedEventArgs e)
        {
            menuCheck5.Content = "regained HP (HP is > 0)";
        }

        private void dmg5_Click(object sender, RoutedEventArgs e)
        {
            menuCheck5.Content = "was dealt lethal damage";
        }
    }
}