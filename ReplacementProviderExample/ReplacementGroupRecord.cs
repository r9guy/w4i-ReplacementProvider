using System.Collections.Generic;

namespace ReplacementProviderExample
{
    /// <summary>
    /// Class to store data from Material groups data source csv file.
    /// </summary>
    public class ReplacementGroupRecord
    {
        /// <summary>
        /// Gets the material code.
        /// </summary>
        public string MaterialCode { get; }
        /// <summary>
        /// Gets the name of the material.
        /// </summary>
        public string MaterialName { get; }
        /// <summary>
        /// Gets the new material code.
        /// </summary>
        public string NewMaterialCode { get; }
        /// <summary>
        /// Gets the new name of the material.
        /// </summary>
        public string NewMaterialName { get; }
        /// <summary>
        /// Gets the new material appearance.
        /// </summary>
        public string NewMaterialAppearance { get; }

        /// <summary>
        /// Gets the color code.
        /// </summary>
        public string ColorCode { get; }
        /// <summary>
        /// Gets the name of the color.
        /// </summary>
        public string ColorName { get; }
        /// <summary>
        /// Gets the new color code.
        /// </summary>
        public string NewColorCode { get; }
        /// <summary>
        /// Gets the new name of the color.
        /// </summary>
        public string NewColorName { get; }
        /// <summary>
        /// Gets the new color appearance.
        /// </summary>
        public string NewColorAppearance { get; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplacementGroupRecord"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="indexByColumn">The index by column.</param>
        public ReplacementGroupRecord(IReadOnlyList<string> row, IDictionary<string, int> indexByColumn)
        {
            MaterialCode = row[indexByColumn["Material Code"]];
            MaterialName = row[indexByColumn["Material Name"]];
            NewMaterialCode = row[indexByColumn["New Material Code"]];
            NewMaterialName = row[indexByColumn["New Material Name"]];
            NewMaterialAppearance = row[indexByColumn["Material Image File"]];

            ColorCode = row[indexByColumn["Color Code"]];
            ColorName = row[indexByColumn["Color Name"]];
            NewColorCode = row[indexByColumn["New Color Code"]];
            NewColorName = row[indexByColumn["New Color Name"]];
            NewColorAppearance = row[indexByColumn["Color Image File"]];

            Width = double.Parse(row[indexByColumn["Width"]]);
            Thickness = double.Parse(row[indexByColumn["Thickness"]]);
        }
    }
}