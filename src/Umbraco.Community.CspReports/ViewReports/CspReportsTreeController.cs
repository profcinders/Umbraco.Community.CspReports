using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.ModelBinders;
using Umbraco.Community.CSPManager;

namespace Umbraco.Community.CspReports.ViewReports
{
    [PluginController(CspReportsConstants.PluginAlias)]
    [Tree(
        CspConstants.PluginAlias,
        CspReportsConstants.TreeAlias,
        TreeTitle = "CSP Reports",
        IsSingleNodeTree = true,
        SortOrder = 10)]
    public sealed class CspReportsTreeController(
        ILocalizedTextService localizedTextService,
        UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection,
        IEventAggregator eventAggregator)
        : TreeController(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
    {
#pragma warning disable CS8603 // Possible null reference return.
        protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
            => default;
#pragma warning restore CS8603 // Possible null reference return.

        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormCollection queryStrings)
            => TreeNodeCollection.Empty;

        protected override ActionResult<TreeNode?> CreateRootNode(FormCollection queryStrings)
        {
            var root = base.CreateRootNode(queryStrings);
            if (root.Value is null) return root;

            root.Value.RoutePath = $"{CspConstants.PluginAlias}/{CspReportsConstants.TreeAlias}/reports-list";
            root.Value.Icon = "icon-inbox-full";
            root.Value.HasChildren = false;
            root.Value.MenuUrl = null;

            return root;
        }
    }
}
