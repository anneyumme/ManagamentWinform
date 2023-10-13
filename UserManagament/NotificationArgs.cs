using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagament
{

    internal class NotificationArgs
    {
        public static XtraMessageBoxArgs Args(SvgImageCollection svgImageCollection1, int index, string content,
            string caption)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs()
            {
                Caption = caption,
                Text = content,
                ImageOptions = new MessageBoxImageOptions()
                {
                    SvgImage = svgImageCollection1[index],
                    SvgImageSize = new Size(24, 24),
                }
            };
            return args;
        }

    }
}
