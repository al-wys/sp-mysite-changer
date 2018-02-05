using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;

namespace MySiteChanger.Features.MySiteMasterChanger
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("b33f0505-7e9e-4fad-982d-83020fc8b2e8")]
    public class MySiteMasterChangerEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            if (properties.Feature.Parent is SPSite site)
            {
                var webTemplate = site.RootWeb.WebTemplate.ToUpper();
                if (webTemplate.StartsWith("SPSPERS")) // This is a personal site
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate () // Ensure this operation is executed by an administrator
                    {
                        using (var siteWithAdmin = new SPSite(site.Url))
                        {
                            // Change the root web's master url
                            var rootWeb = siteWithAdmin.RootWeb;
                            var masterUrl = rootWeb.MasterUrl.ToLower();
                            rootWeb.MasterUrl = masterUrl.Replace("mysite15", "seattle");

                            rootWeb.Update();
                        }
                    });
                }
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            if (properties.Feature.Parent is SPSite site)
            {
                var webTemplate = site.RootWeb.WebTemplate.ToUpper();
                if (webTemplate.StartsWith("SPSPERS")) // This is a personal site
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate () // Ensure this operation is executed by an administrator
                    {
                        using (var siteWithAdmin = new SPSite(site.Url))
                        {
                            // Change the root web's master url
                            var rootWeb = siteWithAdmin.RootWeb;
                            var masterUrl = rootWeb.MasterUrl.ToLower();
                            rootWeb.MasterUrl = masterUrl.Replace("seattle", "mysite15");

                            rootWeb.Update();
                        }
                    });
                }
            }
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
