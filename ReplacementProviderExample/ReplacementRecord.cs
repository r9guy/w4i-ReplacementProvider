using System.Collections.Generic;

namespace ReplacementProviderExample
{
    /// <summary>
    /// Class to store data from Colors, Hardware and Materials csv files.
    /// </summary>
    public class ReplacementRecord
    {
        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the new code.
        /// </summary>
        public string NewCode { get; }
        /// <summary>
        /// Gets the new name.
        /// </summary>
        public string NewName { get; }
        /// <summary>
        /// Gets the new appearance.
        /// </summary>
        public string NewAppearance { get; }

        /// <summary>
        /// Gets or sets the additional value.
        /// </summary>
        public string AdditionalValue { get; set; }
        /// <summary>
        /// Gets or sets the additional value2.
        /// </summary>
        public string AdditionalValue2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplacementRecord"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="indexByColumn">The index by column.</param>
        public ReplacementRecord(IReadOnlyList<string> row, IDictionary<string, int> indexByColumn)
        {
            Code = row[indexByColumn["Code"]];
            Name = row[indexByColumn["Name"]];
            NewCode = row[indexByColumn["New Code"]];
            NewName = row[indexByColumn["New Name"]];
            NewAppearance = row[indexByColumn["Image File"]];

            if (indexByColumn.ContainsKey("AdditionalValue"))
                AdditionalValue = row[indexByColumn["AdditionalValue"]];

            if (indexByColumn.ContainsKey("AdditionalValue2"))
                AdditionalValue2 = row[indexByColumn["AdditionalValue2"]];
        }
    }
}