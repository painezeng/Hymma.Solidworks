﻿using Hymma.Mathematics;
using Hymma.SolidTools.Addins;
using Hymma.SolidTools.Core;
using Hymma.SolidTools.Fluent.Addins;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        }

        public override AddinUserInterface GetUserInterFace()
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
            .WithOptions(PmpOptions.OkayButton | PmpOptions.LockedPage | PmpOptions.DisablePageBuildDuringHandlers | PmpOptions.CancelButton | PmpOptions.PushpinButton)
            .WhileClosing((reason) =>
            {
                if (reason != PMPCloseReason.Okay)
                    return;
                ModelDoc2 Part = (ModelDoc2)this.Solidworks.ActiveDoc;

                var boolstatus = Part.Extension.SelectByID2("Front Plane", "PLANE", 0, 0, 0, true, 0, null, 0);
                var myRefPlane = (RefPlane)Part.FeatureManager.InsertRefPlane(8, 0.01, 0, 0, 0, 0);
                boolstatus = Part.Extension.SelectByID2("Front Plane", "PLANE", 0, 0, 0, true, 0, null, 0);
                myRefPlane = (RefPlane)Part.FeatureManager.InsertRefPlane(8, 0.02, 0, 0, 0, 0);

                boolstatus = Part.Extension.SelectByID2("Plane2", "PLANE", 0, 0, 0, false, 0, null, 0);
                object vSkLines = null;
                vSkLines = Part.SketchManager.CreateCornerRectangle(-0.0250462141853123, 0.0157487558892494, 0, 0.0275128867944718, -0.015559011842391, 0);

                Part.SketchManager.InsertSketch(true);

                // Sketch to extrude
                boolstatus = Part.Extension.SelectByID2("Sketch1", "SKETCH", 0, 0, 0, false, 0, null, 0);
                // Start condition reference
                boolstatus = Part.Extension.SelectByID2("Plane2", "PLANE", 0.00105020593408751, -0.00195369982668282, 0.0248175428318827, true, 32, null, 0);
                // End condition reference
                boolstatus = Part.Extension.SelectByID2("Plane1", "PLANE", 0.0068370744701368, -0.004419862088339, 0.018892268568016, true, 1, null, 0);

                // Boss extrusion start condition reference is Plane2, and the extrusion end is offset 3 mm from the end condition reference, Plane1
                var myFeature = (Feature)Part.FeatureManager.FeatureExtrusion3(true, false, true, (int)swEndConditions_e.swEndCondOffsetFromSurface, 0, 0.003, 0.003, false, false, false,
                    false, 0.0174532925199433, 0.0174532925199433, false, false, false, false, true, true, true,
                    (int)swStartConditions_e.swStartSurface, 0, false);
            })
            #region Group 1
                .AddGroup("Group Caption")
                    .HasTheseControls(GetControlSet2())
                .SaveGroup()
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

        private List<IPmpControl> GetControlSet2()
        {
            var controls = new List<IPmpControl>();
            var wpfControl = new UserControl1();
            var pmpWpfControl = new PmpWindowHandler(new System.Windows.Forms.Integration.ElementHost(), wpfControl, 15);
            controls.Add(pmpWpfControl);

            var chkBx = new PmpCheckBox("checkbox");
            controls.Add(chkBx);

            var radio = new PmpRadioButton("radio button", false);
            controls.Add(radio);
            var txtBox = new PmpTextBox("text box");
            txtBox.Style = (int)TexTBoxStyles.NoBorder;
            txtBox.OnChange = (text) =>
            {
                if (text.StartsWith("t"))
                {
                    txtBox.BackGroundColor = Color.Aqua;
                    txtBox.TextColor = Color.IndianRed;
                    txtBox.Style = (int)TexTBoxStyles.ReadOnly;
                }
            };
            controls.Add(txtBox);
            return controls;
        }
    }
}
