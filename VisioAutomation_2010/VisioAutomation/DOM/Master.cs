﻿using System.Collections.Generic;
using VA = VisioAutomation;
using IVisio = Microsoft.Office.Interop.Visio;
using System.Linq;

namespace VisioAutomation.DOM
{
    public class Master : ShapeFromMaster
    {
        public VA.Drawing.Point DropPosition { get; private set; }
        public VA.Drawing.Size? DroppedSize;

        public Master(IVisio.Master master, VA.Drawing.Point dropposition) :
            base(master)
        {
            this.DropPosition = dropposition;
        }


        public Master(IVisio.Master master, VA.Drawing.Rectangle rect) :
            base(master)
        {
            this.DropPosition = rect.Center;
            this.DroppedSize = rect.Size;
        }

        public Master(string master, string stencil, VA.Drawing.Point dropposition) :
            base(master, stencil)
        {
            this.DropPosition = dropposition;
        }

        public Master(string master, string stencil, VA.Drawing.Rectangle rect) :
            base(master, stencil)
        {
            this.DropPosition = rect.Center;
            this.DroppedSize = rect.Size;
            this.ShapeCells.Width = rect.Size.Width;
            this.ShapeCells.Height = rect.Size.Height;
        }

        public Master(IVisio.Master master, double x, double y) :
            this(master, new VA.Drawing.Point(x, y))
        {
        }
    }
}