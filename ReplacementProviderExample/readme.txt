1. Run ReplacementProviderExample.sln with Visual Studio or any C# development environment.
2. Add new reference to Woodwork4Inventor.ReplacementProvider.dll (References -> Add Reference). Woodwork4Inventor.ReplacementProvider.dll comes with Woodwork for Inventor installation, can be found at „C:\Program Files\Woodwork for Inventor 2020 v10“.
3. Compile project.
4. Check "Use custom replacement provider" in Woodwork for Inventor BOM settings and browse compiled .dll.
5. Example designed to work with „Slant Box“ model. That can be found in Woodwork for Inventor samples project „W4INV 2020 Design“.
6. Values in material and hardware replacement dialog will be applied from compiled application when clicked on Replace button for specific field or Replace All ribbon button.