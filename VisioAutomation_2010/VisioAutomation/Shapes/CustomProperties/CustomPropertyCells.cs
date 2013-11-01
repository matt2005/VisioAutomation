﻿using VisioAutomation.CustomProperties;
using VisioAutomation.Shapes.CustomProperties;
using VA=VisioAutomation;
using VisioAutomation.Extensions;
using System.Collections.Generic;
using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioAutomation.Shapes.CustomProperties
{
    public class CustomPropertyCells : VA.ShapeSheet.CellGroups.CellGroupMultiRow
    {
        public VA.ShapeSheet.CellData<double> Value{ get; set; }
        public VA.ShapeSheet.CellData<double> Prompt { get; set; }
        public VA.ShapeSheet.CellData<double> Label { get; set; }
        public VA.ShapeSheet.CellData<double> Format { get; set; }
        public VA.ShapeSheet.CellData<int> SortKey { get; set; }
        public VA.ShapeSheet.CellData<int> Invisible { get; set; }
        public VA.ShapeSheet.CellData<int> Verify { get; set; }
        public VA.ShapeSheet.CellData<int> LangId { get; set; }
        public VA.ShapeSheet.CellData<int> Calendar { get; set; }
        public VA.ShapeSheet.CellData<int> Type { get; set; }

        public CustomPropertyCells()
        {
            
        }

        public CustomPropertyCells(string value)
        {
            this.Value = value;
            this.Type = 0;
        }

        public CustomPropertyCells(int value)
        {
            this.Value = value;
            this.Type = 2;
        }

        public CustomPropertyCells(double value)
        {
            this.Value = value;
            this.Type = 2;
        }

        public CustomPropertyCells(float value)
        {
            this.Value = value;
            this.Type = 2;
        }

        public CustomPropertyCells(bool value)
        {
            this.Value = ((bool)value) ? "TRUE" : "FALSE";
            this.Type = 3;
        }

        public CustomPropertyCells(System.DateTime value)
        {
            this.Value = string.Format("DATETIME({0},{1},{2})", value.Month, value.Date, value.Year);
            this.Type = 3;
        }

        public CustomPropertyCells(VA.ShapeSheet.FormulaLiteral value)
        {
            this.Value = value;
            this.Type = 2;
        }

        private string SmartStringToFormulaString(VA.ShapeSheet.FormulaLiteral formula, bool force_no_quoting)
        {
            if (!formula.HasValue)
            {
                return null;
            }

            if (formula.Value.Length == 0)
            {
                return VA.Convert.StringToFormulaString(formula.Value);
            }

            if (formula.Value[0] != '\"')
            {
                if (force_no_quoting)
                {
                    return formula.Value;
                }
                return VA.Convert.StringToFormulaString(formula.Value);
            }

            return formula.Value;
        }

        public override void ApplyFormulasForRow(ApplyFormula func, short row)
        {
            var cp = this;

            string str_label =  this.SmartStringToFormulaString(cp.Label.Formula, false);
            string str_value = null;
            if (cp.Type.Formula.Value == "0" || cp.Type.Formula.Value == null)
            {
                // if type has no value or is a "0" then it is a string
                str_value = this.SmartStringToFormulaString(cp.Value.Formula, false);
            }
            else
            {
               // For non-stringd don't add any extra quotes
                str_value = this.SmartStringToFormulaString(cp.Value.Formula, true);                
            }
            string str_format = this.SmartStringToFormulaString(cp.Format.Formula, false);
            string str_prompt = this.SmartStringToFormulaString(cp.Prompt.Formula, false);

            func(VA.ShapeSheet.SRCConstants.Prop_Label.ForRow(row), str_label);
            func(VA.ShapeSheet.SRCConstants.Prop_Value.ForRow( row), str_value);
            func(VA.ShapeSheet.SRCConstants.Prop_Format.ForRow( row), str_format);
            func(VA.ShapeSheet.SRCConstants.Prop_Prompt.ForRow( row), str_prompt);
            func(VA.ShapeSheet.SRCConstants.Prop_Calendar.ForRow( row), cp.Calendar.Formula);
            func(VA.ShapeSheet.SRCConstants.Prop_LangID.ForRow( row), cp.LangId.Formula);
            func(VA.ShapeSheet.SRCConstants.Prop_SortKey.ForRow( row), cp.SortKey.Formula);
            func(VA.ShapeSheet.SRCConstants.Prop_Invisible.ForRow( row), cp.Invisible.Formula);
            func(VA.ShapeSheet.SRCConstants.Prop_Type.ForRow( row), cp.Type.Formula);
        }

        public static IList<List<CustomPropertyCells>> GetCells(IVisio.Page page, IList<int> shapeids)
        {
            var query = get_query();
            return _GetCells(page, shapeids, query, query.GetCells);
        }

        public static IList<CustomPropertyCells> GetCells(IVisio.Shape shape)
        {
            var query = get_query();
            return _GetCells(shape, query, query.GetCells);
        }

        private static CustomPropertyCellQuery _mCellQuery;
        private static CustomPropertyCellQuery get_query()
        {
            _mCellQuery = _mCellQuery ?? new CustomPropertyCellQuery();
            return _mCellQuery;
        }


        public static CustomPropertyCells FromValue(object value)
        {
            if (value is string)
            {
                return new CustomPropertyCells((string)value);
            }
            else if (value is int)
            {
                return new CustomPropertyCells((int)value);
            }
            else if (value is double)
            {
                return new CustomPropertyCells((double)value);
            }
            else if (value is float)
            {
                return new CustomPropertyCells((float)value);
            }
            else if (value is bool)
            {
                return new CustomPropertyCells((bool)value);
            }
            else if (value is System.DateTime)
            {
                return new CustomPropertyCells((System.DateTime)value);
            }
            else
            {
                string msg = string.Format("Unsupported type for value \"{0}\" \"{1}\"", value, value.GetType());
                throw new System.ArgumentOutOfRangeException(msg);
            }
        }
    }
}

