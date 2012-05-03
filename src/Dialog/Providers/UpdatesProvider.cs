using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using EnvDTE;
using Microsoft.VisualStudio.ExtensionsExplorer;
//using CoApp.VsExtension.VisualStudio;
using CoApp.Toolkit.Engine.Client;

namespace CoApp.VsExtension.Dialog.Providers
{
    internal class UpdatesProvider : OnlineProvider
    {
        public UpdatesProvider()
        {
        }

        public override string Name
        {
            get
            {
                return Resources.Dialog_UpdateProvider;
            }
        }

        public override float SortOrder
        {
            get
            {
                return 3.0f;
            }
        }

        public override bool RefreshOnNodeSelection
        {
            get
            {
                return true;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "We want to suppress all errors to show an empty node.")]
        protected override void FillRootNodes()
        {
            var repository = new UpdatesRepository();

            PackagesTreeNodeBase node = CreateTreeNodeForPackages(repository);
            RootNode.Nodes.Add(node);
        }

        protected override PackagesTreeNodeBase CreateTreeNodeForPackages(IPackageRepository repository)
        {
            return new SimpleTreeNode(this, "All", RootNode, repository);
        }

        public override bool CanExecute(PackageItem item)
        {
            return false;
        }

        public override IVsExtension CreateExtension(Package package)
        {
            return new PackageItem(this, package, isUpdateItem: true)
            {
                CommandName = Resources.Dialog_UpdateButton
            };
        }

        public override string NoItemsMessage
        {
            get
            {
                return Resources.Dialog_UpdatesProviderNoItem;
            }
        }

        public override string ProgressWindowTitle
        {
            get
            {
                return Resources.Dialog_UpdateProgress;
            }
        }

        protected override string GetProgressMessage(Package package)
        {
            return Resources.Dialog_UpdateProgress + package.ToString();
        }
    }
}