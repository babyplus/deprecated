using System.Collections.Generic;
using MyModels;
using Microsoft.VisualBasic.FileIO;

namespace MyTools 
{
    public class CsvTool
    {
        static public string Print(String t)
        {
            using (TextFieldParser parser = new TextFieldParser(t))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[]? fields = parser.ReadFields();
                    if (fields is not null) foreach (string field in fields)
                        System.Console.WriteLine(field);
                }
            }
            return "";
        }

        static public void Exit(int code, String tips)
        {
            SysTool.Exit(code, tips);
        }

        static public void RecordResource<T>(String id, T t, Dictionary<String, T> ts)
        {
            ts.Add(id, t); 
        }

        static public void RecordDiagram(String id, CsvPageEx description, Dictionary<String, Dictionary<String, MxCell>> pages)
        {
            if (! pages.ContainsKey(id)) pages.Add(id, new Dictionary<String, MxCell>());
            pages[id].Add("parent", new MxCell(){id = "0"});
        }

        static public void RecordMxCell(String id, CsvLayerEx description, Dictionary<String, Dictionary<String, MxCell>> pages)
        {
            String pageId = description.PageId;
            if (! pages.ContainsKey(pageId)) Exit(201, $"No corresponding page({pageId})."); 
            if (! pages[pageId].ContainsKey(id)) pages[pageId].Add(id, new MxCell(){parent = "0",
                                                                                    id = id,
                                                                                    @value = description.Topic});
        }

        static public MxGeometry GenerateGeo(CsvArrangementEx arrangement)
        {
            MxGeometry geo = new MxGeometry(){x = arrangement.X,
                                              y = arrangement.Y,
                                              width = arrangement.Width,
                                              height = arrangement.Height,
                                              @as = "geometry"};
            int x,y,w,h,x_offset,y_offset,w_offset,h_offset = 0;
            if (int.TryParse(arrangement.X, out x)
                && int.TryParse(arrangement.Y, out y)
                && int.TryParse(arrangement.Width, out w)
                && int.TryParse(arrangement.Height, out h)
                && int.TryParse(arrangement.X_offset, out x_offset)
                && int.TryParse(arrangement.Y_offset, out y_offset)
                && int.TryParse(arrangement.W_offset, out w_offset)
                && int.TryParse(arrangement.H_offset, out h_offset))
            {
                arrangement.X=$"{x+x_offset}";
                arrangement.Y=$"{y+y_offset}";
                arrangement.Width=$"{w+w_offset}";
                arrangement.Height=$"{h+h_offset}";
            }
            else
                Exit(204, "Arrangement parameter error.");
            return geo; 
        }

        static public String GenerateStyle(Dictionary<ResourceType, dynamic> resources, String style, String theme, String supplementalStyle)
        {
            Dictionary<String, CsvThemeEx> themes = resources[ResourceType.Themes];
            Dictionary<String, CsvStyleEx> styles = resources[ResourceType.Styles];
            if (! styles.ContainsKey(style)) return style;
            if ("" == styles[style].ThemeIdx) return styles[style].Context;
            String themeIdx = ("" == theme) ? styles[style].ThemeIdx : theme;
            if (! themes.ContainsKey(themeIdx)) Exit(205, $"No corresponding theme({themeIdx})");
            if ( "" == themes[themeIdx].FillColor || "" == themes[themeIdx].StrokeColor || true == themes[themeIdx].Auto ) themes[themeIdx].Shake();
            themes.Add((String)resources[ResourceType.Id], new () {
                                                                    FillColor=themes[themeIdx].FillColor,
                                                                    StrokeColor=themes[themeIdx].StrokeColor,
                                                                    FontColor=themes[themeIdx].FontColor
                                                                    });
            string context = styles[style].Context;
            context = context.Replace("${fillColor}", themes[themeIdx].FillColor)
                             .Replace("${strokeColor}", themes[themeIdx].StrokeColor)
                             .Replace("${fontColor}", themes[themeIdx].FontColor) + supplementalStyle;
            return context; 
        }

        static public void RecordMxCell(String id, CsvEntityEx description, Dictionary<String, Dictionary<String, MxCell>> pages, Dictionary<ResourceType, dynamic> resources)
        {
            Dictionary<String, CsvArrangementEx> arrangements = resources[ResourceType.Arrangements];
            Dictionary<String, CsvTextEx> texts = resources[ResourceType.Texts];
            String pageId = description.PageId;
            if (! pages.ContainsKey(pageId)) Exit(201, $"No corresponding page({pageId})."); 
            String layerId = description.LayerId;
            if (! pages[pageId].ContainsKey(layerId)) Exit(202, $"No corresponding layer({pageId}.{layerId}).");
            String arrangement = description.Arrangement;
            if (! arrangements.ContainsKey(arrangement)) Exit(203, $"No corresponding arrangement({pageId}.{arrangement}).");
            String theme = description.Theme;
            String supplementalStyle = description.SupplementalStyle;
            MxGeometry geo = GenerateGeo(arrangements[arrangement]);
            String style = GenerateStyle(resources, description.Style, theme, supplementalStyle);
            if (! pages[pageId].ContainsKey(id)) pages[pageId].Add(id, new MxCell(){parent = layerId,
                                                                                    id = id,
                                                                                    @value = (texts.ContainsKey(description.Description)) ? 
                                                                                                    texts[description.Description].Text :  description.Description,
                                                                                    style = style,
                                                                                    mxGeometry = geo,
                                                                                    vertex = "1"});
        }

        static public void RecordMxCell(String id, CsvJointEx description, Dictionary<String, Dictionary<String, MxCell>> pages, Dictionary<ResourceType, dynamic> resources)
        {
            String pageId = description.PageId;
            if (! pages.ContainsKey(pageId)) Exit(201, $"No corresponding page({pageId})."); 
            String layerId = description.LayerId;
            if (! pages[pageId].ContainsKey(layerId)) Exit(202, $"No corresponding layer({pageId}.{layerId}).");
            String theme = description.Theme;
            String supplementalStyle = description.SupplementalStyle;
            String style = GenerateStyle(resources, description.Style, theme, supplementalStyle);
            if (! pages[pageId].ContainsKey(description.Source)) Exit(202, $"No corresponding source({pageId}.{description.Source}).");
            if (! pages[pageId].ContainsKey(description.Target)) Exit(202, $"No corresponding target({pageId}.{description.Target}).");
            if (! pages[pageId].ContainsKey(id)) pages[pageId].Add(id, new MxCell(){parent = layerId,
                                                                                    id = id,
                                                                                    @value = "",
                                                                                    style = style,
                                                                                    source = description.Source,
                                                                                    target = description.Target,
                                                                                    edge = "1",
                                                                                    mxGeometry = new MxGeometry(){@as = "geometry",
                                                                                                                  relative = "1",
                                                                                                                  mxPoint = new [] {new MxPoint(){@as="sourcePoint"},
                                                                                                                                    new MxPoint(){@as="targetPoint"}}
                                                                                                                }
                                                                                    });
        }

        static public void Convert(Dictionary<String, Dictionary<String, MxCell>> pages, Mxfile mxfile)
        {
            int pagesCnt = pages.Count;
            mxfile.diagrams = new Diagram[pagesCnt];
            int pageIdx = 0;
            foreach (var (pageId, pageInfo) in pages)
            {
                int mxCellsCnt = pageInfo.Count;
                int mxCellIdx = 0;
                mxfile.diagrams[pageIdx] = new Diagram(){name="test"};
                mxfile.diagrams[pageIdx].mxGraphModels = new MxGraphModel[1];
                mxfile.diagrams[pageIdx].mxGraphModels![0] = new MxGraphModel();
                mxfile.diagrams[pageIdx].mxGraphModels![0].roots = new Root[1];
                mxfile.diagrams[pageIdx].mxGraphModels![0].roots![0] = new Root();
                mxfile.diagrams[pageIdx].mxGraphModels![0].roots![0].mxcells = new MxCell[mxCellsCnt];
                foreach (var (mxCellId,mxCell) in pageInfo)
                {
                    mxfile.diagrams[pageIdx].mxGraphModels![0].roots![0].mxcells![mxCellIdx] = mxCell;
                    mxCellIdx++;
                }
                pageIdx++; 
            }
        }
        
        static public void Convert(List<CsvRow> items, Mxfile mxfile)
        {
            Dictionary<String, CsvArrangementEx> arrangements = new();
            Dictionary<String, CsvThemeEx> themes = new();
            Dictionary<String, CsvStyleEx> styles = new();
            Dictionary<String, CsvTextEx> texts = new();
            Dictionary<ResourceType, dynamic> resources = new(){{ResourceType.Arrangements, arrangements},{ResourceType.Themes, themes},{ResourceType.Styles, styles},{ResourceType.Texts, texts}};
            Dictionary<String, Dictionary<String, MxCell>> pages = new();
            foreach (CsvRow item in items)
            {
                String id = item.Id;
                CsvRowEx? expand = item.Ex;
                resources[ResourceType.Id] = item.Id;
                if (expand is not null)
                {
                    if (CsvRowType.Arrangement == expand.Type) RecordResource<CsvArrangementEx>(id, (CsvArrangementEx)expand, arrangements); 
                    if (CsvRowType.Theme == expand.Type) RecordResource<CsvThemeEx>(id, (CsvThemeEx)expand, themes); 
                    if (CsvRowType.Style == expand.Type) RecordResource<CsvStyleEx>(id, (CsvStyleEx)expand, styles); 
                    if (CsvRowType.Text == expand.Type) RecordResource<CsvTextEx>(id, (CsvTextEx)expand, texts); 
                    if (CsvRowType.Page == expand.Type) RecordDiagram(id, (CsvPageEx)expand, pages); 
                    if (CsvRowType.Layer == expand.Type) RecordMxCell(id, (CsvLayerEx)expand, pages); 
                    if (CsvRowType.Entity == expand.Type) RecordMxCell(id, (CsvEntityEx)expand, pages, resources); 
                    if (CsvRowType.Joint == expand.Type) RecordMxCell(id, (CsvJointEx)expand, pages, resources); 
                } 
            }
            Convert(pages, mxfile);
        }

        static public void Convert(string path, Mxfile mxfile)
        {
            if(File.Exists(path))
            {
                using (TextFieldParser parser = new TextFieldParser(path))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int row = 1;
                    List<CsvRow> items = new ();
                    while (!parser.EndOfData)
                    {
                        int column = 1;
                        string[]? fields = parser.ReadFields();
                        if (fields is not null) foreach (string field in fields)
                        {
                            if (1 == column) items.Add(new CsvRow(row, field));
                            if (2 == column)
                            {
                                items[items.Count-1].Ex = ("arrangement" == field) ? new CsvArrangementEx() :
                                                          ("theme"   == field) ? new CsvThemeEx() :
                                                          ("style"   == field) ? new CsvStyleEx() :
                                                          ("text"   == field) ? new CsvTextEx() :
                                                          ("page"   == field) ? new CsvPageEx() :
                                                          ("layer"  == field) ? new CsvLayerEx() :
                                                          ("entity" == field) ? new CsvEntityEx() :
                                                          ("joint"  == field) ? new CsvJointEx() :
                                                          default;
                            }
                            if (3 <= column)
                            {
                                CsvRowEx? ex = items[items.Count-1].Ex;
                                if (ex != default)
                                    ex.SetAttribute(column-2, field);
                            }
                            column++;
                        }
                        row++;
                    }
                    Convert(items, mxfile);
                }
            }
            else
                System.Console.WriteLine($"The {path} isn`t existed.");
        }
    }

    public enum CsvRowType
    {
        Unknown,
        Arrangement,
        Theme,
        Style,
        Text,
        Page,
        Layer,
        Entity,
        Joint 
    }

    public enum ResourceType
    {
        Id,
        Arrangements,
        Themes,
        Styles,
        Texts
    }
    
    public class CsvRow
    {
        public int Idx;
        public string Id;
        public CsvRowEx? Ex;
        public CsvRow(int idx, string id)
        {
            this.Idx = idx;
            this.Id = id;
        }
        
    }

    public class CsvRowEx
    {
        public CsvRowType Type;
        public CsvRowEx() { this.Type = CsvRowType.Unknown;}
        public void SetAttribute(int idx, string attribute)
        {
            if (CsvRowType.Arrangement == this.Type) ((CsvArrangementEx)this).SetAttribute(idx, attribute); 
            if (CsvRowType.Theme == this.Type) ((CsvThemeEx)this).SetAttribute(idx, attribute); 
            if (CsvRowType.Style == this.Type) ((CsvStyleEx)this).SetAttribute(idx, attribute); 
            if (CsvRowType.Text == this.Type) ((CsvTextEx)this).SetAttribute(idx, attribute); 
            if (CsvRowType.Page == this.Type) ((CsvPageEx)this).SetAttribute(idx, attribute); 
            if (CsvRowType.Layer == this.Type) ((CsvLayerEx)this).SetAttribute(idx, attribute); 
            if (CsvRowType.Entity == this.Type) ((CsvEntityEx)this).SetAttribute(idx, attribute); 
            if (CsvRowType.Joint == this.Type) ((CsvJointEx)this).SetAttribute(idx, attribute); 
        }
    }

    public class CsvArrangementEx : CsvRowEx
    {
        public string Name = "";
        public string X = "";
        public string Y = "";
        public string Height = "";
        public string Width = "";
        public string X_offset = "0";
        public string Y_offset = "0";
        public string H_offset = "0";
        public string W_offset = "0";
        public CsvArrangementEx() {this.Type = CsvRowType.Arrangement;}
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.Name = attribute;
            if (2 == idx) this.X = attribute;
            if (3 == idx) this.Y = attribute;
            if (4 == idx) this.Height = attribute;
            if (5 == idx) this.Width = attribute;
            if (6 == idx) this.X_offset = attribute;
            if (7 == idx) this.Y_offset = attribute;
            if (8 == idx) this.H_offset = attribute;
            if (9 == idx) this.W_offset = attribute;
        }
    }

    public class CsvThemeEx : CsvRowEx
    {
        public string Description = "";
        public string FillColor = "";
        public string StrokeColor = "";
        public string FontColor = "";
        public bool Auto = false;
        public CsvThemeEx() {
            this.Type = CsvRowType.Theme;
            this.FontColor = "#000000";
        }
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.Description = attribute;
            if (2 == idx) this.FontColor = attribute;
            if (3 == idx) this.FillColor = attribute;
            if (4 == idx) this.StrokeColor = attribute;
            this.Auto = true;
        }
        public void Shake()
        {
            string BasicColor = ColorTool.Generate();
            this.FillColor = ColorTool.Offset(BasicColor, ("#FFFFFF" == this.FontColor) ? -32 : 32);
            this.StrokeColor = ColorTool.Offset(BasicColor, ("#FFFFFF" == this.FontColor) ? 32 : -32);
        }
    }

    public class CsvStyleEx : CsvRowEx
    {
        public string Description = "";
        public string Context = "";
        public string ThemeIdx = "";
        public CsvStyleEx() {this.Type = CsvRowType.Style;}
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.Description = attribute;
            if (2 == idx) this.Context = attribute;
            if (3 == idx) this.ThemeIdx = attribute;
        }
    }

    public class CsvTextEx : CsvRowEx
    {
        public string Text = "";
        public CsvTextEx() {this.Type = CsvRowType.Text;}
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.Text = attribute;
        }
    }

    public class CsvPageEx : CsvRowEx
    {
        public string Category = "";
        public CsvPageEx() {this.Type = CsvRowType.Page;}
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.Category = attribute;
        }
    }

    public class CsvLayerEx : CsvRowEx
    {
        public string PageId = "";
        public string Topic = "";
        public CsvLayerEx() {this.Type = CsvRowType.Layer;}
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.PageId = attribute;
            if (2 == idx) this.Topic = attribute;
        }
    }

    public class CsvEntityEx : CsvRowEx
    {
        public string PageId = "";
        public string LayerId = "";
        public string Arrangement = "";
        public string Description = "";
        public string Style = "";
        public string Theme = "";
        public string SupplementalStyle = "";
        public CsvEntityEx() {this.Type = CsvRowType.Entity;}
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.PageId = attribute;
            if (2 == idx) this.LayerId = attribute;
            if (3 == idx) this.Arrangement = attribute;
            if (4 == idx) this.Description = attribute;
            if (5 == idx) this.Style = attribute;
            if (6 == idx) this.Theme = attribute;
            if (7 == idx) this.SupplementalStyle = attribute;
        }
    }

    public class CsvJointEx : CsvRowEx
    {
        public string PageId = "";
        public string LayerId = "";
        public string Source = "";
        public string Target = "";
        public string Style = "";
        public string Theme = "";
        public string SupplementalStyle = "";
        public CsvJointEx() {this.Type = CsvRowType.Joint;}
        public new void SetAttribute(int idx, string attribute)
        {
            if (1 == idx) this.PageId = attribute;
            if (2 == idx) this.LayerId = attribute;
            if (3 == idx) this.Source = attribute;
            if (4 == idx) this.Target = attribute;
            if (5 == idx) this.Style = attribute;
            if (6 == idx) this.Theme = attribute;
            if (7 == idx) this.SupplementalStyle = attribute;
        }
    }
}
