using System.Windows.Input;

namespace CoGet.Dialog.PackageManagerUI
{

    public static class PackageManagerWindowCommands
    {
        public readonly static RoutedCommand PackageOperationCommand = new RoutedCommand();
        public readonly static RoutedCommand PackageOperationCommand2 = new RoutedCommand();

        public readonly static RoutedCommand ShowOptionsPage = new RoutedCommand();
        public readonly static RoutedCommand FocusOnSearchBox = new RoutedCommand();
        public readonly static RoutedCommand OpenExternalLink = new RoutedCommand();
    }
}
