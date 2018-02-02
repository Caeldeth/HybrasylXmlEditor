﻿/*
 * This file is part of Project Hybrasyl.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the Affero General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
 * for more details.
 *
 * You should have received a copy of the Affero General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * (C) 2016 Project Hybrasyl (info@hybrasyl.com)
 *
 */

using Hybrasyl.Statuses;
using Hybrasyl.XML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HybrasylXmlEditor.UI
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            AddTabForms();
        }

        private void AddTabForms()
        {
            //Status Tab
            StatusesDialog stdlg = new StatusesDialog();
            stdlg.TopLevel = false;
            stdlg.Visible = true;
            stdlg.FormBorderStyle = FormBorderStyle.None;
            stdlg.Dock = DockStyle.Fill;
            tabControl1.TabPages[0].Controls.Add(stdlg);

            //Creatures Tab

        }
    }
}
