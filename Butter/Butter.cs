﻿using Hymma.Mathematics;
using Hymma.SolidTools.Addins;
using Hymma.SolidTools.Core;
using Hymma.SolidTools.Fluent.Addins;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Butter
{
    [ComVisible(true)]
    [Guid("7EF4A3B3-534A-4E20-BF10-F41B9D722E05")]
    [Addin(Title = "Butter", Description = "Smooth As butter", LoadAtStartup = true, AddinIcon = "butter")]
    public class Butter : AddinMaker
    {
        private PropertyManagerPageX64 _pmp;
        public Butter() : base(typeof(Butter))
        {
            OnStart += Butter_OnStart;
            OnExit += Butter_OnExit;
        }

        private void Butter_OnExit(object sender, OnConnectToSwEventArgs e)
        {
        }

        private void Butter_OnStart(object sender, OnConnectToSwEventArgs e)
        {
            Solidworks = e.solidworks;
            Solidworks.SendMsgToUser("weldome to butter");
        }

        public override AddinUserInterface AddinUI => GetAddinUi();

        public AddinUserInterface GetAddinUi2()
        {
            var addin = new AddinUserInterface();

            #region commands

            #region command 1
            AddinCommand command1 = new AddinCommand
            {
                CallBackFunction = nameof(ShowMessage),
                EnableMethode = nameof(EnableMethode),
                IconBitmap = Properties.Resources.xtractBlue,
                Name = "command1 Name",
                ToolTip = "command1 tooltip",
                CommandTabTextType = (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow,
                UserId = 0,
                BoxId = 1,
                HintString = "hint for this command"
            };
            #endregion

            #region command2
            AddinCommand command2 = new AddinCommand
            {
                CallBackFunction = nameof(ShowMessage2),
                EnableMethode = nameof(EnableMethode),
                IconBitmap = Properties.Resources.xtractOrange,
                Name = "command2 Name",
                ToolTip = "command 2 's tooltip",
                CommandTabTextType = (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow,
                UserId = 1,
                BoxId = 0,
                HintString = "hint for this command"
            };

            #endregion

            #region command3
            AddinCommand command3 = new AddinCommand();
            command3.CallBackFunction = nameof(ShowPMP);
            command3.EnableMethode = nameof(EnableMethode);
            command3.IconBitmap = Properties.Resources.xtractred;
            command3.Name = "command 3 's Name";
            command3.ToolTip = "command 3 's tooltip";
            command3.CommandTabTextType = (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow;
            command3.UserId = 2;
            command3.BoxId = 1;
            command3.HintString = "hint for this command";
            #endregion

            #endregion

            #region command Group
            var cmdGroup = new AddinCommandGroup(7, new[] { command1, command2, command3 },
                "A title for command group",
                "description for command group",
                "tooltip for thic command group",
                "hint of this command gorup",
                Properties.Resources.xtractred);
            #endregion

            #region Command Tabs
            AddinCommandTab tab1 = new AddinCommandTab()
            {
                TabTitle = "tab title",
                Types = new swDocumentTypes_e[] { swDocumentTypes_e.swDocASSEMBLY, swDocumentTypes_e.swDocDRAWING, swDocumentTypes_e.swDocPART },
                CommandGroup = cmdGroup
            };

            addin.CommandTabs.Add(tab1);
            #endregion

            #region property manager page
            _pmp = new PropertyManagerPageX64(new PMPUi(Solidworks));
            addin.PropertyManagerPages.Add(_pmp);
            #endregion

            return addin;
        }

        public AddinUserInterface GetAddinUi()
        {
            var builder = new AddinFactory().GetUiBuilder();

            #region Command Tab

            builder.AddCommandTab()
                .WithTitle("Butter")
                .That()
                .IsVisibleIn(new[] { swDocumentTypes_e.swDocASSEMBLY, swDocumentTypes_e.swDocDRAWING, swDocumentTypes_e.swDocPART })
                .AddCommandGroup(1)
                    .WithTitle("Butter")
                    .WithDescription("smooth as butter")
                    .WithToolTip("Melt a butter")
                    .WithHint("Melt a butter")
                    .WithIcon(Properties.Resources.xtractred)
                    .Has()
                    .Commands(() =>
                    {
                        var command1 = new AddinCommand("Melt the butter", "easily melt a butter", "Melt the butter", Properties.Resources.butter, nameof(ShowPMP));
                        return new[] { command1 };
                    })
                    .SaveCommnadGroup().
                SaveCommandTab();
            #endregion

            #region Butter PMP
            builder.AddPropertyManagerPage("Butter", Solidworks)

            #region Group 1
                .AddGroup("Group Caption")
                    .HasTheseControls(() =>
                    {
                        var controls = new List<IPmpControl>
                        {
                            new PmpCheckBox(true) { Tip = "a tip for a checkbox", Caption = "default checkbox" },
                            new PmpCheckBox(Properties.Settings.Default.ChkBoxChkd)
                            {
                                Tip = "a tip for a checkbox 2 ",
                                Caption = "checkbox with settings assigned",
                                OnChecked = (isChecked) =>
                                {
                                    Solidworks.SendMsgToUser($"you have clicked on check box  {isChecked}");
                                    Properties.Settings.Default.ChkBoxChkd = isChecked;
                                }
                            },

                            new PmpRadioButton(true)
                            {
                                Tip = "radio button on group 1",
                                Caption = "caption for radio button on group 1",
                                OnChecked = () => { Solidworks.SendMsgToUser("first radio button clicked on"); }

                            },

                            new PmpRadioButton(false)
                            {
                                Tip = "radio button on group 1",
                                Caption = "caption for radio button on group 1",
                                OnChecked = () => { Solidworks.SendMsgToUser("first radio button clicked on"); }
                            }
                        };
                        return controls;
                    })
                .SaveGroup()
            #endregion

            #region Group 2
                    .AddGroup("Radio Buttons")
                .HasTheseControls(() =>
                {
                    var controls = new List<IPmpControl>();
                    controls.Add(new PmpRadioButton(true)
                    {
                        Tip = "a tip for this radio button",
                        Caption = "caption for this radio button",
                        OnChecked = () => { Solidworks.SendMsgToUser($"radio button is checked"); }
                    });

                    controls.Add(new PmpRadioButton()
                    {
                        Tip = "another tip for this radio button",
                        Caption = "yet another caption for a radio button",
                        OnChecked = () => { Solidworks.SendMsgToUser($"radio button is checked"); }
                    });

                    controls.Add(
                        new PmpSelectionBox(new swSelectType_e[] { swSelectType_e.swSelEDGES }, SelectionBoxStyles.MultipleItemSelect)
                        {
                            Tip = "a tip for this selection box",
                            Caption = "Caption for this selectionbox",
                            OnSubmitSelection = (selection, type, tag) =>
                            {
                                if (type != (int)swSelectType_e.swSelEDGES)
                                {
                                    Solidworks.SendMsgToUser("only edges are allowed to select");
                                    return false;
                                }
                                return true;
                            }
                        });
                    return controls;
                }).SaveGroup()
            #endregion

            #region selection box and callout
                    .AddGroup("Selection Boxe")
                    .IsExpanded(true)
                    .HasTheseControls(() =>
                    {
                        var selBox = new PmpSelectionBox(new swSelectType_e[] { swSelectType_e.swSelEDGES }, SelectionBoxStyles.UpAndDownButtons)
                        {
                            Caption = "caption for selection box with callout",
                            Enabled = false
                        };

                        /*var rows = new List<CalloutRow>
                         {
                            new CalloutRow("value 1", "row 1") { Target = new Point(0.1, 0.1, 0.1), TextColor = SysColor.Highlight },
                            new CalloutRow("value 2", "row 2") { Target = new Point(0, 0, 0), TextColor = SysColor.AsmInterferenceVolume }
                        };
                        selBox.CalloutModel = new CalloutModel(rows, Solidworks, (ModelDoc2)Solidworks.ActiveDoc);*/
                        return new[] { selBox };
                    }).SaveGroup()
            #endregion
            .SavePropertyManagerPage(out PropertyManagerPageX64 pmp);
            _pmp = pmp;

            #endregion

            return (AddinUserInterface)builder;
        }

        public void ShowPMP()
        {
            _pmp.Show();
        }

        public int EnableMethode()
        {
            if (Solidworks.ActiveDoc != null)
                return 1;
            return 0;
        }

        public void ShowMessage()
        {
            Solidworks.SendMsgToUser2("message from Butter", 0, 0);
        }

        public void ShowMessage2()
        {
            Solidworks.SendMsgToUser2("message 2 from Butter", 0, 0);
        }

        private List<IPmpControl> GetControlSet1()
        {
            var controls = new List<IPmpControl>
            {
                new PmpCheckBox(true) { Tip = "a tip for a checkbox", Caption = "caption for chckbox" },

                new PmpCheckBox(Properties.Settings.Default.ChkBoxChkd)
                {
                    Tip = "a tip for a checkbox 2 ",
                    Caption = "caption for chckbox 2",
                    OnChecked = (isChecked) =>
                    {
                        Solidworks.SendMsgToUser($"you have clicked on check box  {isChecked}");
                        Properties.Settings.Default.ChkBoxChkd = isChecked;
                    }
                },

                new PmpRadioButton(true)
                {
                    Tip = "radio button on group 1",
                    Caption = "caption for radio button on group 1",
                    OnChecked = () => { Solidworks.SendMsgToUser("first radio button clicked on"); }

                },
                new PmpRadioButton(false)
                {
                    Tip = "radio button on group 1",
                    Caption = "caption for radio button on group 1",
                    OnChecked = () => { Solidworks.SendMsgToUser("first radio button clicked on"); }
                }
            };

            return controls;
        }

        private List<IPmpControl> GetControlSet2()
        {
            var controls = new List<IPmpControl>();
            controls.Add(new PmpRadioButton(true)
            {
                Tip = "a tip for this radio button",
                Caption = "caption for this radio button",
                OnChecked = () => { Solidworks.SendMsgToUser($"radio button is checked"); }
            });

            controls.Add(new PmpRadioButton()
            {
                Tip = "another tip for this radio button",
                Caption = "yet another caption for a radio button",
                OnChecked = () => { Solidworks.SendMsgToUser($"radio button is checked"); }
            });

            controls.Add(
                new PmpSelectionBox(new swSelectType_e[] { swSelectType_e.swSelEDGES }, SelectionBoxStyles.UpAndDownButtons)
                {
                    Tip = "a tip for this selection box",
                    Caption = "Caption for this selectionbox",
                    OnSubmitSelection = (selection, type, tag) =>
                    {
                        if (type != (int)swSelectType_e.swSelEDGES)
                        {
                            Solidworks.SendMsgToUser("only edges are allowed to select");
                            return false;
                        }
                        return true;
                    }

                });
            return controls;
        }

        private PmpSelectionBox GetSelectionBoxWithCallout(SldWorks solidworks)
        {
            var selBox = new PmpSelectionBox(new swSelectType_e[] { swSelectType_e.swSelEDGES }, SelectionBoxStyles.UpAndDownButtons)
            {
                Caption = "caption for selection box with callout"
            };
            var rows = new List<CalloutRow>
            {
                new CalloutRow("value 1", "row 1") { Target = new Point(0.1, 0.1, 0.1), TextColor = SysColor.Highlight },
                new CalloutRow("value 2", "row 2") { Target = new Point(0, 0, 0), TextColor = SysColor.AsmInterferenceVolume }
            };
            selBox.CalloutModel = new CalloutModel(rows, solidworks, (ModelDoc2)solidworks.ActiveDoc);
            return selBox;
        }
    }
}
