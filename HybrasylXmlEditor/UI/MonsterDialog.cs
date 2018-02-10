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

using Hybrasyl.Creatures;
using Hybrasyl.XML;
using HybrasylXmlEditor.UI.ViewModel;
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
    public partial class MonsterDialog : Form
    {
        public MonsterViewModel MonsterVM { get; set; }

        public MonsterDialog()
        {
            InitializeComponent();
        }

        public void MonsterDialogForm_Load(object sender, EventArgs e)
        {
            MonsterVM = new MonsterViewModel(new Creature());
            setBindings();
        }

        private void setBindings()
        {
            textBoxName.DataBindings.Add("Text", MonsterVM, "Name");
            textBoxDescription.DataBindings.Add("Text", MonsterVM, "Description");
            numericSprite.DataBindings.Add("Value", MonsterVM, "Sprite");
        }

        private void buttonNewXml_Click(object sender, EventArgs e)
        {
            MonsterVM.SetDisplayMonster(new Creature());
        }

        private void buttonSaveXml_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveMonsterXML = new SaveFileDialog();
            saveMonsterXML.Filter = "(XML)|*.xml";
            if (saveMonsterXML.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileName = saveMonsterXML.FileName;
                    XmlWriterSettings xmlSettings = new XmlWriterSettings();
                    xmlSettings.Indent = true;
                    xmlSettings.IndentChars = "\t";

                    XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlSettings);
                    var monster = MonsterVM.GetDisplayMonster();
                    Serializer.Serialize(xmlWriter, MonsterVM.GetDisplayMonster());

                    xmlWriter.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file");
                }

            }
        }

        private void buttonLoadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadMonsterXML = new OpenFileDialog();
            loadMonsterXML.Filter = "(XML)|*.xml";
            if (loadMonsterXML.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;

                    XmlReader reader = XmlReader.Create(loadMonsterXML.FileName, settings);
                    Creature nullMonster = null;
                    var readMonster = Serializer.Deserialize(reader, nullMonster);
                    MonsterVM.SetDisplayMonster(readMonster);

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Problem with loading the file");
                }
            }
        }
    }
}