namespace VisioAutomation.CustomProperties
{
    class CustomPropertyCellQuery : VA.ShapeSheet.Query.CellQuery
    {
        public VA.ShapeSheet.Query.CellQuery.Column SortKey { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Ask { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Calendar { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Format { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Invis { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Label { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column LangID { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Prompt { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Value { get; set; }
        public VA.ShapeSheet.Query.CellQuery.Column Type { get; set; }

        public CustomPropertyCellQuery() 
        {
            var sec = this.Sections.Add(IVisio.VisSectionIndices.visSectionProp);

            SortKey = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_SortKey, "SortKey");
            Ask = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Ask, "Ask");
            Calendar = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Calendar, "Calendar");
            Format = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Format, "Format");
            Invis = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Invisible, "Invis");
            Label = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Label, "Label");
            LangID = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_LangID, "LangID");
            Prompt = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Prompt, "Prompt");
            Type = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Type, "Type");
            Value = sec.Columns.Add(VA.ShapeSheet.SRCConstants.Prop_Value, "Value");
        }

        public CustomPropertyCells GetCells(VA.ShapeSheet.CellData<double>[] row)
        {
            var cells = new CustomPropertyCells();
            cells.Value = row[Value.Ordinal];
            cells.Calendar = row[Calendar.Ordinal].ToInt();
            cells.Format = row[Format.Ordinal];
            cells.Invisible = row[Invis.Ordinal].ToInt();
            cells.Label = row[Label.Ordinal];
            cells.LangId = row[LangID.Ordinal].ToInt();
            cells.Prompt = row[Prompt.Ordinal];
            cells.SortKey = row[SortKey.Ordinal].ToInt();
            cells.Type = row[Type.Ordinal].ToInt();
            return cells;
        }
    }

}