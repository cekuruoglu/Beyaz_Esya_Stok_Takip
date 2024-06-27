using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    public class menustyle : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            // Mouse üzerine gelindiğindeki mavi arkaplanı kaldır
            if (!e.Item.Selected)
            {
                base.OnRenderMenuItemBackground(e);
            }
        }
    }

}
