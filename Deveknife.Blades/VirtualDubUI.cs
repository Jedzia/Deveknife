using System;
using System.Windows.Forms;

namespace Deveknife.Blades
{
    public partial class VirtualDubUI : UserControl
    {
        public VirtualDubUI()
        {
            InitializeComponent();
        }

      

        private void Panel1GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            MessageBox.Show("pictureBox1_DragEnter");
            // Use custom cursors if the check box is checked.

            //if (UseCustomCursorsCheck.Checked)
            {

                // Sets the custom cursor based upon the effect.

                e.UseDefaultCursors = false;
                if ((e.Effect & DragDropEffects.Move) == DragDropEffects.Move)
                    Cursor.Current = MyNormalCursor;
                else
                    Cursor.Current = MyNoDropCursor;
            }
  

        }

        protected Cursor MyNoDropCursor
        {
            get
            {
                return Cursors.NoMove2D;
            }
        }

        protected Cursor MyNormalCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Button pressed");
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {

            //MessageBox.Show("panel1_DragEnter");
        }

        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            //var formats = e.Data.GetFormats();
            if (e.Data.GetDataPresent("FileNameW"))
            {
                var data = e.Data.GetData("FileNameW");
                var strings = data as string[];
                if (strings != null && strings.Length > 0)
                {
                    e.Effect = DragDropEffects.Link;
                    this.dropLocationLabel.Text = strings[0];
                }
                return;
            }

            //EffectbyKeystate(e);
        }

        private void EffectbyKeystate(DragEventArgs e)
        {
            // Determine whether string data exists in the drop data. If not, then
            // the drop effect reflects that the drop cannot occur.

            if (!e.Data.GetDataPresent(typeof(System.String)))
            {

                e.Effect = DragDropEffects.None;
                this.dropLocationLabel.Text = "None - no string data.";
                return;
            }

            // Set the effect based upon the KeyState.

            if ((e.KeyState & (8 + 32)) == (8 + 32) && (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                // KeyState 8 + 32 = CTL + ALT

                // Link drag-and-drop effect.

                e.Effect = DragDropEffects.Link;
            }
            else if ((e.KeyState & 32) == 32 && (e.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                // ALT KeyState for link.

                e.Effect = DragDropEffects.Link;
            }
            else if ((e.KeyState & 4) == 4 && (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                // SHIFT KeyState for move.

                e.Effect = DragDropEffects.Move;
            }
            else if ((e.KeyState & 8) == 8 && (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                // CTL KeyState for copy.

                e.Effect = DragDropEffects.Copy;
            }
            else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                // By default, the drop action should be move, if allowed.

                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

            // Get the index of the item the mouse is below. 

            // The mouse locations are relative to the screen, so they must be 

            // converted to client coordinates.

            /* indexOfItemUnderMouseToDrop =
                ListDragTarget.IndexFromPoint(ListDragTarget.PointToClient(new Point(e.X, e.Y)));

            // Updates the label text.

            if (indexOfItemUnderMouseToDrop != ListBox.NoMatches)
            {

                dropLocationLabel.Text = "Drops before item #" + (indexOfItemUnderMouseToDrop + 1);
            }
            else
                dropLocationLabel.Text = "Drops at the end.";*/
        }
    }
}
