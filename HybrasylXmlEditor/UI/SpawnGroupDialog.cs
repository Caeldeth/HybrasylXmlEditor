﻿using Hybrasyl.Creatures;
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
    public partial class SpawnGroupDialog : Form
    {
        private SpawnGroupViewModel SpawnGroupVM;

        public SpawnGroupDialog()
        {
            InitializeComponent();
        }

        private void SpawnGroupDialog_Load(object sender, EventArgs e)
        {
            SpawnGroupVM = new SpawnGroupViewModel(new SpawnGroup());
            SpawnGroupVM.SetDisplaySpawnGroup(new SpawnGroup());
            setBindings();
            setupGridView();
        }

        private void setBindings()
        {
            dataGridViewMaps.AutoGenerateColumns = false;
            dataGridViewMaps.DataSource = SpawnGroupVM.Maps;

            dataGridViewSpawns.AutoGenerateColumns = false;
            dataGridViewSpawns.AllowUserToAddRows = false;
            dataGridViewSpawns.DataSource = SpawnGroupVM.Spawn;
        }

        private void setupGridView()
        {
            #region Map
            dataGridViewMaps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMaps.MultiSelect = false;
            dataGridViewMaps.RowHeadersVisible = false;
            dataGridViewMaps.AllowUserToOrderColumns = false;
            dataGridViewMaps.AllowUserToResizeColumns = false;
            dataGridViewMaps.AllowUserToResizeRows = false;
            dataGridViewMaps.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            DataGridViewTextBoxColumn mapName = new DataGridViewTextBoxColumn();
            mapName.Name = "Name";
            mapName.DataPropertyName = "Name";
            mapName.HeaderText = "Name";
            mapName.Width = 209;
            mapName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapName.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaps.Columns.Add(mapName);

            DataGridViewTextBoxColumn mapMinSpawn = new DataGridViewTextBoxColumn();
            mapMinSpawn.Name = "MinSpawn";
            mapMinSpawn.DataPropertyName = "MinSpawn";
            mapMinSpawn.HeaderText = "MinSpawn";
            mapMinSpawn.Width = 75;
            mapMinSpawn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapMinSpawn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapMinSpawn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaps.Columns.Add(mapMinSpawn);

            DataGridViewTextBoxColumn mapMaxSpawn = new DataGridViewTextBoxColumn();
            mapMaxSpawn.Name = "MaxSpawn";
            mapMaxSpawn.DataPropertyName = "MaxSpawn";
            mapMaxSpawn.HeaderText = "MaxSpawn";
            mapMaxSpawn.Width = 75;
            mapMaxSpawn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapMaxSpawn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapMaxSpawn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaps.Columns.Add(mapMaxSpawn);

            DataGridViewTextBoxColumn mapLimit = new DataGridViewTextBoxColumn();
            mapLimit.Name = "Limit";
            mapLimit.DataPropertyName = "Limit";
            mapLimit.HeaderText = "Limit";
            mapLimit.Width = 75;
            mapLimit.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapLimit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapLimit.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaps.Columns.Add(mapLimit);

            DataGridViewTextBoxColumn mapInterval = new DataGridViewTextBoxColumn();
            mapInterval.Name = "Interval";
            mapInterval.DataPropertyName = "Interval";
            mapInterval.HeaderText = "Interval";
            mapInterval.Width = 75;
            mapInterval.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapInterval.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            mapInterval.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewMaps.Columns.Add(mapInterval);
            #endregion

            #region Spawn
            dataGridViewSpawns.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSpawns.MultiSelect = false;
            dataGridViewSpawns.RowHeadersVisible = false;
            dataGridViewSpawns.AllowUserToOrderColumns = false;
            dataGridViewSpawns.AllowUserToResizeColumns = false;
            dataGridViewSpawns.AllowUserToResizeRows = false;
            dataGridViewSpawns.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            DataGridViewTextBoxColumn spawnBase = new DataGridViewTextBoxColumn();
            spawnBase.Name = "Base";
            spawnBase.DataPropertyName = "Base";
            spawnBase.HeaderText = "Base";
            spawnBase.Width = 268;
            spawnBase.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            spawnBase.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            spawnBase.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewSpawns.Columns.Add(spawnBase);

            DataGridViewTextBoxColumn spawnVariance = new DataGridViewTextBoxColumn();
            spawnVariance.Name = "Variance";
            spawnVariance.DataPropertyName = "Variance";
            spawnVariance.HeaderText = "Variance";
            spawnVariance.Width = 75;
            spawnVariance.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            spawnVariance.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            spawnVariance.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewSpawns.Columns.Add(spawnVariance);
            #endregion
        }

        private void buttonNewXml_Click(object sender, EventArgs e)
        {
            SpawnGroupVM.SetDisplaySpawnGroup(new SpawnGroup());
            dataGridViewMaps.DataSource = SpawnGroupVM.Maps;
            dataGridViewSpawns.DataSource = SpawnGroupVM.Spawn;
        }

        private void buttonSaveXml_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveSpawnGroupXML = new SaveFileDialog();
            saveSpawnGroupXML.Filter = "(XML)|*.xml";
            if (saveSpawnGroupXML.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var fileName = saveSpawnGroupXML.FileName;
                    XmlWriterSettings xmlSettings = new XmlWriterSettings();
                    xmlSettings.Indent = true;
                    xmlSettings.IndentChars = "\t";

                    XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlSettings);
                    var test = SpawnGroupVM;
                    Serializer.Serialize(xmlWriter, SpawnGroupVM.GetDisplaySpawnGroup());

                    xmlWriter.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Problem saving file");
                }

            }
        }

        private void buttonLoadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadSpawnGroupXML = new OpenFileDialog();
            loadSpawnGroupXML.Filter = "(XML)|*.xml";
            if (loadSpawnGroupXML.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;

                    XmlReader reader = XmlReader.Create(loadSpawnGroupXML.FileName, settings);
                    SpawnGroup nullSpawnGroup = null;
                    var readSpawnGroup = Serializer.Deserialize(reader, nullSpawnGroup);
                    SpawnGroupVM.SetDisplaySpawnGroup(readSpawnGroup);
                    dataGridViewMaps.DataSource = SpawnGroupVM.Maps;
                    dataGridViewSpawns.DataSource = SpawnGroupVM.Spawn;

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
