using VA=VisioAutomation;
using System.Collections.Generic;
using IVisio = Microsoft.Office.Interop.Visio;
using System.Linq;

namespace VisioAutomation.Connections
{
    public static class ConnectionPointHelper
    {
        public static int AddConnectionPoint(
            IVisio.Shape shape,
            Connections.ConnectionPointCells cp)
        {
            if (shape == null)
            {
                throw new System.ArgumentNullException("shape");
            }

            if (!cp.X.Formula.HasValue)
            {
                throw new System.ArgumentException("Must provide an X Formula");
            }

            if (!cp.Y.Formula.HasValue)
            {
                throw new System.ArgumentException("Must provide an Y Formula");
            }

            var n = shape.AddRow((short)IVisio.VisSectionIndices.visSectionConnectionPts,
                                 (short)IVisio.VisRowIndices.visRowLast,
                                 (short)IVisio.VisRowTags.visTagCnnctPt);

            var update = new VA.ShapeSheet.Update.SRCUpdate();
            cp.Apply(update,n);
            update.Execute(shape);

            return n;
        }


        public static void DeleteConnectionPoint(IVisio.Shape shape, int index)
        {
            if (shape == null)
            {
                throw new System.ArgumentNullException("shape");
            }

            if (index < 0)
            {
                throw new System.ArgumentOutOfRangeException("index");
            }

            var row = (IVisio.VisRowIndices)index;
            shape.DeleteRow( (short) IVisio.VisSectionIndices.visSectionConnectionPts, (short)row);
        }

        public static int GetConnectionPointCount(IVisio.Shape shape)
        {
            if (shape == null)
            {
                throw new System.ArgumentNullException("shape");
            }

            return shape.RowCount[ (short) IVisio.VisSectionIndices.visSectionConnectionPts];
        }

        public static int DeleteAllConnectionPoints(IVisio.Shape shape)
        {
            if (shape == null)
            {
                throw new System.ArgumentNullException("shape");
            }

            int n = GetConnectionPointCount(shape);
            for (int i = n - 1; i >= 0; i--)
            {
                DeleteConnectionPoint(shape, i);
            }

            return n;
        }

        [System.Obsolete]
        public static IList<List<ConnectionPointCells>> GetCells(IVisio.Page page, IList<int> shapeids)
        {
            return ConnectionPointCells.GetCells(page, shapeids);
        }

        [System.Obsolete]
        public static IList<ConnectionPointCells> GetCells(IVisio.Shape shape)
        {
            return ConnectionPointCells.GetCells(shape);
        }

        public static IList<List<ConnectionPointCells>> GetConnectionPoints(IVisio.Page page, IList<int> shapeids)
        {
            return ConnectionPointCells.GetCells(page, shapeids);
        }

        public static IList<ConnectionPointCells> GetConnectionPoints(IVisio.Shape shape)
        {
            return ConnectionPointCells.GetCells(shape);
        }
    }
}