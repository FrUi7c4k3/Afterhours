namespace MiX.AM.DT.Diagnostics.AssetViewer {
    using System.ComponentModel.Composition;

    [Export(typeof(IShell))]
    public class ShellViewModel : IShell {}
}
