﻿using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace Hymma.SolidTools.Addins
{
    /// <summary>
    /// a SOLIDWORKS property manager page control
    /// </summary>
    public class PmpControl<T> : IPmpControl, IWrapSolidworksObject<T>
    {
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="type">type of this controller as per <see cref="swPropertyManagerPageControlType_e"/></param>
        public PmpControl(swPropertyManagerPageControlType_e type)
        {
            Type = type;
            Options = 3;
        }

        ///<inheritdoc/>
        public swPropertyManagerPageControlType_e Type { get; set; }

        /// <inheritdoc/>
        public short LeftAlignment { get; set; }

        /// <inheritdoc/>
        public int Options { get; set; }

        /// <inheritdoc/>
        public string Caption { get; set; }

        /// <inheritdoc/>
        public string Tip { get; set; }

        /// <inheritdoc/>
        public short Id { get; set; }

        /// <summary>
        /// Sets the bitmap label for this control on a PropertyManager page.
        /// </summary>
        /// <param name="maskBitmap">Fully qualified path to the location of the bitmap (i.e., the graphic to use) on disk</param>
        /// <param name="colorBitmap">Fully qualified path to the location of the alpha mask bitmap on disk</param>
        /// <remarks>
        /// You can only use this method on a PropertyManager page before the page is displayed, while it is displayed, or when it is closed. <br/>
        /// The image format for the two bitmaps is 18 x 18 pixels x 256 colors. The pixels in MaskBitmap specify transparency through shades of grey with boundaries of black pixels = 100% opaque and white pixels = 100% transparent.
        /// </remarks>
        public virtual void SetPictureLabelByName(string colorBitmap, string maskBitmap)
        {
            Control.SetPictureLabelByName(colorBitmap, maskBitmap);
        }

        /// <summary>
        /// Displays a bubble ToolTip containing the specified title, message, and bitmap. A bubble ToolTip is useful for validating data typed or selected by users in controls on a PropertyManager page.
        /// </summary>
        /// <param name="title">Title to display in bubble ToolTip</param>
        /// <param name="message">Message to display in bubble ToolTip</param>
        /// <param name="icon">Path and filename of bitmap to display in bubble ToolTip</param>
        public void ShowBubleTooltip(string title, string message, string icon)
        {
            Control.ShowBubbleTooltip(title, message, icon);
        }

        ///<inheritdoc/>
        public virtual void Register(IPropertyManagerPageGroup group)
        {
            SolidworksObject = (T)group.AddControl2(Id, (short)Type, Caption, LeftAlignment, Options, Tip);
            Enabled = Visible = true;
        }

        /// <summary>
        /// Left edge of the control <br/>
        /// This property must be set before the control is displayed.<br/>
        /// The value is in dialog units relative to the group box that the control is in. The left edge of the group box is 0; the right edge of the group box is 100
        /// </summary>
        /// <remarks>By default, the left edge of a control is either the left edge of its group box or indented a certain distance. This is determined by the <see cref="LeftAlignment"/></remarks>
        public short LeftEdge { get => Control.Left; set => Control.Left = value; }

        /// <summary>
        /// By default, the width of the control is usually set so that it extends to the right edge of its group box (not for buttons). Using this API overrides that default.<br/>
        /// The value is in dialog units relative to the group box that the control is in. The width of the group box is 100
        /// </summary>
        public short Width { get => Control.Width; set => Control.Width = value; }

        /// <summary>
        /// Gets or sets the top edge of the control on a PropertyManager page
        /// </summary>
        public short Top { get => Control.Top; set => Control.Top = value; }

        /// <summary>
        /// Gets or sets how to override the SOLIDWORKS default behavior when changing the width of a PropertyManager page. <br/>
        /// Resize the PropertyManager page as defined in <see cref="swPropMgrPageControlOnResizeOptions_e"/>
        /// <list type="bullet">
        /// <item>
        /// <term>swControlOptionsOnResize_LockLeft </term>
        /// <description>the control is locked in place relative to the left edge of the PropertyManager page. <br/>
        /// When the page width is changed, the control stays in place and its width does not change.</description>
        /// </item>
        /// <item>
        /// <term>swControlOptionsOnResize_LockRight</term>
        /// <description>the control is locked in place relative to the right edge of the PropertyManager page.<br/>
        /// When the page width is changed, the control shifts to the right, but its width does not change.</description>
        /// </item>
        /// <item>
        /// <term>swControlOptionsOnResize_LockLeft and swControlOptionsOnResize_LockRight specified</term>
        /// <description>the left edge of the control stays in place relative to the left edge and the right edge of the control stays in place relative to the right edge of the PropertyManager page,<br/>
        /// giving the effect that the control grows and shrinks with the PropertyManager page.</description>
        /// </item>
        /// </list>
        /// </summary>
        public int OptionsForResize { get => Control.OptionsForResize; set => Control.OptionsForResize = value; }

        /// <summary>
        /// enables or disables this property control on
        /// </summary>
        public bool Enabled { get => Control.Enabled; set => Control.Enabled = value; }

        /// <summary>
        /// gets or sets the visibility of thei control
        /// </summary>
        public bool Visible { get => Control.Visible; set => Control.Visible = value; }
        
        ///<inheritdoc/>
        public T SolidworksObject
        {
            get
            {
                return (T)Control;
            }
            internal set
            {
                Control = value as IPropertyManagerPageControl;
            }
        }

        /// <summary>
        /// property manager page control that can be cast to all the property manager page members
        /// </summary>
        private IPropertyManagerPageControl Control;
    }
}
