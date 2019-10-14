using System.Collections.Generic;
using CeliAPS.T4I.Woodwork4Inventor.ReplacementProvider.Interfaces;
using System.Windows.Forms;

namespace ReplacementProviderExample
{
    /// <summary>
    /// Example Woodwork for Inventor replacement provider class.
    /// </summary>
    /// <seealso cref="IReplacementProviderEx" />
    public class Provider : IReplacementProviderEx  // Woodwork4Inventor.ReplacementProvider.dll must be referenced. Can be found in C:\Program Files\Woodwork for Inventor (Inventor version) (Woodwork version)
    {
        private readonly Replacer _replacer = new Replacer();

        /// <summary>
        /// This method is called by Woodwork for Inventor when replacement for hardware component, material or color is requested.
        /// </summary>
        /// <param name="replacementData">Provides access to replace configuration and replaceable object.</param>
        /// 
        
        public void Replace(IReplacementData replacementData)
        {
            // Replacement data can be of 4 types. By finding out the type we can determine what item replacement is requested.
            // IMaterialGroupReplacementData - material, color and size combination
            // IMaterialReplacementData - generic material
            // IColorReplacementData - generic color
            // IHardwareReplacementData - hardware item

            switch (replacementData)
            {
                case IMaterialGroupReplacementData materialGroupReplacementData:
                    var width = 0.0;
                    var thickness = 0.0;

                    // Get material width and thickness if material with size was selected.
                    if (materialGroupReplacementData is IMaterialSizeReplacementData materialSizeReplacementData)
                    {
                        width = materialSizeReplacementData.Width;
                        thickness = materialSizeReplacementData.Thickness;
                    }

                    _replacer.ReplaceMaterialGroup(materialGroupReplacementData.Material, materialGroupReplacementData.Color, width, thickness);
                    break;

                case IMaterialReplacementData materialReplacementData:
                    _replacer.ReplaceMaterial(materialReplacementData.Material);
                    break;

                case IColorReplacementData colorReplacementData:
                    _replacer.ReplaceColor(colorReplacementData.Color);
                    break;

                case IHardwareReplacementData hardwareReplacementData:
                    _replacer.ReplaceHardware(hardwareReplacementData.HardwareComponent);
                    break;
            }
        }

        /// <summary>
        /// This method is called by Woodwork for Inventor when replacement for all BOM hardware components or materials is requested.
        /// </summary>
        /// <param name="replacementData">Provides access to replace configuration and replaceable objects.</param>
        public void ReplaceAll(IEnumerable<IReplacementData> replacementData)
        {
            int count = 0;
            foreach (var data in replacementData)
            {
                Replace(data);
                count++;
            }
            
        }
    }
}