using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GRemote
{
    public class UI
    {
        public static ToolStripMenuItem UpdateMenuCheckGroup(ToolStripMenuItem parent, String selectedItem)
        {
            ToolStripMenuItem selected = null;
            ToolStripMenuItem menuItem;

            foreach (ToolStripItem item in parent.DropDownItems)
            {
                if (!(item is ToolStripMenuItem))
                {
                    continue;
                }

                menuItem = item as ToolStripMenuItem;
                menuItem.Checked = selectedItem.Equals(item.Tag);

                if (menuItem.Checked)
                {
                    selected = menuItem;
                }
            }

            return selected;
        }
    }
}
