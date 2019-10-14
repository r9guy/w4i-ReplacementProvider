using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CeliAPS.T4I.Woodwork4Inventor.ReplacementProvider.Interfaces;
using Microsoft.VisualBasic.FileIO;
using ReplacementProviderExample.Properties;

namespace ReplacementProviderExample
{
    /// <summary>
    /// Example replacer class. Replace values with specified new values in csv. Replacements are compatible with "Slant Box" model. Found in Woodwork for Inventor samples.
    /// </summary>
    public class Replacer
    {

        ReplacementProvider ReplacementData=new ReplacementProvider();

       // private readonly IList<ReplacementRecord> _materialReplacementRecords = ReadRecords<ReplacementRecord>(Resources.MaterialsDataSource);
       // private readonly IList<ReplacementRecord> _colorReplacementRecords = ReadRecords<ReplacementRecord>(Resources.ColorsDataSource);
       // private readonly IList<ReplacementRecord> _hardwareReplacementRecords = ReadRecords<ReplacementRecord>(Resources.HardwareDataSource);
       // private readonly IList<ReplacementGroupRecord> _materialGroupReplacementRecords = ReadRecords<ReplacementGroupRecord>(Resources.MaterialGroupsDataSource);

        public void ReplaceHardware(IHardwareComponent hardwareComponent)
        {
            /*var matchedRecord = _hardwareReplacementRecords.SingleOrDefault(x => x.Code == hardwareComponent.Code && x.Name == hardwareComponent.Name);
            if (matchedRecord != null)
            {
                hardwareComponent.NewCode = matchedRecord.NewCode;
                hardwareComponent.NewName = matchedRecord.NewName;
                hardwareComponent.NewAppearance = matchedRecord.NewAppearance;

                hardwareComponent.SetAddedProperty("Explanation", matchedRecord.AdditionalValue);
                hardwareComponent.SetAddedProperty("Explanation 2", matchedRecord.AdditionalValue2);
            }*/
        }

        public void ReplaceColor(IColor color)
        {
            /*var matchedRecord = _colorReplacementRecords.SingleOrDefault(x => x.Code == color.Code && x.Name == color.Name);
            if (matchedRecord != null)
            {
                color.NewCode = matchedRecord.NewCode;
                color.NewName = matchedRecord.NewName;
                color.NewAppearance = Path.Combine(AssemblyDirectory, matchedRecord.NewAppearance);
            }*/
        }

        public void ReplaceMaterial(IMaterial material)
        {
            /*var matchedRecord = _materialReplacementRecords.SingleOrDefault(x => x.Code == material.Code && x.Name == material.Name);

            if (matchedRecord != null)
            {
                material.NewCode = matchedRecord.NewCode;
                material.NewName = matchedRecord.NewName;
                material.NewAppearance = matchedRecord.NewAppearance;
            }*/

            
            if (ReplacementData.ShowDialog()==DialogResult.OK)
            {
                material.NewCode = ReplacementData.newCode;
                material.NewName = ReplacementData.newName;
            }

        }

        public void ReplaceMaterialGroup(IMaterial material, IColor color, double width, double thickness)
        {
            /*var colorCode = color == null ? "" : color.Code;
            var colorName = color == null ? "" : color.Name;

            var matchedRecord = _materialGroupReplacementRecords.SingleOrDefault(x =>
                x.MaterialCode == material.Code && x.MaterialName == material.Name &&
                x.ColorCode == colorCode && x.ColorName == colorName &&
                Math.Abs(x.Width - width) < 0.00001 && Math.Abs(x.Thickness - thickness) < 0.00001);

            if (matchedRecord != null)
            {
                if (color != null)
                {
                    color.NewName = matchedRecord.NewColorName;
                    color.NewCode = matchedRecord.NewColorCode;
                    color.NewAppearance = Path.Combine(AssemblyDirectory, matchedRecord.NewColorAppearance);
                }

                material.NewCode = matchedRecord.NewMaterialCode;
                material.NewName = matchedRecord.NewMaterialName;
            }*/
            
            if(ReplacementData.FindReplacementMaterial(material.Code,width,thickness))
            {
                material.NewCode = ReplacementData.newCode;
                material.NewName = ReplacementData.newName;
            }
        }

        private static IList<T> ReadRecords<T>(string content)
        {
            var records = new List<T>();
            using (TextFieldParser parser = new TextFieldParser(new StringReader(content)) { Delimiters = new[] { "," } })
            {
                var indexByColumn = new Dictionary<string, int>();
                var headerRow = parser.ReadFields();
                for (var i = 0; i < headerRow.Length; i++)
                    indexByColumn.Add(headerRow[i], i);

                while (!parser.EndOfData)
                {
                    var row = parser.ReadFields();
                    if (row == null)
                        continue;

                    records.Add((T)Activator.CreateInstance(typeof(T), row, indexByColumn));
                }
            }

            return records;
        }


        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}