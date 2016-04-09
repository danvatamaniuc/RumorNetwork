using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RumorNetwork.Logic;
using RumorNetwork.Validator;

namespace RumorNetwork
{
    public partial class MainWindow : Form
    {

        private Dictionary<int, float[]> members;
        private int _memberNr;

        public MainWindow()
        {
            members = new Dictionary<int, float[]>();
            _memberNr = 0;
            InitializeComponent();
        }

        private void cmdGenerateMembers_Click(object sender, EventArgs e)
        {
            try
            {
                _memberNr = int.Parse(txtMembersNumber.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Number of members could not been extracted!");
                return;
            }
            
            DrawMembers();
        }

        private void DrawMembers()
        {
            if (_memberNr.Equals(0))
            {
                return;
            }

            Pen memberPen = new Pen(Color.Black);
            Graphics formGraphics = CreateGraphics();

            members.Clear();

            GenerateRelationalGrid(_memberNr);

            //x and y coords of the circle of members
            int xCoord = 150;
            int yCoord = 190;

            int anglePace = 360/_memberNr;
            int radius = 100;

            int angle = 0;


            //draw all the members in a circle
            for (int i = 0; i < _memberNr; i++)
            {
                float pointX = xCoord + (float) (radius*Math.Sin(Math.PI*angle/180.0));
                float pointY = yCoord + (float) (radius*Math.Cos(Math.PI*angle/180.0));

                angle += anglePace;

                formGraphics.DrawEllipse(memberPen, pointX, pointY, 4, 4);
                members.Add(i, new[] {pointX, pointY});
            }

            memberPen.Dispose();
            formGraphics.Dispose();
        }

        /// <summary>
        /// Creates and populates the grid used to describe relations
        /// </summary>
        /// <param name="memberNr">Number of members</param>
        private void GenerateRelationalGrid(int memberNr)
        {
            dgvMembersRelation.Columns.Clear();

            for (int i = 0; i < memberNr; i++)
            {
                dgvMembersRelation.Columns.Add((i + 1).ToString(), (i + 1).ToString());
                dgvMembersRelation.Columns[i].Width = 20;
                dgvMembersRelation.Rows.Add();
            }

            foreach (DataGridViewRow row in dgvMembersRelation.Rows)
            {
                for (int i = 0; i < memberNr; i++)
                {
                    row.Cells[i].Value = 0;
                }
            }
        }

        /// <summary>
        /// Clears the drawing and the relational grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClear_Click(object sender, EventArgs e)
        {
            _memberNr = 0;
            Invalidate();
            dgvMembersRelation.Columns.Clear();
        }

        private void dgvMembersRelation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string newValue = dgvMembersRelation.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            //if the new value is not 0 and its compliment is 0, new connection added
            //otherwise just delete the connection
            if (
                !newValue.Equals("0")
                &&
                dgvMembersRelation.Rows[e.ColumnIndex].Cells[e.RowIndex].Value.ToString().Equals("0"))
            {
                DrawConnection(e.ColumnIndex, e.RowIndex, Color.Black);
                dgvMembersRelation.Rows[e.ColumnIndex].Cells[e.RowIndex].Value = newValue;
            }
            else if (newValue.Equals("0"))
            {
                dgvMembersRelation.Rows[e.ColumnIndex].Cells[e.RowIndex].Value = newValue;
            }
        }

        private void DrawConnection(int columnIndex, int rowIndex, Color connectionColor)
        {
            //initialize the drawing classes
            Pen connectionPen = new Pen(connectionColor);
            Graphics formGraphics = CreateGraphics();

            //get the points coords
            float[] memberACoords, memberBCoords;

            if (!members.TryGetValue(columnIndex, out memberACoords))
            {
                MessageBox.Show("Error while getting coords for point A!");
                return;
            }

            if (!members.TryGetValue(rowIndex, out memberBCoords))
            {
                MessageBox.Show("Error while getting coords for point B!");
                return;
            }

            //draw the line
            formGraphics.DrawLine(connectionPen, memberACoords[0], memberACoords[1], memberBCoords[0], memberBCoords[1]);

            //dispose of the drawing classes
            connectionPen.Dispose();
            formGraphics.Dispose();
        }

        private void cmdSolve_Click(object sender, EventArgs e)
        {
            List<int[]> relations = new List<int[]>();

            for (int i = 0; i < _memberNr; i++)
            {
                for (int j = i; j < _memberNr; j++)
                {
                    if (j != i && !dgvMembersRelation.Rows[i].Cells[j].Value.ToString().Equals("0"))
                    {
                        relations.Add(new int[] {i, j});
                    }
                }
            }

            if (relations.Count.Equals(0))
            {
                MessageBox.Show("No members and relations defined!");
                return;
            }

            List<int[]> newRelations = null;
              
            try
            {
                if (rbnEA.Checked)
                {
                    newRelations = Solver.EvolutiveSearch(relations, _memberNr);
                }
                else
                {
                    newRelations = Solver.ParticleSwarmOptimization(relations, _memberNr);
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            if (newRelations == null)
            {
                MessageBox.Show("A solution has not been found!");
                return;
            }

            DrawMembers();

            foreach (int[] relation in newRelations)
            {
                dgvMembersRelation.Rows[relation[0]].Cells[relation[1]].Value = "1";
                DrawConnection(relation[0], relation[1], Color.Aqua);
            }
        }
    }
}
