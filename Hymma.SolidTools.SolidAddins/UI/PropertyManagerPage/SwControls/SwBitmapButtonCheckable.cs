﻿using SolidWorks.Interop.swconst;

namespace Hymma.SolidTools.SolidAddins
{
    /// <summary>
    /// a checkable bitmap button
    /// </summary>
    public class SwBitmapButtonCheckable : SwBitmapButtonCustom
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SwBitmapButtonCheckable()
        {
            Type = swPropertyManagerPageControlType_e.swControlType_CheckableBitmapButton;
        }

        /// <summary>
        /// Gets whether the bitmap button is clickable
        /// </summary>
        public bool IsCheckable { get; set; } = true;

        /// <summary>
        /// Gets or sets the state of the bitmap button
        /// </summary>
        public bool Checked { get; set; } = false;
    }
}
