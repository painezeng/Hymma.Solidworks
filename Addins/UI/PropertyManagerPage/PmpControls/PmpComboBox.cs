﻿using SolidWorks.Interop.swconst;
using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System;

namespace Hymma.SolidTools.Addins
{
    /// <summary>
    /// a combo box with multiple text values
    /// </summary>
    public class PmpComboBox : PmpControl<PropertyManagerPageCombobox>
    {
        private readonly string[] _items;
        private readonly ComboBoxStyles _style;
        private readonly short _height;

        /// <summary>
        /// default constructor
        /// </summary>
        public PmpComboBox(string[] items, ComboBoxStyles style, short height = 50) : base(swPropertyManagerPageControlType_e.swControlType_Combobox)
        {
            _items = items;
            _style = style;
            _height = height;
            OnRegister += PmpComboBox_OnRegister;
        }

        private void PmpComboBox_OnRegister()
        {
            AddItems(_items);
            Style = (int)_style;
            Height = _height;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds items to the attached drop-down list for this combo box. 
        /// </summary>
        public void AddItems(string[] items)
        {
            if (SolidworksObject != null)
                SolidworksObject.AddItems(items);
        }

        /// <summary>
        /// Gets the text from the attached drop-down list for this combo box. 
        /// </summary>
        /// <param name="index">Position of the item where to get the text in the 0-based list of items or -1 to get the text of the currently selected item</param>
        /// <returns>text of the specified item</returns>
        public string GetItem(short index)
        {
            if (SolidworksObject!=null)
                return SolidworksObject.ItemText[index];
            return "";
        }

        /// <summary>
        /// Clears all items from the attached drop-down list for this combo box.  
        /// </summary>
        public void Clear()
        {
            if (SolidworksObject != null)
            {
                SolidworksObject.Clear();
            }
        }
        
        /// <summary>
        /// deletes an item from the attached drop-down list for this combo box
        /// </summary>
        /// <param name="index"></param>
        public void Delete(short index)
        {
            if (SolidworksObject!=null)
            {
                SolidworksObject.DeleteItem(index);
            }
        }
        /// <summary>
        /// Inserts an item in the attached drop-down list of this combo box. 
        /// </summary>
        /// <param name="index">Position where to add the item in the 0-based list or -1 to put the item at the end of the list</param>
        /// <param name="item">item to add</param>
        public void InsertItem(short index, string item)
        {
            if (SolidworksObject!=null)
            {
                SolidworksObject.InsertItem(index, item);
            }
        }
        /// <summary>
        /// height of the combo box
        /// </summary>
        public short Height
        {
            get
            {
                if (SolidworksObject != null)
                    return SolidworksObject.Height;
                return -1;
            }
            set
            {
                if (SolidworksObject!=null)
                {
                    SolidworksObject.Height = value;
                }
            }
        }

        /// <summary>
        /// gets or sets the style for the attached drop down list for this combobox
        /// </summary>
        /// <remarks>Style is a combination of Boolean values, each represented by a bit in this long value. The different Boolean values are represented in the swPropMgrPageComboBoxStyle_e enumeration. <br/>
        /// For example, to set the attached drop-down list of a combo box so that the items are sorted, set Style to <see cref="ComboBoxStyles.Sorted"/>.
        ///<para>The control style must be set before the PropertyManager page is displayed except if setting Style <see cref="ComboBoxStyles.EditBoxReadOnly"/>. You can set style either before or after the PropertyManager page is displayed.
        ///</para>
        ///</remarks>
        ///<value>
        ///Combo-box style as defined in <see cref="ComboBoxStyles"/></value>
        public int Style
        {
            get
            {
                if (SolidworksObject != null)
                    return SolidworksObject.Style;
                return 0;
            }
            set
            {
                if (SolidworksObject != null)
                    SolidworksObject.Style = value;
            }
        }

        /// <summary>
        /// Gets and sets the item that is currently selected for this combo box. 
        /// </summary>
        /// <remarks>0-based index</remarks>
        public short CurrentSelection
        {
            get
            {
                if (SolidworksObject != null)
                    return SolidworksObject.CurrentSelection;
                return -1;
            }
            set
            {
                if (SolidworksObject != null)
                    SolidworksObject.CurrentSelection = value;
            }
        }

        /// <summary>
        ///  Gets or sets the text in the combo box. 
        /// </summary>
        public string EditText
        {
            get
            {
                if (SolidworksObject != null)
                    return SolidworksObject.EditText;
                return "";
            }
            set
            {
                if (SolidworksObject != null)
                {
                    Style = (int)ComboBoxStyles.EditableText;
                    SolidworksObject.EditText = value;
                }
            }
        }

        /// <summary>
        /// Called when a user changes the selected item in a combo box on this PropertyManager page. 
        /// </summary>
        /// <remarks>solidworks will passs int the id of the selected item</remarks>
        public Action<int> OnSelectionChanged { get; set; }

        /// <summary>
        /// Called when a user changes the text string in the text box of a combo box on this PropertyManager page. solidworsk will pass in the text string
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method is only called if the combo box was set up as an editable text box. If the combo box is set up to as a static text box, then this method is not called.
        ///<para>
        /// If the user can edit the text in the text box, then use this method with <see cref="OnSelectionChanged"/> to find out what is in the text box of the combo box.
        ///</para>
        ///<para>
        ///When this method is called, the control may not yet be updated with the current selection, so the <see cref="CurrentSelection"/> property is not reliable. The text passed into this method is the up-to-date text.
        ///</para>
        /// </para></remarks>
        public Action<string> OnSelectionEdit { get; set; }
    }

    /// <summary>
    /// styles that can be used on a combobox
    /// </summary>
    public enum ComboBoxStyles
    {
        /// <summary>
        /// Sort the items in the attached drop-down list of the combo box in alphabetical order
        /// </summary>
        Sorted = 1,

        /// <summary>
        /// Allows editing of the text in the combo box
        /// </summary>
        EditableText = 2,

        /// <summary>
        /// User can only select a value from the attached drop-down list for the combo box
        /// </summary>
        /// <remarks>You can set EditBoxReadOnly either before or after the PropertyManager page is displayed. If set after the PropertyManager page is displayed and the box contains editable text, then that text cannot be edited by the user.<br/>
        /// However, you can use <see cref="PmpComboBox.EditText"/> to edit the text in the combo box.</remarks>
        EditBoxReadOnly = 4,

        /// <summary>
        /// The item the user selects in the attached drop-down list does not appear in the combo box. Instead, the user's selection causes the add-in to get a callback via <see cref="PmpComboBox.OnSelectionChanged"/>
        /// </summary>
        AvoidSelectionText = 8
    }
}
