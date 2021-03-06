using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioAutomation.Shapes.Geometry
{
    public class GeometryRow
    {
        public ShapeSheet.FormulaLiteral X { get; set; }
        public ShapeSheet.FormulaLiteral Y { get; set; }
        public ShapeSheet.FormulaLiteral A { get; set; }
        public ShapeSheet.FormulaLiteral B { get; set; }
        public ShapeSheet.FormulaLiteral C { get; set; }
        public ShapeSheet.FormulaLiteral D { get; set; }
        public ShapeSheet.FormulaLiteral E { get; set; }
        public IVisio.VisRowTags RowTag { get; set; }

        public GeometryRow(IVisio.VisRowTags tag)
        {
            this.RowTag = tag;
        }

        public virtual IVisio.VisRowTags GetRowTagType()
        {
            return this.RowTag;
        }

        public void AddTo(IVisio.Shape shape, ShapeSheet.Update update, short row, short section)
        {
            short row_index = shape.AddRow(section, row, (short) this.GetRowTagType());
            this.Update(section, row_index, update);
        }


        private void Update(short section, short row_index, ShapeSheet.Update update)
        {
            var x_src = ShapeSheet.SRCConstants.Geometry_X.ForSectionAndRow(section, row_index);
            var y_src = ShapeSheet.SRCConstants.Geometry_Y.ForSectionAndRow(section, row_index);
            var a_src = ShapeSheet.SRCConstants.Geometry_A.ForSectionAndRow(section, row_index);
            var b_src = ShapeSheet.SRCConstants.Geometry_B.ForSectionAndRow(section, row_index);
            var c_src = ShapeSheet.SRCConstants.Geometry_C.ForSectionAndRow(section, row_index);
            var d_src = ShapeSheet.SRCConstants.Geometry_D.ForSectionAndRow(section, row_index);
            var e_src = ShapeSheet.SRCConstants.Geometry_E.ForSectionAndRow(section, row_index);

            update.SetFormulaIgnoreNull(x_src, this.X);
            update.SetFormulaIgnoreNull(y_src, this.Y);
            update.SetFormulaIgnoreNull(a_src, this.A);
            update.SetFormulaIgnoreNull(b_src, this.B);
            update.SetFormulaIgnoreNull(c_src, this.C);
            update.SetFormulaIgnoreNull(d_src, this.D);
            update.SetFormulaIgnoreNull(e_src, this.E);
        }

        public static GeometryRow CreateLineTo(ShapeSheet.FormulaLiteral x, ShapeSheet.FormulaLiteral y)
        {
            // http://msdn.microsoft.com/en-us/library/aa195656(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagLineTo);
            row.X = x;
            row.Y = y;
            return row;
        }

        public static GeometryRow CreateMoveTo(ShapeSheet.FormulaLiteral x, ShapeSheet.FormulaLiteral y)
        {
            // http://msdn.microsoft.com/en-us/library/aa195679(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagMoveTo);
            row.X = x;
            row.Y = y;
            return row;
        }

        public static GeometryRow CreateArcTo(ShapeSheet.FormulaLiteral x, ShapeSheet.FormulaLiteral y,
                                              ShapeSheet.FormulaLiteral a)
        {
            // http://msdn.microsoft.com/en-us/library/aa195698(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagArcTo);
            row.X = x;
            row.Y = y;
            row.A = a;
            return row;
        }

        public static GeometryRow CreateEllipticalArcTo(ShapeSheet.FormulaLiteral x,
                                                        ShapeSheet.FormulaLiteral y,
                                                        ShapeSheet.FormulaLiteral a,
                                                        ShapeSheet.FormulaLiteral b,
                                                        ShapeSheet.FormulaLiteral c,
                                                        ShapeSheet.FormulaLiteral d)
        {
            // http://msdn.microsoft.com/en-us/library/aa195660(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagEllipticalArcTo);
            row.X = x;
            row.Y = y;
            row.A = a;
            row.B = b;
            row.C = c;
            row.D = d;
            return row;
        }

        public static GeometryRow CreateEllipse(ShapeSheet.FormulaLiteral x,
                                                ShapeSheet.FormulaLiteral y,
                                                ShapeSheet.FormulaLiteral a,
                                                ShapeSheet.FormulaLiteral b,
                                                ShapeSheet.FormulaLiteral c,
                                                ShapeSheet.FormulaLiteral d)
        {

            // http://msdn.microsoft.com/en-us/library/aa195692(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagEllipse);
            row.X = x;
            row.Y = y;
            row.A = a;
            row.B = b;
            row.C = c;
            row.D = d;
            return row;
        }

        public static GeometryRow CreateNURBSTo(ShapeSheet.FormulaLiteral x,
                                                ShapeSheet.FormulaLiteral y,
                                                ShapeSheet.FormulaLiteral a,
                                                ShapeSheet.FormulaLiteral b,
                                                ShapeSheet.FormulaLiteral c,
                                                ShapeSheet.FormulaLiteral d,
                                                ShapeSheet.FormulaLiteral e)
        {
            // http://msdn.microsoft.com/en-us/library/aa195685(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagEllipse);
            row.X = x;
            row.Y = y;
            row.A = a;
            row.B = b;
            row.C = c;
            row.D = d;
            row.E = e;
            return row;
        }

        public static GeometryRow CreatePolylineTo(ShapeSheet.FormulaLiteral x,
                                        ShapeSheet.FormulaLiteral y,
                                        ShapeSheet.FormulaLiteral a)
        {
            // http://msdn.microsoft.com/en-us/library/aa195682(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagPolylineTo);
            row.X = x;
            row.Y = y;
            row.A = a;
            return row;
        }

        public static GeometryRow CreateInfiniteLine(ShapeSheet.FormulaLiteral x,
                                ShapeSheet.FormulaLiteral y,
                                ShapeSheet.FormulaLiteral a,
                                ShapeSheet.FormulaLiteral b)
        {
            // http://msdn.microsoft.com/en-us/library/aa195682(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagInfiniteLine);
            row.X = x;
            row.Y = y;
            row.A = a;
            row.B = b;
            return row;
        }

        public static GeometryRow CreateSplineKnot(ShapeSheet.FormulaLiteral x,
                                ShapeSheet.FormulaLiteral y,
                                ShapeSheet.FormulaLiteral a)
        {
            // http://msdn.microsoft.com/en-us/library/aa195667(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagSplineSpan);
            row.X = x;
            row.Y = y;
            row.A = a;
            return row;
        }

        public static GeometryRow CreateSplineStart(ShapeSheet.FormulaLiteral x,
                                                ShapeSheet.FormulaLiteral y,
                                                ShapeSheet.FormulaLiteral a,
                                                ShapeSheet.FormulaLiteral b,
                                                ShapeSheet.FormulaLiteral c,
                                                ShapeSheet.FormulaLiteral d)
        {

            // http://msdn.microsoft.com/en-us/library/aa195663(v=office.11).aspx

            var row = new GeometryRow(IVisio.VisRowTags.visTagSplineBeg);
            row.X = x;
            row.Y = y;
            row.A = a;
            row.B = b;
            row.C = c;
            row.D = d;
            return row;
        }
    }
}